using System;
using LibSMB2Sharp.Native;

namespace LibSMB2Sharp.Exceptions
{
    public class LibSmb2ConnectionException : LibSmb2NativeMethodException
    {
        public LibSmb2ConnectionException(Smb2Context context, int result) : base(context, result)
        { }
    }
}