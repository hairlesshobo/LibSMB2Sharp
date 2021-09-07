using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
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

        public override Smb2Context Context => _context;

        private Smb2Context _context;

        private IntPtr _rootDirPtr = IntPtr.Zero;
        private List<Smb2DirectoryEntry> _openDirEntries = new List<Smb2DirectoryEntry>();
        
        internal Smb2Share(Smb2Context context)
            : base()
        { 
            this._share = this;
            this._isRootShare = true;
            _context = context ?? throw new ArgumentNullException(nameof(context));

            smb2dirent dirEnt = this.GetDirEnt("/") ?? throw new LibSmb2DirectoryNotFoundException("/");

            this.SetDetailsFromDirEnt(ref dirEnt);
            this.RefreshVfsDetails();
        }

        public void RefreshVfsDetails()
        {
            IntPtr ptr = IntPtr.Zero;

            try
            {
                ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(smb2_statvfs)));

                int result = Methods.smb2_statvfs(this.Context.Pointer, "", ptr);

                if (result < Const.EOK)
                    throw new LibSmb2NativeMethodException(this.Context, result);

                smb2_statvfs statvfs = Marshal.PtrToStructure<smb2_statvfs>(ptr);

                this.FsBlockSize = statvfs.f_bsize;
                this.FsFragementSize = statvfs.f_frsize;
                this.FsBlocks = statvfs.f_blocks;
                this.FsBlocksFree = statvfs.f_bfree;
                this.FsBlocksAvailable = statvfs.f_bavail;
                this.FsFiles = statvfs.f_files;
                this.FsFilesFree = statvfs.f_ffree;
                this.FsFilesAvailable = statvfs.f_favail;
                this.FsID = statvfs.f_fsid;
                this.FsFlag = statvfs.f_flag;
                this.FsNameMax = statvfs.f_namemax;
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    Marshal.FreeHGlobal(ptr);
            }
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

            if (!String.IsNullOrWhiteSpace(remainingPath))
            {
                string[] remainingParts = remainingPath.Split('/');

                foreach (string part in remainingParts)
                {
                    Smb2DirectoryEntry newDirEntry = dirEntry.CreateDirectory(part);
                    dirEntry = newDirEntry;
                }
            }

            return dirEntry;
        }

        public Smb2DirectoryEntry GetDirectory()
            => this.GetDirectory(null);

        public new Smb2DirectoryEntry GetDirectory(string path, bool throwOnMissing = true)
        {
            if (String.IsNullOrWhiteSpace(path))
                return this;

            path = Helpers.CleanFilePath(path);

            if (path == "/")
                return this;

            Smb2Entry entry = this.GetEntry(path, throwOnMissing);

            if (entry == null)
                return null;

            Smb2DirectoryEntry dirEntry = entry as Smb2DirectoryEntry;

            if (dirEntry == null)
                throw new LibSmb2NotADirectoryException(path);

            return dirEntry;
        }

        public new Smb2FileEntry CreateFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException($"'{nameof(path)}' cannot be null or whitespace.", nameof(path));

            path = Helpers.CleanFilePath(path);

            Smb2Entry entry = GetEntry(path, false);

            Smb2FileEntry fileEntry = entry as Smb2FileEntry;

            if (entry != null && fileEntry == null)
                throw new LibSmb2NotAFileException(path);

            if (fileEntry != null)
                fileEntry.Remove();

            string directory = Helpers.GetDirectoryPath(path);
            string name = path.Substring(directory.Length).TrimStart('/');
            Smb2DirectoryEntry dirEntry = this.GetDirectory(directory);
            fileEntry = new Smb2FileEntry(this.Share, dirEntry, name);

            return fileEntry;
        }

        public new Smb2FileEntry GetFile(string path, bool createNew = false)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException($"'{nameof(path)}' cannot be null or whitespace.", nameof(path));

            Smb2Entry entry = GetEntry(path, !createNew);

            Smb2FileEntry fileEntry = entry as Smb2FileEntry;

            // an entry exists, but it is not a file.. can't exactly use that
            if (entry != null && fileEntry == null)
                throw new LibSmb2NotAFileException(path);

            if (fileEntry == null && createNew)
                fileEntry = this.CreateFile(path);

            return fileEntry;
        }
        
        public new Smb2Entry GetEntry(string path, bool throwOnMissing = true)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException($"'{nameof(path)}' cannot be null or whitespace.", nameof(path));

            path = Helpers.CleanFilePath(path);

            smb2dirent? dirEntN = this.GetDirEnt(path, throwOnMissing);

            if (dirEntN == null)
                return null;

            smb2dirent dirEnt = dirEntN.Value;

            string dirName = Path.GetDirectoryName(path);

            return Helpers.GenerateEntry(this, ref dirEnt, containingDir: dirName);
        }

        internal new void CreateOrTruncateFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException($"'{nameof(path)}' cannot be null or whitespace.", nameof(path));

            string filePath = Helpers.CleanFilePathForNative(path);

            IntPtr fhPtr = Methods.smb2_open(this.Context.Pointer, filePath, Const.O_WRONLY | Const.O_CREAT);
            Methods.smb2_ftruncate(this.Context.Pointer, fhPtr, 0);
            Methods.smb2_close(this.Context.Pointer, fhPtr);
        }

        internal new smb2dirent? GetDirEnt(string path, bool throwOnMissing = true)
        {
            path = Helpers.CleanFilePath(path);

            smb2_stat_64? stat = Helpers.Stat(this.Context, path);

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

        public override void Dispose()
        {
            base.Dispose();

            foreach (Smb2DirectoryEntry dirEntry in _openDirEntries)
                dirEntry.Dispose();

            Helpers.CloseDir(this.Context, ref _rootDirPtr);
        }

        internal void RegisterOpenDirectory(Smb2DirectoryEntry entry)
            => _openDirEntries.Add(entry);


        // public async Task<Smb2Entry> GetEntryAsync(string path, bool throwOnMissing = true)
        // {
        //     smb2dirent? dirEntN = await this.GetDirEntAsync(path, throwOnMissing);

        //     if (dirEntN == null)
        //         return null;

        //     smb2dirent dirEnt = dirEntN.Value;

        //     string dirName = Path.GetDirectoryName(path);

        //     if (dirEnt.st.smb2_type == Const.SMB2_TYPE_DIRECTORY)
        //         dirName = Path.GetDirectoryName(dirName);

        //     return Helpers.GenerateEntry(this.Context.Pointer, ref dirEnt, this, containingDir: dirName);
        // }


        // private async Task<Nullable<smb2dirent>> GetDirEntAsync(string path, bool throwOnMissing = true)
        // {
        //     path = Helpers.CleanFilePath(path);

        //     smb2_stat_64? stat = await Helpers.StatAsync(this.Context.Pointer, path);

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
    }
}