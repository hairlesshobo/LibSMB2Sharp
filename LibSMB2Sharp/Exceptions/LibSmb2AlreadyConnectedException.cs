using System;

namespace LibSMB2Sharp.Exceptions
{
    public class LibSmb2AlreadyConnectedException : Exception
    {
        public LibSmb2AlreadyConnectedException() : base("Unable to perform requested operation, the LibSmb2 context is already connected")
        { }
    }
}