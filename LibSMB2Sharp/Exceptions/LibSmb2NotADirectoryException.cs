using System;

namespace LibSMB2Sharp.Exceptions
{
    public class LibSmb2NotADirectoryException : Exception
    {
        public LibSmb2NotADirectoryException(string name) : base($"The following entry is not a directory: {name}")
        { }
    }
}