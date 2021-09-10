/**
 *  LibSMB2Sharp - C# Bindings for the libsmb2 C library
 * 
 *  Copyright (c) 2021 Steve Cross <flip@foxhollow.cc>
 *
 *  This program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU Lesser General Public License as published by
 *  the Free Software Foundation; either version 2.1 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public License
 *  along with this program; if not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.IO;
using LibSMB2Sharp.Exceptions;
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
        private smb2dirent _dirEnt;

        protected bool _removed = false;
        protected Smb2DirectoryEntry _parentDirEntry = null;
        protected string _containingDir = null;

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

        protected Smb2Entry()
        {  }

        protected Smb2Entry(Smb2Share share, ref smb2dirent dirEnt, string containingDir = null, Smb2DirectoryEntry parentDir = null)
            : this(share, containingDir, parentDir)
        {
            this.SetDetailsFromDirEnt(ref dirEnt);
        }

        protected Smb2Entry(Smb2Share share, string containingDir = null, Smb2DirectoryEntry parentDir = null)
        {
            _share = share ?? throw new ArgumentNullException(nameof(share));
            this._parentDirEntry = parentDir;
            this._containingDir = containingDir;
        }

        protected void SetDetailsFromDirEnt(ref smb2dirent dirEnt)
        {
            _dirEnt = dirEnt;

            this.Name = _dirEnt.name;
            this.Size = _dirEnt.st.smb2_size;
            this.AcccessDtm = DateTimeOffset.FromUnixTimeSeconds((long)_dirEnt.st.smb2_atime);
            this.ChangeDtm = DateTimeOffset.FromUnixTimeSeconds((long)_dirEnt.st.smb2_ctime);
            this.CreateDtm = DateTimeOffset.FromUnixTimeSeconds((long)_dirEnt.st.smb2_btime);
            this.ModifyDtm = DateTimeOffset.FromUnixTimeSeconds((long)_dirEnt.st.smb2_mtime);
            this.Type = (Smb2EntryType)_dirEnt.st.smb2_type;
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

        public void RefreshDetails()
        {
            smb2dirent dirEnt = this.Share.GetDirEnt(this.RelativePath, true) ?? throw new Exception();

            this.SetDetailsFromDirEnt(ref dirEnt);
        }

        protected void Move(string newPath, bool movingFile, bool isRename = false)
        {
            newPath = Helpers.CleanFilePath(newPath).TrimEnd('/');

            smb2_stat_64? statResult = Helpers.Stat(this.Context, newPath);

            if (statResult != null)
            {
                if (movingFile == false || statResult.Value.smb2_type == Const.SMB2_TYPE_FILE)
                    throw new LibSmb2OperationNotAllowedException($"Cannot move directory, destination path already exists: {newPath}");
            }

            // we are moving to a directory, so we need to append the file name to the new path
            if (statResult != null && statResult.Value.smb2_type == Const.SMB2_TYPE_DIRECTORY)
                newPath += "/" + this.Name;

            string newParentDirPath = "/";

            if (newPath.LastIndexOf('/') > 0)
                newParentDirPath = newPath.Substring(0, newPath.LastIndexOf('/'));

            Smb2DirectoryEntry newParentDirEntry = this.Share.CreateDirectoryTree(newParentDirPath);

            int result = Methods.smb2_rename(
                this.Share.Context.Pointer, 
                Helpers.CleanFilePathForNative(this.RelativePath), 
                newPath
            );
            
            if (result < 0)
                throw new LibSmb2NativeMethodException(this.Context, result);

            if (!isRename)
            {
                smb2dirent newDirEnt = Share.GetDirEnt(newPath, true) ?? throw new Exception();

                this._parentDirEntry = newParentDirEntry;
                this._containingDir = newParentDirPath;
                this.SetDetailsFromDirEnt(ref newDirEnt);
            }
        }

        public void Rename(string newName)
        {
            Helpers.SanitizeEntryName(newName);

            string newPath = Helpers.CleanFilePath($"{this.Directory}/{newName}");

            this.Move(newPath, false, true);
        }

        public override string ToString()
            => (this.Type == Smb2EntryType.File ? "File" : "Directory") + " : " + this.RelativePath;
    }
}