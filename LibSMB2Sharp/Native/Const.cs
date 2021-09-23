/*
 *  LibSMB2Sharp - C# Bindings for the libsmb2 C library
 * 
 *  Copyright (c) 2021 Steve Cross <flip@foxhollow.cc>
 *
 *  Parts of this code are borrowed from the libsmb2 project. Those parts are 
 *  Copyright (C) 2016 by Ronnie Sahlberg <ronniesahlberg@gmail.com>
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

namespace FoxHollow.LibSMB2Sharp.Native
{
    public static class Const
    {
        public const int MAX_ERROR_SIZE = 256;

        public const int SMB2_SPL_SIZE = 4;
        public const int SMB2_HEADER_SIZE = 64;

        public const int SMB2_SIGNATURE_SIZE = 16;
        public const int SMB2_KEY_SIZE = 16;

        public const int SMB2_MAX_VECTORS = 256;

        public const int SMB2_FD_SIZE = 16;

        /*
         * SMB2 NEGOTIATE
         */
        public const UInt16 SMB2_NEGOTIATE_SIGNING_ENABLED = 0x0001;
        public const UInt16 SMB2_NEGOTIATE_SIGNING_REQUIRED = 0x0002;

        /*
         * File Types
         */
        public const UInt32 SMB2_TYPE_FILE = 0x00000000;
        public const UInt32 SMB2_TYPE_DIRECTORY = 0x00000001;

        /*
         * Set which version of SMB to negotiate.
         * Default is to let the server pick the version.
         */
        public enum smb2_negotiate_version 
        {
            SMB2_VERSION_ANY  = 0,
            SMB2_VERSION_ANY2 = 2,
            SMB2_VERSION_ANY3 = 3,
            SMB2_VERSION_0202 = 0x0202,
            SMB2_VERSION_0210 = 0x0210,
            SMB2_VERSION_0300 = 0x0300,
            SMB2_VERSION_0302 = 0x0302,
            SMB2_VERSION_0311 = 0x0311
        };

        /*
         * File open flags
         */
        public const int O_RDONLY = 00;
        public const int O_WRONLY = 01;
        public const int O_RDWR = 02;
        public const int O_CREAT = 0100;
        public const int O_EXCL = 0200;
        public const int O_SYNC = 04010000;

        /// <summary>No Error Occurred</summary>
        public const int EOK = 0;

        /// <summary>Operation not permitted</summary>
        public const int EPERM = 1;

        /// <summary>No such file or directory</summary>
        public const int ENOENT = 2;

        /// <summary>I/O error</summary>
        public const int EIO = 5;

        /// <summary>Exec format error</summary>
        public const int ENOEXEC = 8;

        /// <summary>Bad file number</summary>
        public const int EBADF = 9;

        /// <summary>Try again</summary>
        public const int EAGAIN = 11;

        /// <summary>Out of memory</summary>
        public const int ENOMEM = 12;

        /// <summary>Permission denied</summary>
        public const int EACCES = 13;

        /// <summary>Device or resource busy</summary>
        public const int EBUSY = 16;

        /// <summary>File exists</summary>
        public const int EEXIST = 17;

        /// <summary>Cross-device link</summary>
        public const int EXDEV = 18;

        /// <summary>No such device</summary>
        public const int ENODEV = 19;

        /// <summary>Not a directory</summary>
        public const int ENOTDIR = 20;

        /// <summary>Invalid argument</summary>
        public const int EINVAL = 22;

        /// <summary>Too many open files</summary>
        public const int EMFILE = 24;

        /// <summary>Text file busy</summary>
        public const int ETXTBSY = 26;

        /// <summary>No space left on device</summary>
        public const int ENOSPC = 28;

        /// <summary>Read-only file system</summary>
        public const int EROFS = 30;

        /// <summary>Broken pipe</summary>
        public const int EPIPE = 32;

        /// <summary>Resource deadlock would occur</summary>
        public const int EDEADLK = 35;

        /// <summary>No data available</summary>
        public const int ENODATA = 61;

        /// <summary>Network dropped connection because of reset</summary>
        public const int ENETRESET = 102;

        /// <summary>Connection reset by peer</summary>
        public const int ECONNRESET = 104;

        /// <summary>Connection timed out</summary>
        public const int ETIMEDOUT = 110;

        /// <summary>Connection refused</summary>
        public const int ECONNREFUSED = 111;

    }  
}