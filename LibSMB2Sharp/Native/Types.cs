using System;
using System.Text.Json.Serialization;

namespace LibSMB2Sharp.Native
{
    //typedef void (*smb2_command_cb)(struct smb2_context *smb2, int status, void *command_data, void *cb_data);
    // TODO: add command_data and cb_data
    public delegate void smb2_command_cb(IntPtr smb2, int status, IntPtr command_data, IntPtr cb_data);
    
}