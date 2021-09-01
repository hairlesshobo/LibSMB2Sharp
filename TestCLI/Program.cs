using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using LibSMB2Sharp;
using LibSMB2Sharp.Exceptions;

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

            if (String.IsNullOrWhiteSpace(connString))
                throw new ArgumentNullException("connString");

            smb2_context smb2 = Methods.smb2_init_context() 
                ?? throw new LibSMB2SharpException("Failed to init context");

            smb2_url url = Methods.smb2_parse_url(ref smb2, connString)
                ?? throw new LibSMB2SharpException(ref smb2);

            if (!String.IsNullOrWhiteSpace(url.domain))
                Methods.smb2_set_domain(ref smb2, url.domain);

            if (!String.IsNullOrWhiteSpace(password))
                Methods.smb2_set_password(ref smb2, password);

            Methods.smb2_set_security_mode(ref smb2, Const.SMB2_NEGOTIATE_SIGNING_ENABLED);

            if (Methods.smb2_connect_share(ref smb2, url.server, url.share, url.user) < 0)
            {
                Console.WriteLine("ERROR: " + Methods.smb2_get_error(ref smb2));
                Environment.Exit(-1);
            }

            smb2dir dir = Methods.smb2_opendir(ref smb2, url.path)
                ?? throw new LibSMB2SharpException(ref smb2);

            smb2dirent? ent = null;

            while ((ent = Methods.smb2_readdir(ref smb2, ref dir)) != null)
            {
                Console.WriteLine(ent.Value.name);
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
