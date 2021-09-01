using System;
using LibSMB2Sharp.Native;

namespace LibSMB2Sharp.Exceptions
{
    public class LibSmb2ConnectionException : Exception
    {
        public LibSmb2ConnectionException(IntPtr smb2) : base(Methods.smb2_get_error(smb2))
        { }
    }
}