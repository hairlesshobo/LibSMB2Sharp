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
using System.Runtime.InteropServices;
using FoxHollow.LibSMB2Sharp.Exceptions;
using FoxHollow.LibSMB2Sharp.Native;

namespace FoxHollow.LibSMB2Sharp
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


        internal static void CloseDir(Smb2Context context, ref IntPtr dirPtr)
        {
            if (dirPtr != IntPtr.Zero)
            {
                Methods.smb2_closedir(context.Pointer, dirPtr);
                dirPtr = IntPtr.Zero;
            }
        }

        internal static void OpenDir(Smb2Context context, ref IntPtr dirPtr, string path = null)
        {
            if (dirPtr == IntPtr.Zero)
            {
                if (path == null)
                    path = String.Empty;

                path = path.TrimStart('/');
                
                dirPtr = Methods.smb2_opendir(context.Pointer, path);

                if (dirPtr == IntPtr.Zero)
                    throw new LibSmb2NativeMethodException(context.Pointer, "Failed to open directory");
            }
        }

        internal static Smb2Entry GenerateEntry(Smb2Share share, ref smb2dirent dirEnt, string containingDir = null, Smb2DirectoryEntry parent = null)
        {
            if (dirEnt.st.smb2_type == Const.SMB2_TYPE_DIRECTORY)
                return new Smb2DirectoryEntry(share, ref dirEnt, containingDir, parent);
            else
                return new Smb2FileEntry(share, ref dirEnt, containingDir, parent);
        }

        // TODO: Add ability to strip leading server info, if present
        internal static string CleanFilePathForNative(string path)
            => CleanFilePath(path).TrimStart('/');

        internal static string CleanFilePath(string path)
            => $"/{path.Trim().Replace('\\', '/').Replace("//", "/").Trim('/').TrimStart('.')}";

        internal static void SanitizeEntryName(string name)
        {
            // TODO: impove name sanitization
            
            if (name.IndexOf('/') >= 0 || name.IndexOf('\\') >= 0)
                throw new LibSmb2InvalidEntryNameException(name);
        }

        internal static string GetDirectoryPath(string fullPath)
        {
            fullPath = CleanFilePath(fullPath);

            if (fullPath.LastIndexOf('/') == 0)
                return "/";

            return fullPath.Substring(0, fullPath.LastIndexOf('/'));
        }


        internal static smb2_stat_64? Stat(Smb2Context context, string path)
        {
            IntPtr ptr = IntPtr.Zero;

            try
            {
                ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(smb2_stat_64)));

                int result = Methods.smb2_stat(context.Pointer, CleanFilePathForNative(path), ptr);

                if (result == -Const.ENOENT)
                    return null;

                if (ptr == IntPtr.Zero || result < Const.EOK)
                    throw new LibSmb2NativeMethodException(context, result);

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
        // private static Task<Nullable<smb2_stat_64>> StatAsync(Smb2Context context, string path)
        // {
        //     TaskCompletionSource<Nullable<smb2_stat_64>> tcs = new TaskCompletionSource<Nullable<smb2_stat_64>>();

        //     IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(smb2_stat_64)));

        //     int result = Methods.smb2_stat_async(
        //         Context.Pointer, 
        //         Helpers.CleanFilePathForNative(path), 
        //         ptr, 
        //         Helpers.AsyncCallbackRaw((cbResult, dataPtr) => {
        //             if (cbResult == -Const.ENOENT)
        //                 tcs.TrySetResult(null);

        //             if (dataPtr == IntPtr.Zero || cbResult < Const.EOK)
        //                 throw new LibSmb2NativeMethodException(Context.Pointer, cbResult);

        //             smb2_stat_64 statResult = Marshal.PtrToStructure<smb2_stat_64>(dataPtr);

        //             tcs.TrySetResult(null);

        //             if (ptr != IntPtr.Zero)
        //                 Marshal.FreeHGlobal(ptr);

        //             tcs.TrySetResult(statResult);
        //         }),
        //         IntPtr.Zero
        //     );

        //     if (result < Const.EOK)
        //         throw new LibSmb2NativeMethodException(Context.Pointer, result);

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