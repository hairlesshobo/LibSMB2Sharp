using System;
using System.Collections.Generic;
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
                
                dirPtr = Methods.smb2_opendir(contextPtr, path);

                if (dirPtr == IntPtr.Zero)
                    throw new LibSmb2NativeMethodException(contextPtr);
            }
        }

        internal static IEnumerable<ISmb2Entry> GetDirEntries(IntPtr contextPtr, IntPtr dirPtr, Smb2Share share, bool ignoreSelfAndParent = true)
        {
            // Open();

            IntPtr entPtr = IntPtr.Zero;

            while ((entPtr = Methods.smb2_readdir(contextPtr, dirPtr)) != IntPtr.Zero)
            {
                smb2dirent ent = Marshal.PtrToStructure<smb2dirent>(entPtr);

                if (ignoreSelfAndParent && (ent.name == "." || ent.name == ".."))
                    continue;

                Smb2Entry newEntry = new Smb2Entry()
                {
                    Name = ent.name,
                    Size = ent.st.smb2_size,
                    ModifyDtm = DateTimeOffset.FromUnixTimeSeconds((long)ent.st.smb2_mtime),
                    CreateDtm = DateTimeOffset.FromFileTime((long)ent.st.smb2_ctime),
                    Type = (Smb2EntryType)ent.st.smb2_type,
                    RelativePath = Path.Combine("/", ent.name)
                };

                if (newEntry.Type == Smb2EntryType.Directory)
                    yield return new Smb2DirectoryEntry(contextPtr, newEntry, share);
                else
                    yield return newEntry;
            }

            // Close();
        }
    }
}