/**
 *  LibSMB2Sharp - C# Bindings for the libsmb2 C library
 * 
 *  Copyright (c) 2021 Steve Cross <flip@foxhollow.cc>
 *
 *  This program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU Lesser General Public License as published by
 *  the Free Software Foundation; either version 2.1 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public License
 *  along with this program; if not, see <http://www.gnu.org/licenses/>.
 */

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

        public LibSmb2NativeMethodException(Smb2Context context, int result) 
            : base(GetErrorMessage(context, result))
        { 
            this.ErrorCode = result;
        }

        private static string GetErrorMessage(Smb2Context context, int result)
            => GetErrorMessage(context.Pointer, result);

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