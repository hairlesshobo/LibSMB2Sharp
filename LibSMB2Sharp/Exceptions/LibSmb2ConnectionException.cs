using System;
using LibSMB2Sharp.Native;

namespace LibSMB2Sharp.Exceptions
{
    public class LibSmb2ConnectionException : LibSmb2NativeMethodException
    {
        public LibSmb2ConnectionException(IntPtr smb2, int result) : base(smb2, result)
        { }
    }
}