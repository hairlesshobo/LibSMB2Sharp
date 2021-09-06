using System;
using LibSMB2Sharp.Native;

namespace LibSMB2Sharp
{
    public class Smb2FileEntry : Smb2Entry, IDisposable
    {
        private Smb2FileReader reader = null;

        public Smb2FileEntry (Smb2Share share, ref smb2dirent dirEnt, string containingDir = null, Smb2DirectoryEntry parentEntry = null)
            : base (share, ref dirEnt, containingDir, parentEntry)
        { 
        }

        public void Dispose()
            => reader?.Dispose();

        public Smb2FileReader OpenReader()
        {
            reader = new Smb2FileReader(this.Context, this);

            return reader;
        }

    }
}