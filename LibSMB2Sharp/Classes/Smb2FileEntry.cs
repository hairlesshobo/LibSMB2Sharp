using System;
using LibSMB2Sharp.Native;

namespace LibSMB2Sharp
{
    public class Smb2FileEntry : Smb2Entry, IDisposable
    {
        private Smb2FileReader reader = null;
        private IntPtr _contextPtr = IntPtr.Zero;

        public Smb2FileEntry (IntPtr contextPtr, Smb2Share share, ref smb2dirent dirEnt, string containingDir = null, Smb2DirectoryEntry parentEntry = null)
            : base (share, ref dirEnt, containingDir, parentEntry)
        { 
            _contextPtr = contextPtr;
        }

        public void Dispose()
            => reader?.Dispose();

        public Smb2FileReader OpenReader()
        {
            reader = new Smb2FileReader(_contextPtr, this);

            return reader;
        }

    }
}