using System;

namespace LibSMB2Sharp
{
    public static class Const
    {
        public const int MAX_ERROR_SIZE = 256;

        // #define PAD_TO_32BIT(len) ((len + 0x03) & 0xfffffffc)

        public const int SMB2_SPL_SIZE = 4;
        public const int SMB2_HEADER_SIZE = 64;

        public const int SMB2_SIGNATURE_SIZE = 16;
        public const int SMB2_KEY_SIZE = 16;

        public const int SMB2_MAX_VECTORS = 256;

        public const int SMB2_FD_SIZE = 16;
    }  
}