using System;

namespace LibSMB2Sharp.Exceptions
{
    public class LibSmb2NotAFileException : Exception
    {
        public LibSmb2NotAFileException(string name) : base($"The following entry is not a file: {name}")
        { }
    }
}