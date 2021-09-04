using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace LibSMB2Sharp.Native
{
    public delegate void smb2_command_cb(IntPtr smb2, int status, IntPtr command_data, IntPtr cb_data);
    
}