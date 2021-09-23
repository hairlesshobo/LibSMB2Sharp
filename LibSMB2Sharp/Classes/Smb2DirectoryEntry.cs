/*
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using FoxHollow.LibSMB2Sharp.Exceptions;
using FoxHollow.LibSMB2Sharp.Native;

namespace FoxHollow.LibSMB2Sharp
{
    public class Smb2DirectoryEntry : Smb2Entry, IDisposable
    {
        protected IntPtr _dirPtr = IntPtr.Zero;

        protected Smb2DirectoryEntry()
            : base()
        { }

        internal Smb2DirectoryEntry(Smb2Share share, ref smb2dirent dirEnt, string containingDir = null, Smb2DirectoryEntry parentEntry = null)
            : base(share, ref dirEnt, containingDir, parentEntry)
        {
            this.Share.RegisterOpenDirectory(this);
        }

        public IEnumerable<Smb2Entry> GetEntries(bool ignoreSelfAndParent = true)
        {
            if (_removed)
                throw new LibSmb2DirectoryNotFoundException(this.RelativePath);

            Open();

            IntPtr entPtr = IntPtr.Zero;

            while ((entPtr = Methods.smb2_readdir(this.Context.Pointer, _dirPtr)) != IntPtr.Zero)
            {
                smb2dirent ent = Marshal.PtrToStructure<smb2dirent>(entPtr);

                if (ignoreSelfAndParent && (ent.name == "." || ent.name == ".."))
                    continue;

                yield return Helpers.GenerateEntry(this.Share, ref ent, parent: this);
            }

            Close();
        }

        public virtual Smb2FileEntry CreateFile(string name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            Helpers.SanitizeEntryName(name);

            Smb2Entry entry = this.GetEntry(name, false);

            Smb2FileEntry fileEntry = entry as Smb2FileEntry;

            if (entry != null && fileEntry == null)
                throw new LibSmb2NotAFileException(Path.Combine(this.RelativePath, name));

            this.CreateOrTruncateFile(name);

            fileEntry = new Smb2FileEntry(this.Share, this, name);

            return fileEntry;
        }

        public virtual Smb2FileEntry GetFile(string name, bool createNew = false)
        {
            Helpers.SanitizeEntryName(name);

            Smb2Entry entry = this.GetEntry(name, !createNew);

            Smb2FileEntry fileEntry = entry as Smb2FileEntry;

            // an entry exists, but it is not a file.. can't exactly use that
            if (entry != null && fileEntry == null)
                throw new LibSmb2NotAFileException(Path.Combine(this.RelativePath, name));

            if (fileEntry == null && createNew)
                fileEntry = this.CreateFile(name);

            return fileEntry;
        }

        public virtual Smb2DirectoryEntry GetDirectory(string name, bool throwOnMissing = true)
        {
            if (String.IsNullOrWhiteSpace(name))
                return this;

            Helpers.SanitizeEntryName(name);

            Smb2Entry entry = this.GetEntry(name, throwOnMissing);

            if (entry == null)
                return null;

            Smb2DirectoryEntry dirEntry = entry as Smb2DirectoryEntry;

            if (dirEntry == null)
                throw new LibSmb2NotADirectoryException(Path.Combine(this.RelativePath, name));

            return dirEntry;
        }

        public virtual Smb2Entry GetEntry(string name, bool throwOnMissing = true)
        {
            Helpers.SanitizeEntryName(name);
            
            smb2dirent? dirEntN = this.GetDirEnt(name, throwOnMissing);

            if (dirEntN == null)
                return null;

            smb2dirent dirEnt = dirEntN.Value;

            return Helpers.GenerateEntry(this.Share, ref dirEnt, parent: this);
        }

        public void Remove()
        {
            if (_removed)
                throw new LibSmb2DirectoryNotFoundException(this.RelativePath);

            // lets protect people from themselves
            if (this._isRootShare || this.RelativePath == "/" || this.Name == "")
                throw new LibSmb2OperationNotAllowedException("The root directory of a share cannot be removed");

            if (this.GetEntries().Any())
                throw new LibSmb2DirectoryNotEmptyException(this.RelativePath);

            int result = Methods.smb2_rmdir(this.Context.Pointer, Helpers.CleanFilePathForNative(this.RelativePath));

            if (result < Const.EOK)
                throw new LibSmb2NativeMethodException(this.Context, result);

            _removed = true;
        }

        public Smb2DirectoryEntry CreateDirectory(string name)
        {
            if (_removed)
                throw new LibSmb2DirectoryNotFoundException(this.RelativePath);

            Helpers.SanitizeEntryName(name);

            string dirNameRelative = Helpers.CleanFilePath($"{this.RelativePath}/{name}");

            int result = Methods.smb2_mkdir(this.Context.Pointer, Helpers.CleanFilePathForNative(dirNameRelative));

            if (result < Const.EOK)
                throw new LibSmb2NativeMethodException(this.Context, result);

            return _share.GetDirectory(dirNameRelative);
        }

        public void Move(string newPath)
        {
            if (_removed)
                throw new LibSmb2DirectoryNotFoundException(this.RelativePath);

            this.Close();

            base.Move(newPath, false);
        }

        public virtual void Dispose()
            => Close();

        internal void CreateOrTruncateFile(string name)
        {
            Helpers.SanitizeEntryName(name);

            string filePath = Helpers.CleanFilePathForNative(Path.Combine(this.RelativePath, name));

            IntPtr fhPtr = Methods.smb2_open(this.Context.Pointer, filePath, Const.O_WRONLY | Const.O_CREAT);
            Methods.smb2_ftruncate(this.Context.Pointer, fhPtr, 0);
            Methods.smb2_close(this.Context.Pointer, fhPtr);
        }

        internal virtual smb2dirent? GetDirEnt(string name, bool throwOnMissing = true)
        {
            Helpers.SanitizeEntryName(name);

            string path = Path.Combine(this.RelativePath, name);

            smb2_stat_64? stat = Helpers.Stat(this.Context, path);

            if (stat == null)
            {
                if (throwOnMissing)
                    throw new LibSmb2FileNotFoundException(path);
                else
                    return null;
            } 

            path = path.TrimEnd('/');

            smb2dirent dirEnt = new smb2dirent()
            {
                name = Path.GetFileName(path),
                st = stat.Value
            };

            return dirEnt;
        }

        private void Open()
            => Helpers.OpenDir(this.Context, ref _dirPtr, this.RelativePath);

        private void Close()
            => Helpers.CloseDir(this.Context, ref _dirPtr);

        //! DOES NOT WORK
        //! CAUSES ERROR "free(): double free detected in tcache 2" to be emitted when application closes
        // private Task<Smb2DirectoryEntry> CreateDirectoryAsync(string name)
        // {
        //     if (_removed)
        //         throw new LibSmb2DirectoryNotFoundException(this.RelativePath);

        //     Helpers.SanitizeEntryName(name);

        //     string dirNameRelative = $"{this.RelativePath}/{name}";

        //     TaskCompletionSource<Smb2DirectoryEntry> tcs = new TaskCompletionSource<Smb2DirectoryEntry>();

        //     int result = Methods.smb2_mkdir_async(
        //         this.Context.Pointer, 
        //         Helpers.CleanFilePathForNative(dirNameRelative), 
        //         Helpers.AsyncCallback(async (_) => tcs.TrySetResult(await _share.GetDirectoryAsync(dirNameRelative))),
        //         IntPtr.Zero
        //     );

        //     if (result < Const.EOK)
        //         throw new LibSmb2NativeMethodException(this.Context.Pointer, result);

        //     return tcs.Task;
        // }
    }
}