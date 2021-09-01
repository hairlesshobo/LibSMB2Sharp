using System;
using LibSMB2Sharp.Native;

namespace LibSMB2Sharp.Exceptions
{
    public class LibSmb2NativeMethodException : Exception
    {
        public LibSmb2NativeMethodException(string message) : base(message)
        {

        }

        public LibSmb2NativeMethodException(IntPtr smb2) : base(Methods.smb2_get_error(smb2))
        {

        }
    }
}