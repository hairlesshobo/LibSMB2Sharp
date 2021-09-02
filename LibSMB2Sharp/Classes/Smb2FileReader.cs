using System;
using System.IO;
using System.Runtime.InteropServices;
using LibSMB2Sharp.Exceptions;
using LibSMB2Sharp.Native;

namespace LibSMB2Sharp
{
    public class Smb2FileReader : Stream
    {
        private long _length = -1;
        private long _position = 0;
        private IntPtr _contextPtr = IntPtr.Zero;
        private IntPtr _fhPtr = IntPtr.Zero;
        private IntPtr _bufferPtr = IntPtr.Zero;
        private int _bufferSize = -1;

        public override bool CanRead => true;

        public override bool CanSeek => throw new NotImplementedException();

        public override bool CanWrite => false;

        public override long Length => _length;
        public Smb2FileEntry FileEntry { get; private set; }

        public override long Position
        {
            get => _position;
            set => throw new NotImplementedException();
        }

        public Smb2FileReader(IntPtr contextPtr, Smb2FileEntry entry)
        {
            this.FileEntry = entry ?? throw new ArgumentNullException(nameof(entry));
            this._contextPtr = contextPtr;
            this._fhPtr = Methods.smb2_open(_contextPtr, Helpers.CleanFilePathForNative(entry.RelativePath), Const.O_RDONLY);

            if (this._fhPtr == IntPtr.Zero)
                throw new LibSmb2NativeMethodException(_contextPtr);
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            // the requested buffer size is different than the previous (or never set)
            // so we will need to allocate the buffer on this read
            if (count > this._bufferSize)
            {
                // We previously allocated a buffer of a different size.. let's free it
                // before we allocate a new buffer
                if (_bufferPtr != IntPtr.Zero)
                    Marshal.FreeHGlobal(_bufferPtr);

                // allocate the buffer
                _bufferPtr = Marshal.AllocHGlobal(count);
            }

            if ((this.Length - this.Position) > count)
                count = (int)(this.Length - this.Position);
            
            int bytesRead = Methods.smb2_read(_contextPtr, _fhPtr, _bufferPtr, (uint)count);

            _position += bytesRead;

            if (bytesRead < 0)
                throw new LibSmb2NativeMethodException(_contextPtr);

            Marshal.Copy(_bufferPtr, buffer, offset, count);

            return bytesRead;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override void Close()
        {
            if (_fhPtr != IntPtr.Zero)
            {
                Methods.smb2_close(_contextPtr, _fhPtr);
                _fhPtr = IntPtr.Zero;
            }

            if (_bufferPtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_bufferPtr);
                _bufferPtr = IntPtr.Zero;
            }
        }
    }
}