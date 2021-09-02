using System;
using System.IO;
using System.Runtime.InteropServices;
using LibSMB2Sharp.Exceptions;
using LibSMB2Sharp.Native;

namespace LibSMB2Sharp
{
    internal class Helpers
    {
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
                    throw new LibSmb2NativeMethodException(contextPtr);
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
        public static string CleanFilePathForNative(string path)
            => CleanFilePath(path).TrimStart('/');

        public static string CleanFilePath(string path)
            => path.Replace('\\', '/').Replace("//", "/").TrimStart('.');


        internal static smb2_stat_64? Stat(IntPtr contextPtr, string path)
        {
            IntPtr ptr = IntPtr.Zero;

            try
            {
                ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(smb2_stat_64)));

                int result = Methods.smb2_stat(contextPtr, CleanFilePathForNative(path), ptr);

                if (result == -2)
                    return null;

                if (ptr == IntPtr.Zero || result < 0)
                    throw new LibSmb2NativeMethodException(contextPtr);

                return Marshal.PtrToStructure<smb2_stat_64>(ptr);
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    Marshal.FreeHGlobal(ptr);
            }
        }
    }
}