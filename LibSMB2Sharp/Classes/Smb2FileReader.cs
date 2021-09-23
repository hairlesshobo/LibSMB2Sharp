/*
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
using System.IO;
using System.Runtime.InteropServices;
using FoxHollow.LibSMB2Sharp.Exceptions;
using FoxHollow.LibSMB2Sharp.Native;

namespace FoxHollow.LibSMB2Sharp
{
    public class Smb2FileReader : Stream
    {
        private long _position = 0;
        private IntPtr _fhPtr = IntPtr.Zero;
        private IntPtr _bufferPtr = IntPtr.Zero;
        private int _bufferSize = -1;

        public Smb2Context Context { get; private set; }

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override long Length => (long)this.FileEntry.Size;
        public Smb2FileEntry FileEntry { get; private set; }

        public override long Position 
        {
            get => _position;
            set => throw new NotSupportedException();
        }

        /// <summary>
        ///     Create a new instance of the Smb2FileReader class, which allows for reading
        ///     from a remote file on a smb share
        /// </summary>
        /// <param name="context">Smb2Context that the reader will operate against</param>
        /// <param name="entry">FileEntry that is to be opened for reading</param>
        public Smb2FileReader(Smb2Context context, Smb2FileEntry entry)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            this.FileEntry = entry ?? throw new ArgumentNullException(nameof(entry));
            this._fhPtr = Methods.smb2_open(this.Context.Pointer, Helpers.CleanFilePathForNative(entry.RelativePath), Const.O_RDONLY);
            this.FileEntry.LockFile();

            if (this._fhPtr == IntPtr.Zero)
                throw new LibSmb2NativeMethodException(this.Context.Pointer, "Failed to open file");
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_fhPtr == IntPtr.Zero)
                throw new LibSmb2FileAlreadyClosedException(this.FileEntry.RelativePath);
                
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

                this._bufferSize = count;
            }

            if ((this.Length - this.Position) < count)
                count = (int)(this.Length - this.Position);

            if (count == 0)
                return 0;
            
            int bytesRead = Methods.smb2_read(this.Context.Pointer, _fhPtr, _bufferPtr, (uint)count);

            _position += bytesRead;

            if (bytesRead < 0)
                throw new LibSmb2NativeMethodException(this.Context, bytesRead);

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
                Methods.smb2_close(this.Context.Pointer, _fhPtr);
                _fhPtr = IntPtr.Zero;
            }

            if (_bufferPtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_bufferPtr);
                _bufferPtr = IntPtr.Zero;
            }

            this.FileEntry.UnlockFile();
        }
    }
}