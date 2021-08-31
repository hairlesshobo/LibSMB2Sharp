using System;
using System.Runtime.InteropServices;

namespace LibSMB2Sharp
{
    public class Methods
    {
        [DllImport("libsmb2.so.3.0.0", EntryPoint = "smb2_init_context", SetLastError = true)]
        private static extern IntPtr _smb2_init_context();
        public static smb2_context smb2_init_context()
        {
            IntPtr returnPtr = IntPtr.Zero;

            try
            {
                smb2_context returnData = new smb2_context();

                returnPtr = Marshal.AllocHGlobal(Marshal.SizeOf(returnData));

                returnPtr = _smb2_init_context();

                // returnData = (smb2_context)Marshal.PtrToStructure(returnPtr, typeof(smb2_context));

                return returnData;
            }
            finally
            {
                if (returnPtr != IntPtr.Zero)
                    Marshal.FreeHGlobal(returnPtr);
            }
        }
        
        [DllImport("libsmb2.so.3.0.0", EntryPoint = "smb2_parse_url", SetLastError = true)]
        private static extern IntPtr _smb2_parse_url(IntPtr smb2_ctx, string url);
        public static smb2_url smb2_parse_url(smb2_context smb2, string url)
        {
            IntPtr ptr = IntPtr.Zero;
            IntPtr returnPtr = IntPtr.Zero;

            try
            {
                smb2_url returnData = new smb2_url();

                returnPtr = Marshal.AllocHGlobal(Marshal.SizeOf(returnData));
                ptr = Marshal.AllocHGlobal(Marshal.SizeOf(smb2));
                

                Marshal.StructureToPtr(smb2, ptr, false);

                returnPtr = _smb2_parse_url(ptr, url);

                returnData = (smb2_url)Marshal.PtrToStructure(returnPtr, typeof(smb2_url));

                return returnData;
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    Marshal.FreeHGlobal(ptr);

                if (returnPtr != IntPtr.Zero)
                    Marshal.FreeHGlobal(returnPtr);
            }
        }
    }
}
