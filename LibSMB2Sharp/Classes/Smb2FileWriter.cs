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
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using LibSMB2Sharp.Exceptions;
using LibSMB2Sharp.Native;

namespace LibSMB2Sharp
{
    public class Smb2FileWriter : Stream
    {
        private long _position = 0;
        private IntPtr _fhPtr = IntPtr.Zero;
        private IntPtr _bufferPtr = IntPtr.Zero;
        private int _bufferSize = -1;

        public Smb2Context Context { get; private set; }

        public override bool CanRead => false;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override long Length => this.Position;
        public Smb2FileEntry FileEntry { get; private set; }

        public override long Position 
        {
            get => _position;
            set => throw new NotSupportedException();
        }

        public Smb2FileWriter(Smb2Context context, Smb2FileEntry entry)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            this.FileEntry = entry ?? throw new ArgumentNullException(nameof(entry));
            this.FileEntry.LockFile();
            
            _fhPtr = Methods.smb2_open(this.Context.Pointer, Helpers.CleanFilePathForNative(entry.RelativePath), Const.O_WRONLY | Const.O_CREAT);
            Methods.smb2_ftruncate(this.Context.Pointer, _fhPtr, 0);

            if (this._fhPtr == IntPtr.Zero)
                throw new LibSmb2NativeMethodException(this.Context.Pointer, "Failed to open file");
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
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

            if (count == 0)
                return;

            Marshal.Copy(buffer, offset, _bufferPtr, count);

            int bytesWritten = Methods.smb2_write(this.Context.Pointer, _fhPtr, _bufferPtr, (uint)count);

            if (bytesWritten < 0)
                throw new LibSmb2NativeMethodException(this.Context, bytesWritten);

            if (bytesWritten != count)
                throw new Exception("Unknown error occurred that caused smb2 to write less bytes than were provided!");

            _position += bytesWritten;
        }

        public void Write(string text)
        {
            byte[] bytesToWrite = Encoding.UTF8.GetBytes(text);
            this.Write(bytesToWrite, 0, bytesToWrite.Length);
        }

        public void WriteLine(string text)
        {
            this.Write(text);
            this.Write(Environment.NewLine);
        }

        public override void Close()
        {
            if (_fhPtr != IntPtr.Zero)
            {
                // flush whatever may be in memory
                //! See PInvoke.cs:495 for an explanation for why this
                //! is not being used 
                // Methods.smb2_fsync(this.Context.Pointer, _fhPtr);

                // close the file
                Methods.smb2_close(this.Context.Pointer, _fhPtr);

                _fhPtr = IntPtr.Zero;
            }

            if (_bufferPtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_bufferPtr);
                _bufferPtr = IntPtr.Zero;
            }

            this.FileEntry.RefreshDetails();
            this.FileEntry.UnlockFile();
        }
    }
}