using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using LibSMB2Sharp.Exceptions;
using LibSMB2Sharp.Native;

namespace LibSMB2Sharp
{
    public class Smb2DirectoryEntry : Smb2Entry, IDisposable
    {
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

        public Smb2DirectoryEntry CreateDirectory(string name)
        {
            // TODO: impove name sanitization

            if (name.IndexOf('/') >= 0 || name.IndexOf('\\') >= 0)
                throw new LibSmb2InvalidDirectoryNameException(name);

            string dirNameRelative = $"{this.RelativePath}/{name}";
                
            int result = Methods.smb2_mkdir(_contextPtr, Helpers.CleanFilePathForNative(dirNameRelative));

            if (result < 0)
                throw new LibSmb2NativeMethodException(_contextPtr);
            
            return _share.GetDirectory(dirNameRelative);
        }

        public virtual void Dispose()
            => Close();

        private void Open()
            => Helpers.OpenDir(_contextPtr, ref _dirPtr, this.RelativePath);

        private void Close()
            => Helpers.CloseDir(_contextPtr, ref _dirPtr);

    }
}