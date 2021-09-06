using System;

namespace LibSMB2Sharp.Exceptions
{
    public class LibSmb2FileAlreadyClosedException : Exception
    {
        public LibSmb2FileAlreadyClosedException(string filePath)
            : base($"The requested operation could not be completed because the file is already closed : {filePath}")
        {
            
        }
    }
}