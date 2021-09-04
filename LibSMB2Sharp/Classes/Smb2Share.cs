using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using LibSMB2Sharp.Exceptions;
using LibSMB2Sharp.Native;

namespace LibSMB2Sharp
{
    public class Smb2Share : Smb2DirectoryEntry, IDisposable
    {
        public uint FsBlockSize { get; private set; }
        public uint FsFragementSize { get; private set; }
        public ulong FsBlocks { get; private set; }
        public ulong FsBlocksFree { get; private set; }
        public ulong FsBlocksAvailable { get; private set; }
        public uint FsFiles { get; private set; }
        public uint FsFilesFree { get; private set; }
        public uint FsFilesAvailable { get; private set; }
        public uint FsID { get; private set; }
        public uint FsFlag { get; private set; }
        public uint FsNameMax { get; private set; }

        public Smb2Context Context { get; private set; }

        private IntPtr _contextPtr = IntPtr.Zero;
        private IntPtr _rootDirPtr = IntPtr.Zero;
        private List<Smb2DirectoryEntry> _openDirEntries = new List<Smb2DirectoryEntry>();
        
        internal Smb2Share(Smb2Context context, IntPtr contextPtr)
            : base(contextPtr)
        { 
            this._share = this;
            this._isRootShare = true;
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            _contextPtr = contextPtr;

            smb2dirent? dirEntN = this.GetDirEnt("/");
            smb2dirent dirEnt = dirEntN.Value;

            this.SetDetails(ref dirEnt);

            // IntPtr ptr = IntPtr.Zero;

            // try
            // {
            //     ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(smb2_statvfs)));

            //     int result = Methods.smb2_statvfs(_contextPtr, "", ptr);

            //     if (result < Const.EOK)
            //         throw new LibSmb2NativeMethodException(_contextPtr, result);

            //     smb2_statvfs statvfs = Marshal.PtrToStructure<smb2_statvfs>(ptr);

            //     this.FsBlockSize = statvfs.f_bsize;
            //     this.FsFragementSize = statvfs.f_frsize;
            //     this.FsBlocks = statvfs.f_blocks;
            //     this.FsBlocksFree = statvfs.f_bfree;
            //     this.FsBlocksAvailable = statvfs.f_bavail;
            //     this.FsFiles = statvfs.f_files;
            //     this.FsFilesFree = statvfs.f_ffree;
            //     this.FsFilesAvailable = statvfs.f_favail;
            //     this.FsID = statvfs.f_fsid;
            //     this.FsFlag = statvfs.f_flag;
            //     this.FsNameMax = statvfs.f_namemax;
            // }
            // finally
            // {
            //     if (ptr != IntPtr.Zero)
            //         Marshal.FreeHGlobal(ptr);
            // }
        }

        public Smb2DirectoryEntry CreateDirectoryTree(string path)
        {                
            string rawPath = Helpers.CleanFilePathForNative(path);

            string testPath = rawPath.TrimEnd('/') + "/";

            // lets find the highest level dir that actually exists

            Smb2DirectoryEntry dirEntry = null;

            do
            {
                int index = testPath.LastIndexOf('/');
                
                if (index < 0)
                    index = 0;

                testPath = testPath.Substring(0, index);

                dirEntry = this.GetDirectory(testPath, false);

                if (dirEntry != null)
                    break;

            } while (testPath.Length > 0);

            if (dirEntry == null)
                throw new LibSmb2FileNotFoundException("");

            string remainingPath = rawPath.Substring(testPath.Length);
            
            string[] remainingParts = rawPath.Split('/');

            foreach (string part in remainingParts)
            {
                Smb2DirectoryEntry newDirEntry = dirEntry.CreateDirectory(part);
                dirEntry = newDirEntry;
            }

            return dirEntry;
        }

        public Smb2DirectoryEntry GetDirectory(string path, bool throwOnMissing = true)
        {
            path = path.Trim();

            if (String.IsNullOrWhiteSpace(path))
                return this;

            Smb2Entry entry = GetEntry(path, throwOnMissing);

            if (entry == null)
                return null;

            Smb2DirectoryEntry dirEntry = entry as Smb2DirectoryEntry;

            if (dirEntry == null)
                throw new LibSmb2NotADirectoryException(path);

            return dirEntry;
        }

