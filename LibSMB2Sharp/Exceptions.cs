using System;

namespace LibSMB2Sharp.Exceptions
{
    public class LibSMB2SharpException : Exception
    {
        public LibSMB2SharpException(string message) : base(message)
        {

        }

        public LibSMB2SharpException(ref smb2_context smb2) : base(Methods.smb2_get_error(ref smb2))
        {

        }
    }
}