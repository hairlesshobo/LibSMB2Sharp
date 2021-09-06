using System;

namespace LibSMB2Sharp.Exceptions
{
    public class LibSmb2FileLockedException : Exception
    {
        public LibSmb2FileLockedException(string filePath) : base($"Unable to perform the requested operation because the file is currently locked : {filePath}")
        { }
    }
}