using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using LibSMB2Sharp.Native;
using LibSMB2Sharp.Exceptions;
using LibSMB2Sharp;

namespace TestCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            string connString = String.Empty;
            string password = String.Empty;

            string devFile = Path.Combine(Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName, "dev_connect_string.txt");

            if (File.Exists(devFile))
            {
                string[] lines = File.ReadAllLines(devFile);
                connString = lines[0];
                password = lines[1];
            }

            using (Smb2Context smb2 = new Smb2Context(connectionString: connString, password: password))
            {
                Smb2Share share = smb2.OpenShare();

                // foreach (var entry in share.Entries)
                // {
                //     string name = entry.Name;

                //     if (entry.Type == Smb2EntryType.Directory)
                //     {
                //         name += "/";
                //     }

                //     Console.WriteLine("| " + name.PadRight(60) + entry.Size.ToString().PadLeft(14) + entry.ModifyDtm.ToString().PadLeft(30));

                //     Smb2DirectoryEntry dirEntry = entry as Smb2DirectoryEntry;

                //     if (dirEntry != null)
                //     {
                //         foreach (var subentry in dirEntry.Entries)
                //         {
                //             string subname = subentry.Name;

                //             if (subentry.Type == Smb2EntryType.Directory)
                //                 subname += "/";

                //             Console.WriteLine("|---- " + subname.PadRight(56) + subentry.Size.ToString().PadLeft(14) + subentry.ModifyDtm.ToString().PadLeft(30));
                //         }
                //     }
                // }

                share.GetFile("/original_lists/studio_corruptions47.txt");

                // Console.WriteLine(JsonSerializer.Serialize(share.Entries, new JsonSerializerOptions()
                // {
                //     WriteIndented = true,
                //     IgnoreNullValues = false,
                //     IncludeFields = true
                // }));

                // Smb2FileEntry entry = smb2.OpenFile("/screenrc");

            }


            // Console.WriteLine(JsonSerializer.Serialize(ent, new JsonSerializerOptions()
            // {
            //     WriteIndented = true,
            //     IgnoreNullValues = false,
            //     IncludeFields = true
            // }));
        }
    }
}
