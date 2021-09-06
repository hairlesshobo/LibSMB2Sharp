using System;
using LibSMB2Sharp.Exceptions;
using LibSMB2Sharp.Native;

namespace LibSMB2Sharp
{
    public class Smb2FileEntry : Smb2Entry, IDisposable
    {
        private bool _locked = false;
        private Smb2FileReader reader = null;

        // TODO: without this, we cannot json serialize.. remove later
        public Smb2FileEntry() { }

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

        public void Move(string newPath)
        {
            if (_removed)
                throw new LibSmb2FileNotFoundException(this.RelativePath);

            if (_locked)
                throw new LibSmb2FileLockedException(this.RelativePath);
                
            base.Move(newPath, true);
        }

        public void Remove()
        {
            if (_removed)
                throw new LibSmb2FileNotFoundException(this.RelativePath);

            if (_locked)
                throw new LibSmb2FileLockedException(this.RelativePath);

            throw new NotImplementedException();
        }

        internal void LockFile()
            => _locked = true;

        internal void UnlockFile()
            => _locked = false;
    }
}