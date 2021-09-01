using System;
using System.Runtime.InteropServices;

namespace LibSMB2Sharp
{
    public class Methods
    {
        [DllImport("libsmb2.so.3.0.0", EntryPoint = "smb2_init_context", SetLastError = true)]
        private static extern IntPtr _smb2_init_context();
        public static smb2_context? smb2_init_context()
        {
            IntPtr returnPtr = IntPtr.Zero;

            try
            {
                returnPtr = Marshal.AllocHGlobal(Marshal.SizeOf(new smb2_context()));

                returnPtr = _smb2_init_context();

                if (returnPtr == IntPtr.Zero)
                    return null;

                return Marshal.PtrToStructure<smb2_context>(returnPtr);
            }
            finally
            {
                if (returnPtr != IntPtr.Zero)
                    Marshal.FreeHGlobal(returnPtr);
            }
        }
        

        [DllImport("libsmb2.so.3.0.0", EntryPoint = "smb2_parse_url", SetLastError = true)]
        private static extern IntPtr _smb2_parse_url([In, Out] ref smb2_context smb2, string url);
        public static smb2_url? smb2_parse_url(ref smb2_context smb2, string url)
        {
            IntPtr returnPtr = IntPtr.Zero;

            try
            {
                returnPtr = Marshal.AllocHGlobal(Marshal.SizeOf(new smb2_url()));

                returnPtr = _smb2_parse_url(ref smb2, url);

                if (returnPtr == IntPtr.Zero)
                    return null;

                return Marshal.PtrToStructure<smb2_url>(returnPtr);
            }
            finally
            {
                if (returnPtr != IntPtr.Zero)
                    Marshal.FreeHGlobal(returnPtr);
            }
        }


        [DllImport("libsmb2.so.3.0.0", SetLastError = true)]
        public static extern void smb2_set_domain([In, Out] ref smb2_context smb2, string domain);

        
        [DllImport("libsmb2.so.3.0.0", SetLastError = true)]
        public static extern void smb2_set_workstation([In, Out] ref smb2_context smb2, string workstation);
        
        
        [DllImport("libsmb2.so.3.0.0", SetLastError = true)]
        public static extern void smb2_set_password([In, Out] ref smb2_context smb2, string workstation);

        
        [DllImport("libsmb2.so.3.0.0", SetLastError = true)]
        public static extern void smb2_set_security_mode([In, Out] ref smb2_context smb2, UInt16 security_mode);

        
        [DllImport("libsmb2.so.3.0.0", SetLastError = true)]
        public static extern int smb2_connect_share([In, Out] ref smb2_context smb2, string server, string share, string user);

        
        [DllImport("libsmb2.so.3.0.0", EntryPoint = "smb2_opendir", SetLastError = true)]
        private static extern IntPtr _smb2_opendir([In, Out] ref smb2_context smb2, string path);
        public static smb2dir? smb2_opendir(ref smb2_context smb2, string path)
        {
            IntPtr returnPtr = IntPtr.Zero;

            try
            {
                returnPtr = Marshal.AllocHGlobal(Marshal.SizeOf(new smb2dir()));

                returnPtr = _smb2_opendir(ref smb2, path);

                if (returnPtr == IntPtr.Zero)
                    return null;

                return Marshal.PtrToStructure<smb2dir>(returnPtr);
            }
            finally
            {
                if (returnPtr != IntPtr.Zero)
                    Marshal.FreeHGlobal(returnPtr);
            }
        }


        [DllImport("libsmb2.so.3.0.0", EntryPoint = "smb2_readdir", SetLastError = true)]
        private static extern IntPtr _smb2_readdir([In, Out] ref smb2_context smb2, [In, Out] ref smb2dir dir);
        public static smb2dirent? smb2_readdir(ref smb2_context smb2, ref smb2dir dir)
        {
            IntPtr ptr = _smb2_readdir(ref smb2, ref dir);

            if (ptr == IntPtr.Zero)
                return null;

            return Marshal.PtrToStructure<smb2dirent>(ptr);
        }
        


        public static string smb2_get_error([In] ref smb2_context smb2)
            => new string(smb2.error_string, 0, Array.IndexOf(smb2.error_string, (char)0));
    }
}
