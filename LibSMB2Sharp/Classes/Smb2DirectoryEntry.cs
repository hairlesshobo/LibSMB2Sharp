using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using LibSMB2Sharp.Exceptions;
using LibSMB2Sharp.Native;

namespace LibSMB2Sharp
{
    public class Smb2DirectoryEntry : Smb2Entry, IDisposable
    {
        private bool _removed = false;
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
            if (_removed)
                throw new LibSmb2DirectoryNotFoundException(this.RelativePath);

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

        public void Remove()
        {
            if (_removed)
                throw new LibSmb2DirectoryNotFoundException(this.RelativePath);

            // lets protect people from themselves
            if (this._isRootShare || this.RelativePath == "/" || this.Name == "")
                throw new LibSmb2OperationNotAllowedException("The root directory of a share cannot be removed");

            if (this.GetEntries().Any())
                throw new LibSmb2DirectoryNotEmptyException(this.RelativePath);

            int result = Methods.smb2_rmdir(_contextPtr, Helpers.CleanFilePathForNative(this.RelativePath));

            if (result < Const.EOK)
                throw new LibSmb2NativeMethodException(_contextPtr, result);
            
            _removed = true;
        }

        public Smb2DirectoryEntry CreateDirectory(string name)
        {
            if (_removed)
                throw new LibSmb2DirectoryNotFoundException(this.RelativePath);

            // TODO: impove name sanitization

            if (name.IndexOf('/') >= 0 || name.IndexOf('\\') >= 0)
                throw new LibSmb2InvalidDirectoryNameException(name);

            string dirNameRelative = $"{this.RelativePath}/{name}";
                
            int result = Methods.smb2_mkdir(_contextPtr, Helpers.CleanFilePathForNative(dirNameRelative));

            if (result < Const.EOK)
                throw new LibSmb2NativeMethodException(_contextPtr, result);
            
            return _share.GetDirectory(dirNameRelative);
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
        //         _contextPtr, 
        //         Helpers.CleanFilePathForNative(dirNameRelative), 
        //         Helpers.AsyncCallback(async (_) => tcs.TrySetResult(await _share.GetDirectoryAsync(dirNameRelative))),
        //         IntPtr.Zero
        //     );

        //     if (result < Const.EOK)
        //         throw new LibSmb2NativeMethodException(_contextPtr, result);
                
        //     return tcs.Task;
        // }

        public virtual void Dispose()
            => Close();

        private void Open()
            => Helpers.OpenDir(_contextPtr, ref _dirPtr, this.RelativePath);

        private void Close()
            => Helpers.CloseDir(_contextPtr, ref _dirPtr);

    }
}