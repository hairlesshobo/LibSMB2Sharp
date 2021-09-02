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

            smb2dirent dirEnt = this.GetDirEnt("/");

            this.SetDetails(ref dirEnt);

            IntPtr ptr = IntPtr.Zero;

            try
            {
                ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(smb2_statvfs)));

                int result = Methods.smb2_statvfs(_contextPtr, "", ptr);

                if (result < 0)
                    throw new LibSmb2NativeMethodException(_contextPtr);

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

        public Smb2DirectoryEntry GetDirectory(string path)
        {
            Smb2Entry entry = GetEntry(path);

            Smb2DirectoryEntry dirEntry = entry as Smb2DirectoryEntry;

            if (dirEntry == null)
                throw new LibSmb2NotADirectoryException(path);

            return dirEntry;
        }

        public Smb2FileEntry GetFile(string path)
        {
            Smb2Entry entry = GetEntry(path);

            Smb2FileEntry fileEntry = entry as Smb2FileEntry;

            if (fileEntry == null)
                throw new LibSmb2NotAFileException(path);

            return fileEntry;
        }
        
        public Smb2Entry GetEntry(string path)
        {
            smb2dirent dirEnt = this.GetDirEnt(path);

            string dirName = Path.GetDirectoryName(path);

            return Helpers.GenerateEntry(_contextPtr, ref dirEnt, this, containingDir: dirName);
        }

        private smb2dirent GetDirEnt(string path)
        {
            smb2_stat_64? stat = Helpers.Stat(_contextPtr, path);

            if (stat == null)
                throw new LibSmb2FileNotFoundException(path);

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

            Helpers.CloseDir(_contextPtr, ref _rootDirPtr);
        }

        internal void RegisterOpenDirectory(Smb2DirectoryEntry entry)
            => _openDirEntries.Add(entry);
    }
}