using System;
using System.IO;
using System.Runtime.InteropServices;
using LibSMB2Sharp.Exceptions;
using LibSMB2Sharp.Interfaces;
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

        internal static ISmb2Entry GenerateEntry(IntPtr contextPtr, ref smb2dirent ent, Smb2Share share, string parentDir = "/")
        {
            Smb2Entry newEntry = new Smb2Entry()
            {
                Name = ent.name,
                Size = ent.st.smb2_size,
                ModifyDtm = DateTimeOffset.FromUnixTimeSeconds((long)ent.st.smb2_mtime),
                CreateDtm = DateTimeOffset.FromFileTime((long)ent.st.smb2_ctime),
                Type = (Smb2EntryType)ent.st.smb2_type,
                RelativePath = Path.Combine(parentDir, ent.name)
            };

            if (newEntry.Type == Smb2EntryType.Directory)
                return new Smb2DirectoryEntry(contextPtr, newEntry, share);
            else
                return newEntry;
        }

        // TODO: Add ability to strip leading server info, if present
        public static string CleanFilePath(string path)
            => path.Replace('\\', '/').Replace("//", "/").TrimStart('/');


        internal static smb2_stat_64? Stat(IntPtr contextPtr, string path)
        {
            IntPtr ptr = IntPtr.Zero;

            try
            {
                ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(smb2_stat_64)));

                int result = Methods.smb2_stat(contextPtr, CleanFilePath(path), ptr);

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