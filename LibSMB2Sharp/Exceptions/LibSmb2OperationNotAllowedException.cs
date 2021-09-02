using System;

namespace LibSMB2Sharp.Exceptions
{
    public class LibSmb2OperationNotAllowedException : Exception
    {
        public LibSmb2OperationNotAllowedException(string message) : base(message)
        { }
    }
}