using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using LibSMB2Sharp.Exceptions;
using LibSMB2Sharp.Interfaces;
using LibSMB2Sharp.Native;

namespace LibSMB2Sharp
{
    public class Smb2DirectoryEntry : Smb2Entry, IDisposable
    {
        public IEnumerable<ISmb2Entry> Entries => GetEntries();

        private IntPtr _contextPtr = IntPtr.Zero;
        private IntPtr _dirPtr = IntPtr.Zero;
        private Smb2Share _share;

        internal Smb2DirectoryEntry(IntPtr contextPtr, Smb2Entry entry, Smb2Share share)
        {
            _contextPtr = contextPtr;
            _share = share ?? throw new ArgumentNullException(nameof(share));

            if (entry is null)
                throw new ArgumentNullException(nameof(entry));
            
            this.CreateDtm = entry.CreateDtm;
            this.ModifyDtm = entry.ModifyDtm;
            this.Name = entry.Name;
            this.Size = entry.Size;
            this.Type = entry.Type;
            this.RelativePath = entry.RelativePath;

            _share.RegisterOpenDirectory(this);
        }

        // internal Smb2DirectoryEntry(IntPtr contextPtr, string path)
        // {
        //     _contextPtr = contextPtr;

        //     path = path?.Replace('\\', '/');

        //     if (path != null && path.Substring(0, 1) != "/")
        //         path = $"/{path}";

        //     this.RelativePath = path ?? throw new ArgumentNullException(nameof(path));
        // }

        private void Open()
            => Helpers.OpenDir(_contextPtr, ref _dirPtr, this.RelativePath);

        private void Close()
            => Helpers.CloseDir(_contextPtr, ref _dirPtr);

        private IEnumerable<ISmb2Entry> GetEntries(bool ignoreSelfAndParent = true)
        {
            Open();

            IntPtr entPtr = IntPtr.Zero;

            while ((entPtr = Methods.smb2_readdir(_contextPtr, _dirPtr)) != IntPtr.Zero)
            {
                smb2dirent ent = Marshal.PtrToStructure<smb2dirent>(entPtr);

                if (ignoreSelfAndParent && (ent.name == "." || ent.name == ".."))
                    continue;

                yield return Helpers.GenerateEntry(_contextPtr, ref ent, _share, this.RelativePath);
            }

            Close();
        }

        public void Dispose()
            => Close();
    }
}