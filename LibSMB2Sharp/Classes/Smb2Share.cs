using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using LibSMB2Sharp.Exceptions;
using LibSMB2Sharp.Interfaces;
using LibSMB2Sharp.Native;

namespace LibSMB2Sharp
{
    public class Smb2Share : IDisposable
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

        public IEnumerable<ISmb2Entry> Entries => GetEntries();


        private IntPtr _contextPtr = IntPtr.Zero;
        private IntPtr _rootDirPtr = IntPtr.Zero;
        private Smb2DirectoryEntry _rootDir = null;
        private List<Smb2DirectoryEntry> _openDirEntries = new List<Smb2DirectoryEntry>();
        
        internal Smb2Share(IntPtr contextPtr)
        { 
            _contextPtr = contextPtr;

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

        // public Smb2DirectoryEntry GetDirectory(string path)
        //     => new Smb2DirectoryEntry(_contextPtr, path);

        // public Smb2FileEntry GetFile(string filePath)
        // {
        //     return new Smb2FileEntry();
        //     // using (Smb2DirectoryEntry dirEntry = this.OpenDirectory())
        // }

        public void Dispose()
        {
            foreach (Smb2DirectoryEntry dirEntry in _openDirEntries)
                dirEntry.Dispose();

            Helpers.CloseDir(_contextPtr, ref _rootDirPtr);
        }

        internal void RegisterOpenDirectory(Smb2DirectoryEntry entry)
            => _openDirEntries.Add(entry);

        private void Open()
            => Helpers.OpenDir(_contextPtr, ref _rootDirPtr);

        private void Close()
            => Helpers.CloseDir(_contextPtr, ref _rootDirPtr);

        private IEnumerable<ISmb2Entry> GetEntries(bool ignoreSelfAndParent = true)
        {
            Open();

            IntPtr entPtr = IntPtr.Zero;

            while ((entPtr = Methods.smb2_readdir(_contextPtr, _rootDirPtr)) != IntPtr.Zero)
            {
                smb2dirent ent = Marshal.PtrToStructure<smb2dirent>(entPtr);

                if (ignoreSelfAndParent && (ent.name == "." || ent.name == ".."))
                    continue;

                Smb2Entry newEntry = new Smb2Entry()
                {
                    Name = ent.name,
                    Size = ent.st.smb2_size,
                    ModifyDtm = DateTimeOffset.FromUnixTimeSeconds((long)ent.st.smb2_mtime),
                    CreateDtm = DateTimeOffset.FromFileTime((long)ent.st.smb2_ctime),
                    Type = (Smb2EntryType)ent.st.smb2_type,
                    RelativePath = Path.Combine("/", ent.name)
                };

                if (newEntry.Type == Smb2EntryType.Directory)
                    yield return new Smb2DirectoryEntry(_contextPtr, newEntry, this);
                else
                    yield return newEntry;
            }

            Close();
        }
    }
}