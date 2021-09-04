using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using LibSMB2Sharp.Native;
using LibSMB2Sharp.Exceptions;
using LibSMB2Sharp;
using System.Threading.Tasks;

namespace TestCLI
{
    class Program
    {
        static async Task Main(string[] args)
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

                foreach (Smb2Entry entry in share.GetEntries())
                {
                    Console.WriteLine(entry.RelativePath);
                }

                // share.CreateDirectoryTree("/test/another/test/for/steve");
                // Smb2DirectoryEntry dirEntry = share.GetDirectory("/test/");

                // dirEntry.Remove();


                // Smb2FileEntry entry = share.GetFile(".///original_lists//studio_corruptions.txt");
                
                

                // Console.WriteLine(JsonSerializer.Serialize(entry, new JsonSerializerOptions()
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
