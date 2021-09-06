using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
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

                // share.CreateDirectoryTree("/test/another/test/for/steve");
                // Smb2DirectoryEntry dirEntry = share.GetDirectory("/meow/");
                Smb2FileEntry fileEntry = share.GetFile("/meow/doc.txt");

                fileEntry.Move("/this/is/a/test/meow3");

                // dirEntry.Move("/this/is/a/test/meow3");

                // dirEntry.Remove();


                // Smb2FileEntry entry = share.GetFile(".///original_lists//studio_corruptions.txt");
                
                

                Console.WriteLine(JsonSerializer.Serialize(fileEntry, new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    IgnoreNullValues = false,
                    // IncludeFields = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                }));

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
