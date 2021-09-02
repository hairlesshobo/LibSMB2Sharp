using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using LibSMB2Sharp.Native;

namespace LibSMB2Sharp
{
    public class Smb2DirectoryEntry : Smb2Entry, IDisposable
    {
        // public override IEnumerable<Smb2Entry> Entries => GetEntries();

        private IntPtr _contextPtr = IntPtr.Zero;
        protected IntPtr _dirPtr = IntPtr.Zero;

        protected Smb2DirectoryEntry (IntPtr contextPtr)
            : base()
        {
            _contextPtr = contextPtr;
        }

        internal Smb2DirectoryEntry (IntPtr contextPtr, Smb2Share share, ref smb2dirent dirEnt, string containingDir = null, Smb2DirectoryEntry parentEntry = null)
            : base (share, ref dirEnt, containingDir, parentEntry)
        { 
            _contextPtr = contextPtr;

            this.Share.RegisterOpenDirectory(this);
        }

        private void Open()
            => Helpers.OpenDir(_contextPtr, ref _dirPtr, this.RelativePath);

        private void Close()
            => Helpers.CloseDir(_contextPtr, ref _dirPtr);

        public IEnumerable<Smb2Entry> GetEntries(bool ignoreSelfAndParent = true)
        {
            Open();

            IntPtr entPtr = IntPtr.Zero;

            while ((entPtr = Methods.smb2_readdir(_contextPtr, _dirPtr)) != IntPtr.Zero)
            {
                smb2dirent ent = Marshal.PtrToStructure<smb2dirent>(entPtr);

                if (ignoreSelfAndParent && (ent.name == "." || ent.name == ".."))
                    continue;

                yield return Helpers.GenerateEntry(_contextPtr, ref ent, _share, parent: this);
            }

            Close();
        }

        public virtual void Dispose()
            => Close();
    }
}