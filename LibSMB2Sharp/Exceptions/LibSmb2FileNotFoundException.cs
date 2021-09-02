using System;

namespace LibSMB2Sharp.Exceptions
{
    public class LibSmb2FileNotFoundException : Exception
    {
        public LibSmb2FileNotFoundException(string fileName) : base($"The following file does not exist: {fileName}")
        { }
    }
}