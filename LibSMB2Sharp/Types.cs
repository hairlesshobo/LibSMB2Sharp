using System;

namespace LibSMB2Sharp
{
    //typedef void (*smb2_command_cb)(struct smb2_context *smb2, int status, void *command_data, void *cb_data);
    // TODO: add command_data and cb_data
    public delegate void smb2_command_cb(smb2_context smb2, int status);
    
}