        // private async Task<Smb2DirectoryEntry> GetDirectoryAsync(string path, bool throwOnMissing = true)
        // {
        //     path = path.Trim();

        //     if (String.IsNullOrWhiteSpace(path))
        //         return this;

        //     Smb2Entry entry = await GetEntryAsync(path, throwOnMissing);

        //     if (entry == null)
        //         return null;

        //     Smb2DirectoryEntry dirEntry = entry as Smb2DirectoryEntry;

        //     if (dirEntry == null)
        //         throw new LibSmb2NotADirectoryException(path);

        //     return dirEntry;
        // }

        public Smb2FileEntry GetFile(string path)
        {
            Smb2Entry entry = GetEntry(path);

            Smb2FileEntry fileEntry = entry as Smb2FileEntry;

            if (fileEntry == null)
                throw new LibSmb2NotAFileException(path);

            return fileEntry;
        }
        
        public Smb2Entry GetEntry(string path, bool throwOnMissing = true)
        {
            smb2dirent? dirEntN = this.GetDirEnt(path, throwOnMissing);

            if (dirEntN == null)
                return null;

            smb2dirent dirEnt = dirEntN.Value;

            string dirName = Path.GetDirectoryName(path);

            if (dirEnt.st.smb2_type == Const.SMB2_TYPE_DIRECTORY)
                dirName = Path.GetDirectoryName(dirName);

            return Helpers.GenerateEntry(_contextPtr, ref dirEnt, this, containingDir: dirName);
        }

        // public async Task<Smb2Entry> GetEntryAsync(string path, bool throwOnMissing = true)
        // {
        //     smb2dirent? dirEntN = await this.GetDirEntAsync(path, throwOnMissing);

        //     if (dirEntN == null)
        //         return null;

        //     smb2dirent dirEnt = dirEntN.Value;

        //     string dirName = Path.GetDirectoryName(path);

        //     if (dirEnt.st.smb2_type == Const.SMB2_TYPE_DIRECTORY)
        //         dirName = Path.GetDirectoryName(dirName);

        //     return Helpers.GenerateEntry(_contextPtr, ref dirEnt, this, containingDir: dirName);
        // }

        private smb2dirent? GetDirEnt(string path, bool throwOnMissing = true)
        {
            path = Helpers.CleanFilePath(path);

            smb2_stat_64? stat = Helpers.Stat(_contextPtr, path);

            if (stat == null)
            {
                if (throwOnMissing)
                    throw new LibSmb2FileNotFoundException(path);
                else
                    return null;
            } 

            path = path.TrimEnd('/');

            smb2dirent dirEnt = new smb2dirent()
            {
                name = Path.GetFileName(path),
                st = stat.Value
            };

            return dirEnt;
        }

        // private async Task<Nullable<smb2dirent>> GetDirEntAsync(string path, bool throwOnMissing = true)
        // {
        //     path = Helpers.CleanFilePath(path);

        //     smb2_stat_64? stat = await Helpers.StatAsync(_contextPtr, path);

        //     if (stat == null)
        //     {
        //         if (throwOnMissing)
        //             throw new LibSmb2FileNotFoundException(path);
        //         else
        //             return null;
        //     } 

        //     path = path.TrimEnd('/');

        //     smb2dirent dirEnt = new smb2dirent()
        //     {
        //         name = Path.GetFileName(path),
        //         st = stat.Value
        //     };

        //     return dirEnt;
        // }


        public override void Dispose()
        {
            base.Dispose();

            foreach (Smb2DirectoryEntry dirEntry in _openDirEntries)
                dirEntry.Dispose();

            Helpers.CloseDir(_contextPtr, ref _rootDirPtr);
        }

        internal void RegisterOpenDirectory(Smb2DirectoryEntry entry)
            => _openDirEntries.Add(entry);
    }
}