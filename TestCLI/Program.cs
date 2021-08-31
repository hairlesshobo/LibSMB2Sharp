using System;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using LibSMB2Sharp;

namespace TestCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            //# TODO: Verify smb2_header 
            //# TODO: Verify smb2_pdu 
            //# TODO: Verify smb2dirent 
            //# TODO: Verify smb2_stat_64 
            //# TODO: Verify smb2_statvfs 
            //# TODO: Verify smb2_dirent_internal 
            //# TODO: Verify smb2dir 
            //# TODO: Verify smb2_url 
            //# TODO: Verify smb2_async
            //# TODO: Verify smb2_sync
            //# TODO: Verify smb2fh 
            //# TODO: Verify smb2_io_vectors 
            //# TODO: Verify smb2_iovec 

            // smb2_context ctx = Methods.smb2_init_context();
            
            smb2_context ctx = new smb2_context();
            smb2fh fh = new smb2fh();
            smb2dir dir = new smb2dir();
            smb2_dirent_internal dirent_internal = new smb2_dirent_internal();
            smb2_stat_64 stat_64 = new smb2_stat_64();
            smb2_statvfs statvfs = new smb2_statvfs();
            smb2dirent dirent = new smb2dirent();
            smb2_async async = new smb2_async();
            smb2_sync sync = new smb2_sync();
            smb2_header header = new smb2_header();
            // proto_id o_id = new proto_id();
            smb2_pdu pdu = new smb2_pdu();
            smb2_url url = new smb2_url();
            smb2_io_vectors vectors = new smb2_io_vectors();
            smb2_iovec iovec = new smb2_iovec();

            Console.WriteLine("             context size: " + Marshal.SizeOf(ctx));
            // Console.WriteLine("     smb2_io_vectors size: " + Marshal.SizeOf(vectors));
            // Console.WriteLine("          smb2_iovec size: " + Marshal.SizeOf(iovec));

            // Console.WriteLine("              smb2fh size: " + Marshal.SizeOf(fh));
            // Console.WriteLine("             smb2dir size: " + Marshal.SizeOf(dir));
            // Console.WriteLine("smb2_dirent_internal size: " + Marshal.SizeOf(dirent_internal));
            // Console.WriteLine("        smb2_stat_64 size: " + Marshal.SizeOf(stat_64));
            // Console.WriteLine("        smb2_statvfs size: " + Marshal.SizeOf(statvfs));
            // Console.WriteLine("          smb2dirent size: " + Marshal.SizeOf(dirent));
            // Console.WriteLine("          smb2_async size: " + Marshal.SizeOf(async));
            // Console.WriteLine("           smb2_sync size: " + Marshal.SizeOf(sync));
            // Console.WriteLine("         smb2_header size: " + Marshal.SizeOf(header));
            // Console.WriteLine("            proto_id size: " + Marshal.SizeOf(o_id));
            // Console.WriteLine("            smb2_pdu size: " + Marshal.SizeOf(pdu));
            // Console.WriteLine("            smb2_url size: " + Marshal.SizeOf(url));
            

            // smb2_url obj = Methods.smb2_parse_url(ctx, "smb://den;flip@nas.cz.foxhollow.cc/Series");

            // string json = JsonSerializer.Serialize(ctx, new JsonSerializerOptions()
            // {
            //     WriteIndented = true,
            //     IgnoreNullValues = false,
            //     IncludeFields = true
            // });

            // Console.WriteLine(json);
        }
    }
}
