using System;

namespace LibSMB2Sharp.Exceptions
{
    public class LibSmb2InvalidEntryNameException : Exception
    {
        public LibSmb2InvalidEntryNameException(string name) : base($"The provided entry name contains invalid characters : {name}")
        { }
    }
}