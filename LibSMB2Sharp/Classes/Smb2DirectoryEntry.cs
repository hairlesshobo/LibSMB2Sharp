using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using LibSMB2Sharp.Exceptions;
using LibSMB2Sharp.Native;

namespace LibSMB2Sharp
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

            // TODO: impove name sanitization

            if (name.IndexOf('/') >= 0 || name.IndexOf('\\') >= 0)
                throw new LibSmb2InvalidDirectoryNameException(name);

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

        //! DOES NOT WORK
        //! CAUSES ERROR "free(): double free detected in tcache 2" to be emitted when application closes
        // private Task<Smb2DirectoryEntry> CreateDirectoryAsync(string name)
        // {
        //     if (_removed)
        //         throw new LibSmb2DirectoryNotFoundException(this.RelativePath);

        //     // TODO: impove name sanitization

        //     if (name.IndexOf('/') >= 0 || name.IndexOf('\\') >= 0)
        //         throw new LibSmb2InvalidDirectoryNameException(name);

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

        public virtual void Dispose()
            => Close();

        private void Open()
            => Helpers.OpenDir(this.Context, ref _dirPtr, this.RelativePath);

        private void Close()
            => Helpers.CloseDir(this.Context, ref _dirPtr);

    }
}