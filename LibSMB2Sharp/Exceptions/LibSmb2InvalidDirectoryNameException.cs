using System;

namespace LibSMB2Sharp.Exceptions
{
    public class LibSmb2InvalidDirectoryNameException : Exception
    {
        public LibSmb2InvalidDirectoryNameException(string name) : base("The provided directory name contains invalid characters")
        { }
    }
}