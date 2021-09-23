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

using System.Runtime.InteropServices;

namespace FoxHollow.LibSMB2Sharp.Native
{
    // [StructLayout(LayoutKind.Sequential)]
    // public struct smb2_context {

    //     public int fd;
    //     public int is_connected;

    //     public smb2_sec sec;

    //     public ushort security_mode;
    //     public int use_cached_creds;

    //     public smb2_negotiate_version version;

    //     public string server; // ptr 
    //     public string share; // ptr
    //     public string user; // ptr

    //     /* Only used with --without-libkrb5 */
    //     public string password; // ptr
    //     public string domain; // ptr
    //     public string workstation; // ptr

    //     [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    //     public byte[] client_challenge;

    //     public smb2_command_cb connect_cb;
    //     IntPtr connect_data; // void *connect_data; //ptr

    //     public int credits;

    //     [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    //     public byte[] client_guid;

    //     public uint tree_id;
    //     public ulong message_id;
    //     public ulong session_id;

    //     public IntPtr session_key; //ptr to byte[]
    //     public byte session_key_size;

    //     public byte signing_required;


    //     [MarshalAs(UnmanagedType.ByValArray, SizeConst = Const.SMB2_KEY_SIZE)]
    //     public byte[] signing_key;

    //     /*
    //      * For sending PDUs
    //      */
    //     public IntPtr outqueue; // ptr to smb2_pdu
    //     public IntPtr waitqueue; // ptr to smb2_pdu

    //     //% match @ 184 

    //     /*
    //      * For receiving PDUs
    //      */
    //     public smb2_io_vectors _in;

    //     //% match @ 6352

    //     public smb2_recv_state recv_state;

    //     /* SPL for the (compound) command we are currently reading */
    //     public uint spl;

    //     /* buffer to avoid having to malloc the header */
    //     [MarshalAs(UnmanagedType.ByValArray, SizeConst = Const.SMB2_HEADER_SIZE)]
    //     public byte[] header;

    //     public smb2_header hdr;
    //     /* Offset into smb2->in where the payload for the current PDU starts */
    //     public ulong payload_offset;

    //     /* Pointer to the current PDU that we are receiving the reply for.
    //      * Only valid once the full smb2 header has been received.
    //      */
    //     public IntPtr pdu; // ptr to smb2_pdu

    //     /* Server capabilities */
    //     public char supports_multi_credit;

    //     //% match @ 6512

    //     public uint max_transact_size;
    //     public uint max_read_size;
    //     public uint max_write_size;
    //     public ushort dialect;

    //     [MarshalAs(UnmanagedType.ByValArray, SizeConst = Const.MAX_ERROR_SIZE)]
    //     public char[] error_string;
    //     // public string error_string;

    //     /* Open filehandles */
    //     public IntPtr fhs; // ptr to smb2fh

    //     /* Open dirhandles */
    //     public IntPtr dirs; // ptr to smb2dir
    // }

    // [StructLayout(LayoutKind.Sequential)]
    // public struct smb2fh 
    // {
    //     public IntPtr next; //ptr to smb2fh
    //     public smb2_command_cb cb;
    //     public IntPtr cb_data; // void *cb_data; // ptr

    //     [MarshalAs(UnmanagedType.ByValArray, SizeConst = Const.SMB2_FD_SIZE)]
    //     public byte[] file_id; //smb2_file_id

    //     public long offset;
    // }

    // [StructLayout(LayoutKind.Sequential)]
    // public struct smb2dir 
    // {
    //     public IntPtr next; // ptr

    //     [JsonIgnore]
    //     public smb2_command_cb cb;
    //     public IntPtr cb_data; // void *cb_data;

    //     [MarshalAs(UnmanagedType.ByValArray, SizeConst = Const.SMB2_FD_SIZE)]
    //     public byte[] file_id; //smb2_file_id

    //     public IntPtr entries; // ptr to smb2_dirent_internal

    //     public IntPtr current_entry; // struct smb2_dirent_internal *current_entry; // ptr
    //     public int index;
    // }

    // [StructLayout(LayoutKind.Sequential)]
    // public struct smb2_dirent_internal 
    // {
    //     public IntPtr next; //ptr to smb2_dirent_internal
    //     public smb2dirent dirent;
    // }

