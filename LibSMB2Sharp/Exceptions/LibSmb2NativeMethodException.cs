using System;
using System.Text;
using LibSMB2Sharp.Native;

namespace LibSMB2Sharp.Exceptions
{
    public class LibSmb2NativeMethodException : Exception
    {
        public int ErrorCode { get; private set; } = Int32.MinValue;

        public LibSmb2NativeMethodException(string message) : base(message)
        { }

        public LibSmb2NativeMethodException(IntPtr smb2, string message) : base(message + " : " + Methods.smb2_get_error(smb2))
        { }

        public LibSmb2NativeMethodException(IntPtr smb2, int result) 
            : base(GetErrorMessage(smb2, result))
        { 
            this.ErrorCode = result;
        }

        private static string GetErrorMessage(IntPtr smb2, int result)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine($"libsmb2 native call failed with error code : {result} [{Helpers.GetSmb2ErrorMessage(result)}]");

            string nativeMessage = Methods.smb2_get_error(smb2);

            if (!string.IsNullOrWhiteSpace(nativeMessage))
                builder.AppendLine(nativeMessage);

            return builder.ToString();
        }
    }
}