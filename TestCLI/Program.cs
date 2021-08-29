using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using LibSMB2Sharp;

namespace TestCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            smb2_context ctx = Methods.smb2_init_context();

            smb2_url obj = Methods.smb2_parse_url(ctx, "smb://den;flip@nas.cz.foxhollow.cc/Series");

            string json = JsonSerializer.Serialize(obj, new JsonSerializerOptions()
            {
                WriteIndented = true,
                IgnoreNullValues = false,
                IncludeFields = true
            });

            Console.WriteLine(json);
        }
    }
}