    [StructLayout(LayoutKind.Sequential)]
    public struct smb2_stat_64 
    {
        public uint smb2_type; // file or directory?
        public uint smb2_nlink; // number of hard links
        public ulong smb2_ino; // inode
        public ulong smb2_size; // sizes of file
        public ulong smb2_atime; // last access time
        public ulong smb2_atime_nsec;
        public ulong smb2_mtime; // last modify time
        public ulong smb2_mtime_nsec;
        public ulong smb2_ctime; // change time
        public ulong smb2_ctime_nsec;
        public ulong smb2_btime; // creation time
        public ulong smb2_btime_nsec;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct smb2_statvfs 
    {
        public  uint	f_bsize;
        public  uint	f_frsize;
        public  ulong	f_blocks;
        public  ulong	f_bfree;
        public  ulong	f_bavail;
        public  uint	f_files;
        public  uint	f_ffree;
        public  uint	f_favail;
        public  uint	f_fsid;
        public  uint	f_flag;
        public  uint	f_namemax;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct smb2dirent 
    {
        public string name; // ptr // const char *name;
        public smb2_stat_64 st;
    }

    // [StructLayout(LayoutKind.Sequential)]
    // public struct smb2_async
    // {
    //     public ulong async_id;
    // }

    // [StructLayout(LayoutKind.Sequential)]
    // public struct smb2_sync 
    // {
    //     public uint process_id;
    //     public uint tree_id;
    // }

    // [StructLayout(LayoutKind.Explicit)] 
    // public struct smb2_header 
    // {
    //     // due to a limitation in c# where an array must be aligned on a 
    //     // DWORD (32bit) boundary, instead of an array here, we use a structure
    //     // to hold the bites and then later convert it to a more friendly value
    //     // with a helper if need be
    //     [FieldOffset(0)]
    //     public proto_id protocol_id; //4

    //     [FieldOffset(4)]
    //     public ushort struct_size;

    //     [FieldOffset(6)]
    //     public ushort credit_charge;
        
    //     [FieldOffset(8)]
    //     public uint status;
        
    //     [FieldOffset(12)]
    //     public ushort command;
        
    //     [FieldOffset(14)]
    //     public ushort credit_request_response;
        
    //     [FieldOffset(16)]
    //     public uint flags;

    //     [FieldOffset(20)]
    //     public uint next_command;

    //     [FieldOffset(24)]
    //     public ulong message_id;

    //     // union {
    //     [FieldOffset(32)]
    //     public smb2_async async;
        
    //     [FieldOffset(32)]
    //     public smb2_sync sync;
    //     // };
        
    //     [FieldOffset(40)]
    //     public ulong session_id;
        
    //     [FieldOffset(48)]
    //     [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    //     public byte[] signature;
    // }

    // [StructLayout(LayoutKind.Sequential)] 
    // public struct proto_id
    // {
    //     public byte byte0;
    //     public byte byte1;
    //     public byte byte2;
    //     public byte byte3;

    // }

    // [StructLayout(LayoutKind.Sequential)]
    // public struct smb2_pdu 
    // {
    //     public IntPtr next; //ptr // struct smb2_pdu *next;
    //     public smb2_header header;

    //     public IntPtr next_compound; //ptr // struct smb2_pdu *next_compound;

    //     public smb2_command_cb cb;
    //     public IntPtr cb_data; // void *cb_data;

    //     /* buffer to avoid having to malloc the headers */
    //     [MarshalAs(UnmanagedType.ByValArray, SizeConst = Const.SMB2_HEADER_SIZE)]
    //     public byte[] hdr;

    //     /* pointer to the unmarshalled payload in a reply */
    //     public IntPtr payload; // void *payload;

    //     /* For sending/receiving
    //      * out contains at least two vectors:
    //      * [0]  64 bytes for the smb header
    //      * [1+] command and and extra parameters
    //      *
    //      * in contains at least one vector:
    //      * [0+] command and and extra parameters
    //      */
    //     public smb2_io_vectors _out;
    //     public smb2_io_vectors _in;

    //     /* Data we need to retain between request/reply for QUERY INFO */
    //     public byte info_type;
    //     public byte file_info_class;
    // }

    // [StructLayout(LayoutKind.Sequential)]
    // public struct smb2_io_vectors 
    // {
    //     public ulong num_done;
    //     public ulong total_size;
    //     public int niov;
    //     [MarshalAs(UnmanagedType.ByValArray, SizeConst = Const.SMB2_MAX_VECTORS)]
    //     public smb2_iovec[] iov;
    // }

    // [StructLayout(LayoutKind.Sequential)]
    // public struct smb2_iovec 
    // {
    //     public IntPtr buf; // ptr to char[]
    //     public ulong len;
    //     public IntPtr reserved; // void (*free)(void *);
    // }

    [StructLayout(LayoutKind.Sequential)]
    public struct smb2_url 
    {
        public string domain; // ptr
        public string user; // ptr
        public string server; // ptr
        public string share; // ptr
        public string path; // ptr
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct pollfd
    {
        public int fd;			/* File descriptor to poll.  */
        public short events;		/* Types of events poller cares about.  */
        public short revents;		/* Types of events that actually occurred.  */
    };
}