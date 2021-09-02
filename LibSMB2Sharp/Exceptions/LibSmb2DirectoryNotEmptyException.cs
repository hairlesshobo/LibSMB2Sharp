using System;

namespace LibSMB2Sharp.Exceptions
{
    public class LibSmb2DirectoryNotEmptyException : Exception
    {
        public LibSmb2DirectoryNotEmptyException(string directory) : base($"Cannot remove directory because it is not empty: {directory}")
        { }
    }
}