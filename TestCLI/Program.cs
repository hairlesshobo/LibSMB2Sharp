using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
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

                Smb2FileEntry fileEntry = share.GetFile("/meow/doc2.txt", true);

                using (Smb2FileWriter writer = fileEntry.OpenWriter())
                {
                    // writer.WriteLine("this is a test of the emergency broadcast system");
                    writer.WriteLine("come on, does this actually work??");
                }
                

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
