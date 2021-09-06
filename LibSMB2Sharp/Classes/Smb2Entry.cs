using System;
using System.IO;
using LibSMB2Sharp.Native;

namespace LibSMB2Sharp
{
    public enum Smb2EntryType
    {
        File = 0,
        Directory = 1
    }

    public abstract class Smb2Entry
    {
        protected Smb2DirectoryEntry _parentDirEntry = null;
        protected Smb2Share _share = null;
        protected bool _isRootShare = false;

        public string Name { get; private protected set; }
        public string RelativePath {
            get
            {
                if (this._parentDirEntry == null && !String.IsNullOrWhiteSpace(this._containingDir))
                    return Path.Combine(Helpers.CleanFilePath(this._containingDir), this.Name);

                return Path.Combine((this._parentDirEntry == null ? "/" : this._parentDirEntry.RelativePath), this.Name);
            }
        }
        public string Directory => Path.GetDirectoryName(this.RelativePath);
        public string UncPath => $"{this._share.Context.UncPath}{this.RelativePath}";

        public ulong Size { get; private protected set; }

        public DateTimeOffset AcccessDtm { get; private protected  set; }
        public DateTimeOffset ChangeDtm { get; private protected  set; }
        public DateTimeOffset CreateDtm { get; private protected  set; }
        public DateTimeOffset ModifyDtm { get; private protected set; }
        public Smb2EntryType Type { get; private protected set; }
        public Smb2Share Share => _share;
        public virtual Smb2Context Context => this.Share.Context;
        private smb2dirent _dirEnt;
        private string _containingDir = null;

        protected Smb2Entry()
        {  }

        protected Smb2Entry(Smb2Share share, ref smb2dirent dirEnt, string containingDir = null, Smb2DirectoryEntry parentDir = null)
        {
            _dirEnt = dirEnt;
            _share = share ?? throw new ArgumentNullException(nameof(share));
            this._parentDirEntry = parentDir;
            this._containingDir = containingDir;

            this.SetDirDetails(ref dirEnt);
        }

        protected void SetDirDetails(ref smb2dirent dirEnt)
        {
            this.Name = dirEnt.name;
            this.Size = dirEnt.st.smb2_size;
            this.AcccessDtm = DateTimeOffset.FromUnixTimeSeconds((long)dirEnt.st.smb2_atime);
            this.ChangeDtm = DateTimeOffset.FromUnixTimeSeconds((long)dirEnt.st.smb2_ctime);
            this.CreateDtm = DateTimeOffset.FromUnixTimeSeconds((long)dirEnt.st.smb2_btime);
            this.ModifyDtm = DateTimeOffset.FromUnixTimeSeconds((long)dirEnt.st.smb2_mtime);
            this.Type = (Smb2EntryType)dirEnt.st.smb2_type;
        }

        public Smb2DirectoryEntry GetParentDirectory()
        {
            if (this._isRootShare || this.Directory == null)
                return null;

            if (this._parentDirEntry != null)
                return this._parentDirEntry;

            Smb2DirectoryEntry parentDirEntry = this._share.GetDirectory(this.Directory);

            this._parentDirEntry = parentDirEntry;

            return this._parentDirEntry;
        }
    }
}