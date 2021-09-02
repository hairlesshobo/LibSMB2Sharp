using System;

namespace LibSMB2Sharp.Native
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
         * File open flags
         */
        public const int O_RDONLY = 00;
        public const int O_WRONLY = 01;
        public const int O_RDWR = 02;
        public const int O_CREAT = 0100;
        public const int O_EXCL = 0200;
        public const int O_SYNC = 04010000;
    }  
}