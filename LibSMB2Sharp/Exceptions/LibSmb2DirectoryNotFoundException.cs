using System;

namespace LibSMB2Sharp.Exceptions
{
    public class LibSmb2DirectoryNotFoundException : Exception
    {
        public LibSmb2DirectoryNotFoundException(string fileName) : base($"The following directory does not exist or was already removed: {fileName}")
        { }
    }
}