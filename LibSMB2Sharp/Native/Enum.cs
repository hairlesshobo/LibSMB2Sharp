using System;

namespace LibSMB2Sharp.Native
{
    public enum smb2_sec 
    {
        SMB2_SEC_UNDEFINED = 0,
        SMB2_SEC_NTLMSSP,
        SMB2_SEC_KRB5,
    }

    public enum smb2_negotiate_version 
    {
        SMB2_VERSION_ANY  = 0,
        SMB2_VERSION_ANY2 = 2,
        SMB2_VERSION_ANY3 = 3,
        SMB2_VERSION_0202 = 0x0202,
        SMB2_VERSION_0210 = 0x0210,
        SMB2_VERSION_0300 = 0x0300,
        SMB2_VERSION_0302 = 0x0302
    }

    public enum smb2_recv_state 
    {
        SMB2_RECV_SPL = 0,
        SMB2_RECV_HEADER,
        SMB2_RECV_FIXED,
        SMB2_RECV_VARIABLE,
        SMB2_RECV_PAD,
    }
}