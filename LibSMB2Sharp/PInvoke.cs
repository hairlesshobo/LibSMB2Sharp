using System;
using System.Runtime.InteropServices;

namespace LibSMB2Sharp
{
    public class Methods
    {
        [DllImport("libsmb2.so.3.0.0", SetLastError = true)]
        public static extern smb2_context smb2_init_context();
        
        [DllImport("libsmb2.so.3.0.0", SetLastError = true)]
        private static extern smb2_url smb2_parse_url(IntPtr smb2_ctx, string url);
        public static smb2_url smb2_parse_url(smb2_context smb2, string url)
        {
            IntPtr ptr = IntPtr.Zero;

            try
            {
                ptr = Marshal.AllocHGlobal(Marshal.SizeOf(smb2));

                Marshal.StructureToPtr(smb2, ptr, false);

                return smb2_parse_url(ptr, url);
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    Marshal.FreeHGlobal(ptr);
            }
        }
    }
}
