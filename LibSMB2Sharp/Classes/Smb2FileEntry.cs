using System;
using LibSMB2Sharp.Exceptions;
using LibSMB2Sharp.Native;

namespace LibSMB2Sharp
{
    public class Smb2FileEntry : Smb2Entry, IDisposable
    {
        private bool _locked = false;
        private Smb2FileReader reader = null;
        private Smb2FileWriter writer = null;

        // TODO: without this, we cannot json serialize.. remove later
        public Smb2FileEntry() { }

        
        /// <summary>
        ///     Create a new file entry where one does not already exist. This method will
        ///     create an empty file with the provided named
        /// </summary>
        /// <param name="share"></param>
        /// <param name="dirEntry"></param>
        /// <param name="name"></param>
        internal Smb2FileEntry(Smb2Share share, Smb2DirectoryEntry dirEntry, string name)
            : base()
        {
            _share = share ?? throw new ArgumentNullException(nameof(share));
            this._parentDirEntry = dirEntry;
            this.Name = name;

            string newFilePath = this.RelativePath;

            this.Share.CreateOrTruncateFile(newFilePath);

            this.RefreshDetails();
        }

        internal Smb2FileEntry(Smb2Share share, ref smb2dirent dirEnt, string containingDir = null, Smb2DirectoryEntry parentEntry = null)
            : base(share, ref dirEnt, containingDir, parentEntry)
        {
        }

        public void Dispose()
            => reader?.Dispose();

        public Smb2FileReader OpenReader()
        {
            reader = new Smb2FileReader(this.Context, this);

            return reader;
        }

        public Smb2FileWriter OpenWriter()
        {
            writer = new Smb2FileWriter(this.Context, this);

            return writer;
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

            int result = Methods.smb2_unlink(this.Context.Pointer, Helpers.CleanFilePathForNative(this.RelativePath));

            if (result < 0)
                throw new LibSmb2NativeMethodException(this.Context, result);

            this._removed = true;
        }

        internal void LockFile()
            => _locked = true;

        internal void UnlockFile()
            => _locked = false;
    }
}