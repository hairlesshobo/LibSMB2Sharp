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

                Smb2FileEntry entry = share.GetFile(".///original_lists//studio_corruptions.txt");
                
                using (Smb2FileReader reader = entry.OpenReader())
                {
                    byte[] buff = new byte[2048];

                    int bytesRead = reader.Read(buff, 0, buff.Length);

                    string str = System.Text.Encoding.UTF8.GetString(buff, 0, bytesRead);

                    Console.WriteLine(str);
                }

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
