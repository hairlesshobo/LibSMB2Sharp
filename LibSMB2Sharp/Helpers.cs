using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using LibSMB2Sharp.Exceptions;
using LibSMB2Sharp.Native;

namespace LibSMB2Sharp
{
    internal class Helpers
    {
        // internal delegate void ActionRef<T>(int result, ref T item) where T : struct;

        internal static smb2_command_cb AsyncCallback(Action<int> callback)
        {
            smb2_command_cb asyncCallback = (smb2, result, command_data, cb_data) =>
            {
                if (result < Const.EOK)
                    throw new LibSmb2NativeMethodException(smb2, result);

                callback(result);
            };

            return asyncCallback;
        }

        // internal static smb2_command_cb AsyncCallback<TResult>(ActionRef<TResult> callback)
        //     where TResult : struct
        // {
        //     smb2_command_cb asyncCallback = (smb2, result, command_data, cb_data) =>
        //     {
        //         if (result < Const.EOK)
        //             throw new LibSmb2NativeMethodException(smb2, result);

        //         TResult resultStruct = Marshal.PtrToStructure<TResult>(command_data);

        //         callback(result, ref resultStruct);
        //     };

        //     return asyncCallback;
        // }

        // internal static smb2_command_cb AsyncCallbackRaw(Action<int, IntPtr> callback)
        // {
        //     smb2_command_cb asyncCallback = (smb2, result, command_data, cb_data) => callback(result, command_data);

        //     return asyncCallback;
        // }


        internal static void CloseDir(IntPtr contextPtr, ref IntPtr dirPtr)
        {
            if (dirPtr != IntPtr.Zero)
            {
                Methods.smb2_closedir(contextPtr, dirPtr);
                dirPtr = IntPtr.Zero;
            }
        }

        internal static void OpenDir(IntPtr contextPtr, ref IntPtr dirPtr, string path = null)
        {
            if (dirPtr == IntPtr.Zero)
            {
                if (path == null)
                    path = String.Empty;

                path = path.TrimStart('/');
                
                dirPtr = Methods.smb2_opendir(contextPtr, path);

                if (dirPtr == IntPtr.Zero)
                    throw new LibSmb2NativeMethodException(contextPtr, "Failed to open directory");
            }
        }

        internal static Smb2Entry GenerateEntry(IntPtr contextPtr, ref smb2dirent dirEnt, Smb2Share share, string containingDir = null, Smb2DirectoryEntry parent = null)
        {
            if (dirEnt.st.smb2_type == Const.SMB2_TYPE_DIRECTORY)
                return new Smb2DirectoryEntry(contextPtr, share, ref dirEnt, containingDir, parent);
            else
                return new Smb2FileEntry(contextPtr, share, ref dirEnt, containingDir, parent);
        }

        // TODO: Add ability to strip leading server info, if present
        internal static string CleanFilePathForNative(string path)
            => CleanFilePath(path).TrimStart('/');

        internal static string CleanFilePath(string path)
            => path.Replace('\\', '/').Replace("//", "/").TrimStart('.');


        internal static smb2_stat_64? Stat(IntPtr contextPtr, string path)
        {
            IntPtr ptr = IntPtr.Zero;

            try
            {
                ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(smb2_stat_64)));

                int result = Methods.smb2_stat(contextPtr, CleanFilePathForNative(path), ptr);

                if (result == -Const.ENOENT)
                    return null;

                if (ptr == IntPtr.Zero || result < Const.EOK)
                    throw new LibSmb2NativeMethodException(contextPtr, result);

                return Marshal.PtrToStructure<smb2_stat_64>(ptr);
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    Marshal.FreeHGlobal(ptr);
            }
        }

        //! DOES NOT WORK
        //! causes error "free(): double free detected in tcache 2" to be emitted on program termination
        // private static Task<Nullable<smb2_stat_64>> StatAsync(IntPtr contextPtr, string path)
        // {
        //     TaskCompletionSource<Nullable<smb2_stat_64>> tcs = new TaskCompletionSource<Nullable<smb2_stat_64>>();

        //     IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(smb2_stat_64)));

        //     int result = Methods.smb2_stat_async(
        //         contextPtr, 
        //         Helpers.CleanFilePathForNative(path), 
        //         ptr, 
        //         Helpers.AsyncCallbackRaw((cbResult, dataPtr) => {
        //             if (cbResult == -Const.ENOENT)
        //                 tcs.TrySetResult(null);

        //             if (dataPtr == IntPtr.Zero || cbResult < Const.EOK)
        //                 throw new LibSmb2NativeMethodException(contextPtr, cbResult);

        //             smb2_stat_64 statResult = Marshal.PtrToStructure<smb2_stat_64>(dataPtr);

        //             tcs.TrySetResult(null);

        //             if (ptr != IntPtr.Zero)
        //                 Marshal.FreeHGlobal(ptr);

        //             tcs.TrySetResult(statResult);
        //         }),
        //         IntPtr.Zero
        //     );

        //     if (result < Const.EOK)
        //         throw new LibSmb2NativeMethodException(contextPtr, result);

        //     return tcs.Task;
        // }

        internal static string GetSmb2ErrorMessage(int errno)
        {
            if (errno == -Const.EACCES) return "Permission denied";
            if (errno == -Const.EAGAIN) return "Try again";
            if (errno == -Const.EBADF) return "Bad file number";
            if (errno == -Const.EBUSY) return "Device or resource busy";
            if (errno == -Const.ECONNREFUSED) return "Connection refused";
            if (errno == -Const.ECONNRESET) return "Connection reset by peer";
            if (errno == -Const.EDEADLK) return "Resource deadlock would occur";
            if (errno == -Const.EEXIST) return "File exists";
            if (errno == -Const.EINVAL) return "Invalid argument";
            if (errno == -Const.EIO) return "I/O error";
            if (errno == -Const.EMFILE) return "Too many open files";
            if (errno == -Const.ENETRESET) return "Network dropped connection because of reset";
            if (errno == -Const.ENODATA) return "No data available";
            if (errno == -Const.ENODEV) return "No such device";
            if (errno == -Const.ENOENT) return "No such file or directory";
            if (errno == -Const.ENOEXEC) return "Exec format error";
            if (errno == -Const.ENOMEM) return "Out of memory";
            if (errno == -Const.ENOSPC) return "No space left on device";
            if (errno == -Const.ENOTDIR) return "Not a directory";
            if (errno == -Const.EPERM) return "Operation not permitted";
            if (errno == -Const.EPIPE) return "Broken pipe";
            if (errno == -Const.EROFS) return "Read-only file system";
            if (errno == -Const.ETIMEDOUT) return "Connection timed out";
            if (errno == -Const.ETXTBSY) return "Text file busy";
            if (errno == -Const.EXDEV) return "Cross-device link";

            return "Unknown";
        }
    }
}