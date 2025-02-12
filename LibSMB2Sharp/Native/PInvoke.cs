﻿/*
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
using System.Runtime.InteropServices;

namespace FoxHollow.LibSMB2Sharp.Native
{
    internal class Methods
    {
        /// <summary>
        ///     Create an SMB2 context
        /// </summary>
        /// <returns>null on failure, smb_context structure on success</returns>
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern IntPtr smb2_init_context();



        /// <summary>
        ///     Destroy an smb2 context.
        ///
        ///     Any open "struct smb2fh" will automatically be freed. You can not reference
        ///     any "struct smb2fh" after the context is destroyed. Any open "struct smb2dir" 
        ///     will automatically be freed. You can not reference any "struct smb2dir" after 
        ///     the context is destroyed. Any pending async commands will be aborted with -ECONNRESET.
        /// </summary>
        /// <param name="smb2">smb2 context</param>
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern void smb2_destroy_context(IntPtr smb2);



        /*
         * The following three functions are used to integrate libsmb2 in an event
         * system.
         */
        /*
         * Returns the file descriptor that libsmb2 uses.
         */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern int smb2_get_fd(IntPtr smb2);

        /*
         * Returns which events that we need to poll for for the smb2 file descriptor.
         */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern int smb2_which_events(IntPtr smb2);
        /*
         * Called to process the events when events become available for the smb2
         * file descriptor.
         *
         * Returns:
         *  0 : Success
         * <0 : Unrecoverable failure. At this point the context can no longer be
         *      used and must be freed by calling smb2_destroy_context().
         *
         */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern int smb2_service(IntPtr smb2, int revents);



        /// <summary>
        ///     Set the timeout in seconds after which a command will be aborted with
        ///     SMB2_STATUS_IO_TIMEOUT.
        ///     If you use timeouts with the async API you must make sure to call
        ///     smb2_service() at least once every second.
        /// 
        ///     Default is 0: No timeout.
        /// </summary>
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern void smb2_set_timeout(IntPtr smb2, int seconds);



        /// <summary>
        ///     Set the version that is allowed to make the connection
        /// </summary>
        /// <param name="smb2">Pointer to smb2 context</param>
        /// <param name="version">Version that is allowed</param>
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern void smb2_set_version(IntPtr smb2, smb2_negotiate_version version);



        /// <summary>
        /// Set the security mode for the connection.
        /// 
        /// Default is 0
        /// </summary>
        /// <param name="smb2">smb2 context</param>
        /// <param name="security_mode">
        ///     This is a combination of the flags SMB2_NEGOTIATE_SIGNING_ENABLED
        ///     and  SMB2_NEGOTIATE_SIGNING_REQUIRED
        /// </param>
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern void smb2_set_security_mode(IntPtr smb2, UInt16 security_mode);
        


        /*
         * Set whether smb3 encryption should be used or not.
         * 0  : disable encryption. This is the default.
         * !0 : enable encryption.
         */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern void smb2_set_seal(IntPtr smb2, int val);



        /*
         * Set whether smb2 signing should be required or not
         * 0  : do not require signing. This is the default.
         * !0 : require signing.
         */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern void smb2_set_sign(IntPtr smb2, int val);



        /*
         * Set authentication method.
         * SMB2_SEC_UNDEFINED (use KRB if available or NTLM if not)
         * SMB2_SEC_NTLMSSP
         * SMB2_SEC_KRB5
         */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern void smb2_set_authentication(IntPtr smb2, int val);



        /// <summary>
        ///     Set the username that we will try to authenticate as.
        ///     Default is to try to authenticate as the current user. 
        /// </summary>
        /// <param name="smb2">smb2 context</param>
        /// <param name="user">user ID to use for authentication</param>
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern void smb2_set_user(IntPtr smb2, string user);



        /// <summary>
        ///     Set the password that we will try to authenticate as.
        /// </summary>
        /// <param name="smb2">smb2 context</param>
        /// <param name="workstation">Password to authenticate with</param>
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern void smb2_set_password(IntPtr smb2, string password);

        

        /// <summary>
        ///     Set the domain that we will try to authenticate as.
        /// </summary>
        /// <param name="smb2">smb2 context</param>
        /// <param name="domain">Domain to authenticate with</param>
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern void smb2_set_domain(IntPtr smb2, string domain);



        /// <summary>
        ///     Set the workstation when authenticating.
        /// </summary>
        /// <param name="smb2">smb2 context</param>
        /// <param name="workstation">Workstation to authenticate as</param>        
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern void smb2_set_workstation(IntPtr smb2, string workstation);



        /*
        * Returns the client_guid for this context.
        */
        internal static string smb2_get_client_guid(IntPtr smb2)
        {
            IntPtr ptr = _smb2_get_client_guid(smb2);

            string error = Marshal.PtrToStringAuto(ptr);

            return error;
        }

        [DllImport("libsmb2", EntryPoint = "smb2_get_client_guid", SetLastError = true)]
        private static extern IntPtr _smb2_get_client_guid(IntPtr smb2);



        /*
        * Asynchronous call to connect a TCP connection to the server
        *
        * Returns:
        *  0 if the call was initiated and a connection will be attempted. Result of
        * the connection will be reported through the callback function.
        * <0 if there was an error. The callback function will not be invoked.
        *
        * Callback parameters :
        * status can be either of :
        *    0     : Connection was successful. Command_data is NULL.
        *
        *   <0     : Failed to establish the connection. Command_data is NULL.
        */
        // int smb2_connect_async(IntPtr smb2, const char *server, smb2_command_cb cb, void *cb_data);



        /*
        * Async call to connect to a share.
        * On unix, if user is NULL then default to the current user.
        *
        * Returns:
        *  0 if the call was initiated and a connection will be attempted. Result of
        * the connection will be reported through the callback function.
        * -errno if there was an error. The callback function will not be invoked.
        *
        * Callback parameters :
        * status can be either of :
        *    0     : Connection was successful. Command_data is NULL.
        *
        *   -errno : Failed to connect to the share. Command_data is NULL.
        */
        // int smb2_connect_share_async(IntPtr smb2, const char *server, const char *share, const char *user, smb2_command_cb cb, void *cb_data);



        /// <summary>
        ///     Sync call to connect to a share.
        ///     On unix, if user is NULL then default to the current user.
        /// </summary>
        /// <param name="smb2">smb2 context</param>
        /// <param name="server">server to connect to</param>
        /// <param name="share">share to connect to</param>
        /// <param name="user">user to connect to share as</param>
        /// <returns>
        ///     0      : Connected to the share successfully.
        ///     -errno : Failure.
        /// </returns>
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern int smb2_connect_share(IntPtr smb2, string server, string share, string user);


        /*
        * Async call to disconnect from a share/
        *
        * Returns:
        *  0 if the call was initiated and a connection will be attempted. Result of
        * the disconnect will be reported through the callback function.
        * -errno if there was an error. The callback function will not be invoked.
        *
        * Callback parameters :
        * status can be either of :
        *    0     : Connection was successful. Command_data is NULL.
        *
        *   -errno : Failed to disconnect the share. Command_data is NULL.
        */
        // int smb2_disconnect_share_async(IntPtr smb2, smb2_command_cb cb, void *cb_data);



        /// <summary>
        ///     Sync call to disconnect from a share
        /// </summary>
        /// <param name="smb2">smb2 context</param>
        /// <returns>
        ///     0      : Disconnected from the share successfully.
        ///     -errno : Failure.
        /// </returns>
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern int smb2_disconnect_share(IntPtr smb2);



        /// <summary>
        ///     This function is used to parse an SMB2 URL into a smb2_url structure.
        /// 
        ///     SMB2 URL format:
        ///       smb2://[&lt;domain&gt;;][&lt;username&gt;@]&lt;server&gt;/&lt;share&gt;/&lt;path&gt;
        /// 
        ///     where &lt;server> has the format:
        ///       &lt;host&gt;[:&lt;port&gt;].
        /// </summary>
        /// <param name="smb2"></param>
        /// <param name="url"></param>
        /// <returns>
        ///     If there was a problem with the provided url format, a null will be returned. Otherwise,
        ///     a structure containing each portion of the parsed url will be returned.
        /// </returns>
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern IntPtr smb2_parse_url(IntPtr smb2, string url);


        
        /// <summary>
        ///     Free up any resources used by the provided url structure
        /// </summary>
        /// <param name="url">pointer to the URL to destroy</param>
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern void smb2_destroy_url(IntPtr url);



        /// <summary>
        ///     This function returns a description of the last encountered error.
        /// </summary>
        /// <param name="smb2">smb2 context</param>
        /// <returns>String description of last error</returns>
        internal static string smb2_get_error(IntPtr smb2)
        {
            IntPtr ptr = _smb2_get_error(smb2);

            string error = Marshal.PtrToStringAuto(ptr);

            // Console.WriteLine(error);

            return error;
        }

        [DllImport("libsmb2", EntryPoint = "smb2_get_error", SetLastError = true)]
        private static extern IntPtr _smb2_get_error(IntPtr smb2);
   
        

        /*
        * Async opendir()
        *
        * Returns
        *  0 : The operation was initiated. Result of the operation will be reported
        * through the callback function.
        * <0 : There was an error. The callback function will not be invoked.
        *
        * When the callback is invoked, status indicates the result:
        *      0 : Success.
        *          Command_data is struct smb2dir.
        *          This structure is freed using smb2_closedir().
        * -errno : An error occured.
        *          Command_data is NULL.
        */       
        // int smb2_opendir_async(IntPtr smb2, string path, smb2_command_cb cb, IntPtr cb_data);



        /// <summary>
        ///     Open a remote directory. Must be called after a share has been first opened.
        /// </summary>
        /// <param name="smb2">smb2 context</param>
        /// <param name="path">Path to open</param>
        /// <returns>null on error, smb2dir structure on success</returns>
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern IntPtr smb2_opendir(IntPtr smb2, string path);



        /// <summary>
        ///     Close a directory that is currently open
        /// </summary>
        /// <param name="smb2">smb2 context</param>
        /// <param name="smb2dir">directory context</param>
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern void smb2_closedir(IntPtr smb2, IntPtr smb2dir);



        
        /// <summary>
        ///     Read the next item in the open directory
        /// </summary>
        /// <param name="smb2">smb2 context</param>
        /// <param name="dir">directory context</param>
        /// <returns>
        ///     null if no further files in the directory or
        ///     smb2dirent structure describing the entry
        /// </returns>
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern IntPtr smb2_readdir(IntPtr smb2, IntPtr dir);



        /*
        * rewinddir()
        */
        /*
        * smb2_rewinddir() never blocks, thus no async version is needed.
        */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern void smb2_rewinddir(IntPtr smb2, IntPtr smb2dir);



        /*
        * telldir()
        */
        /*
        * smb2_telldir() never blocks, thus no async version is needed.
        */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern long smb2_telldir(IntPtr smb2, IntPtr smb2dir);



        /*
        * seekdir()
        */
        /*
        * smb2_seekdir() never blocks, thus no async version is needed.
        */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern void smb2_seekdir(IntPtr smb2, IntPtr smb2dir, long loc);




        /*
         * Async open()
         *
         * Opens or creates a file.
         * Supported flags are:
         * O_RDONLY
         * O_WRONLY
         * O_RDWR
         * O_SYNC
         * O_CREAT
         * O_EXCL
         *
         * Returns
         *  0     : The operation was initiated. Result of the operation will be
         *          reported through the callback function.
         * -errno : There was an error. The callback function will not be invoked.
         *
         * When the callback is invoked, status indicates the result:
         *      0 : Success.
         *          Command_data is struct smb2fh.
         *          This structure is freed using smb2_close().
         * -errno : An error occured.
         *          Command_data is NULL.
         */       
        // int smb2_open_async(IntPtr smb2, const char *path, int flags, smb2_command_cb cb, void *cb_data);

        /*
         * Sync open()
         *
         * Returns NULL on failure.
          returns smb2fh
         */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern IntPtr smb2_open(IntPtr smb2, string path, int flags);


        /*
         * Async close()
         *
         * Returns
         *  0     : The operation was initiated. Result of the operation will be
         *          reported through the callback function.
         * -errno : There was an error. The callback function will not be invoked.
         *
         * When the callback is invoked, status indicates the result:
         *      0 : Success.
         * -errno : An error occured.
         *
         * Command_data is always NULL.
         */
        // int smb2_close_async(IntPtr smb2, IntPtr fh, smb2_command_cb cb, void *cb_data);

        /*
         * Sync close()
         */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern int smb2_close(IntPtr smb2, IntPtr fh);



        /*
         * FSYNC
         */
        /*
         * Async fsync()
         *
         * Returns
         *  0     : The operation was initiated. Result of the operation will be
         *          reported through the callback function.
         * -errno : There was an error. The callback function will not be invoked.
         *
         * When the callback is invoked, status indicates the result:
         *      0 : Success.
         * -errno : An error occured.
         *
         * Command_data is always NULL.
         */
        // int smb2_fsync_async(IntPtr smb2, IntPtr fh, smb2_command_cb cb, void *cb_data);



        /*
         * Sync fsync()
         */
        //! No idea why, but dotnet cannot find this entry point.. even
        //! though it was confirmed to exit with `libsmb2.so`.. and it has
        //! no problem finding all the other entry points we need. I'm sure one
        //! day i will be forced to figure out why... until then, we're gonna try 
        //! to do without
        [DllImport("libsmb2", EntryPoint = "smb2_fsync", SetLastError = true)]
        internal static extern int smb2_fsync(IntPtr smb2, IntPtr fh);



        /*
         * GetMaxReadWriteSize
         * SMB2 servers have a maximum size for read/write data that they support.
         */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern ulong smb2_get_max_read_size(IntPtr smb2);


        
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern ulong smb2_get_max_write_size(IntPtr smb2);




        /*
         * PREAD
         */
        /*
         * Async pread()
         * Use smb2_get_max_read_size to discover the maximum data size that the
         * server supports.
         *
         * Returns
         *  0     : The operation was initiated. Result of the operation will be
         *          reported through the callback function.
         * -errno : There was an error. The callback function will not be invoked.
         *
         * When the callback is invoked, status indicates the result:
         *    >=0 : Number of bytes read.
         * -errno : An error occured.
         *
         * Command_data is always NULL.
         */       
        // int smb2_pread_async(IntPtr smb2, IntPtr fh, IntPtr buf, uint count, ulong offset, smb2_command_cb cb, void *cb_data);



        /*
         * Sync pread()
         * Use smb2_get_max_read_size to discover the maximum data size that the
         * server supports.
         */
         [DllImport("libsmb2", SetLastError = true)]
        internal static extern int smb2_pread(IntPtr smb2, IntPtr fh, IntPtr buf, uint count, ulong offset);



        /*
         * PWRITE
         */
        /*
         * Async pwrite()
         * Use smb2_get_max_write_size to discover the maximum data size that the
         * server supports.
         *
         * Returns
         *  0     : The operation was initiated. Result of the operation will be
         *          reported through the callback function.
         * -errno : There was an error. The callback function will not be invoked.
         *
         * When the callback is invoked, status indicates the result:
         *    >=0 : Number of bytes written.
         * -errno : An error occured.
         *
         * Command_data is always NULL.
         */       
        // int smb2_pwrite_async(IntPtr smb2, IntPtr fh, IntPtr buf, uint count, ulong offset, smb2_command_cb cb, void *cb_data);



        /*
         * Sync pwrite()
         * Use smb2_get_max_write_size to discover the maximum data size that the
         * server supports.
         */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern int smb2_pwrite(IntPtr smb2, IntPtr fh, IntPtr buf, uint count, ulong offset);



        /*
         * READ
         */
        /*
         * Async read()
         *
         * Returns
         *  0     : The operation was initiated. Result of the operation will be
         *          reported through the callback function.
         * -errno : There was an error. The callback function will not be invoked.
         *
         * When the callback is invoked, status indicates the result:
         *    >=0 : Number of bytes read.
         * -errno : An error occured.
         *
         * Command_data is always NULL.
         */
        // int smb2_read_async(IntPtr smb2, IntPtr fh, IntPtr buf, uint count, smb2_command_cb cb, void *cb_data);



        /*
         * Sync read()
         */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern int smb2_read(IntPtr smb2, IntPtr fh, IntPtr buffer, UInt32 count);



        /*
         * WRITE
         */
        /*
         * Async write()
         *
         * Returns
         *  0     : The operation was initiated. Result of the operation will be
         *          reported through the callback function.
         * -errno : There was an error. The callback function will not be invoked.
         *
         * When the callback is invoked, status indicates the result:
         *    >=0 : Number of bytes written.
         * -errno : An error occured.
         *
         * Command_data is always NULL.
         */
        // int smb2_write_async(IntPtr smb2, IntPtr fh, IntPtr buf, uint count, smb2_command_cb cb, void *cb_data);



        /*
         * Sync write()
         */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern int smb2_write(IntPtr smb2, IntPtr fh, IntPtr buf, uint count);



        /*
         * Sync lseek()
         */
        /*
         * smb2_seek() never blocks, thus no async version is needed.
         */
        // [DllImport("libsmb2", SetLastError = true)]
        // smb2_lseek(IntPtr smb2, IntPtr fh, int64_t offset, int whence, uint64_t *current_offset)



        /*
         * UNLINK
         */
        /*
         * Async unlink()
         *
         * Returns
         *  0     : The operation was initiated. Result of the operation will be
         *          reported through the callback function.
         * -errno : There was an error. The callback function will not be invoked.
         *
         * When the callback is invoked, status indicates the result:
         *      0 : Success.
         * -errno : An error occured.
         *
         * Command_data is always NULL.
         */
        // [DllImport("libsmb2", SetLastError = true)]
        // int smb2_unlink_async(IntPtr smb2, const char *path, smb2_command_cb cb, void *cb_data);



        /*
         * Sync unlink()
         */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern int smb2_unlink(IntPtr smb2, string path);



        /*
         * RMDIR
         */
        /*
         * Async rmdir()
         *
         * Returns
         *  0     : The operation was initiated. Result of the operation will be
         *          reported through the callback function.
         * -errno : There was an error. The callback function will not be invoked.
         *
         * When the callback is invoked, status indicates the result:
         *      0 : Success.
         * -errno : An error occured.
         *
         * Command_data is always NULL.
         */
        // int smb2_rmdir_async(IntPtr smb2, const char *path, smb2_command_cb cb, void *cb_data);



        /*
         * Sync rmdir()
         */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern int smb2_rmdir(IntPtr smb2, string path);



        /*
         * MKDIR
         */
        /*
         * Async mkdir()
         *
         * Returns
         *  0     : The operation was initiated. Result of the operation will be
         *          reported through the callback function.
         * -errno : There was an error. The callback function will not be invoked.
         *
         * When the callback is invoked, status indicates the result:
         *      0 : Success.
         * -errno : An error occured.
         *
         * Command_data is always NULL.
         */
        // [DllImport("libsmb2", SetLastError = true)]
        // int smb2_mkdir_async(IntPtr smb2, string path, smb2_command_cb cb, IntPtr cb_data);



        /*
         * Sync mkdir()
         */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern int smb2_mkdir(IntPtr smb2, string path);



        /*
         * STATVFS
         */
        /*
         * Async statvfs()
         *
         * Returns
         *  0     : The operation was initiated. Result of the operation will be
         *          reported through the callback function.
         * -errno : There was an error. The callback function will not be invoked.
         *
         * When the callback is invoked, status indicates the result:
         *      0 : Success. Command_data is struct smb2_statvfs
         * -errno : An error occured.
         */
        // [DllImport("libsmb2", SetLastError = true)]
        // int smb2_statvfs_async(IntPtr smb2, string path, IntPtr statvfs, smb2_command_cb cb, IntPtr cb_data);



        /// <summary>
        ///     statvfs
        /// </summary>
        /// <param name="smb2">smb2 context</param>
        /// <param name="path">path to stat</param>
        /// <param name="statvfs">pointer to smb2_statvfs object</param>
        /// <returns></returns>
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern int smb2_statvfs(IntPtr smb2, string path, IntPtr statvfs);



        /*
         * FSTAT
         */
        /*
         * Async fstat()
         *
         * Returns
         *  0     : The operation was initiated. Result of the operation will be
         *          reported through the callback function.
         * -errno : There was an error. The callback function will not be invoked.
         *
         * When the callback is invoked, status indicates the result:
         *      0 : Success. Command_data is struct smb2_stat_64
         * -errno : An error occured.
         */
        // int smb2_fstat_async(IntPtr smb2, IntPtr fh, struct smb2_stat_64 *st, smb2_command_cb cb, void *cb_data);



        /*
         * Sync fstat()
         */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern int smb2_fstat(IntPtr smb2, IntPtr fh, IntPtr st);



        /*
         * Async stat()
         *
         * Returns
         *  0     : The operation was initiated. Result of the operation will be
         *          reported through the callback function.
         * -errno : There was an error. The callback function will not be invoked.
         *
         * When the callback is invoked, status indicates the result:
         *      0 : Success. Command_data is struct smb2_stat_64
         * -errno : An error occured.
         */
        // [DllImport("libsmb2", EntryPoint = "smb2_stat_async", SetLastError = true)]
        // int smb2_stat_async(IntPtr smb2, string path, IntPtr st, smb2_command_cb cb, IntPtr cb_data);


        /*
         * Sync stat()
         */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern int smb2_stat(IntPtr smb2, string path, IntPtr st);



        /*
         * Async rename()
         *
         * Returns
         *  0     : The operation was initiated. Result of the operation will be
         *          reported through the callback function.
         * -errno : There was an error. The callback function will not be invoked.
         *
         * When the callback is invoked, status indicates the result:
         *      0 : Success.
         * -errno : An error occured.
         */
        // int smb2_rename_async(IntPtr smb2, const char *oldpath, const char *newpath, smb2_command_cb cb, void *cb_data);



        /*
         * Sync rename()
         */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern int smb2_rename(IntPtr smb2, string oldpath, string newpath);



        /*
         * Async truncate()
         *
         * Returns
         *  0     : The operation was initiated. Result of the operation will be
         *          reported through the callback function.
         * -errno : There was an error. The callback function will not be invoked.
         *
         * When the callback is invoked, status indicates the result:
         *      0 : Success.
         * -errno : An error occured.
         */
        // int smb2_truncate_async(IntPtr smb2, const char *path, ulong length, smb2_command_cb cb, void *cb_data);



        /*
         * Sync truncate()
         * Function returns
         *      0 : Success
         * -errno : An error occured.
         */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern int smb2_truncate(IntPtr smb2, string path, ulong length);



        /*
         * Async ftruncate()
         *
         * Returns
         *  0     : The operation was initiated. Result of the operation will be
         *          reported through the callback function.
         * -errno : There was an error. The callback function will not be invoked.
         *
         * When the callback is invoked, status indicates the result:
         *      0 : Success.
         * -errno : An error occured.
         */
        // int smb2_ftruncate_async(IntPtr smb2, IntPtr fh, ulong length, smb2_command_cb cb, void *cb_data);



        /*
         * Sync ftruncate()
         * Function returns
         *      0 : Success
         * -errno : An error occured.
         */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern int smb2_ftruncate(IntPtr smb2, IntPtr fh, ulong length);



        /*
         * READLINK
         */
        /*
         * Async readlink()
         *
         * Returns
         *  0     : The operation was initiated. The link content will be
         *          reported through the callback function.
         * -errno : There was an error. The callback function will not be invoked.
         *
         * When the callback is invoked, status indicates the result:
         *      0 : Success. Command_data is the link content.
         * -errno : An error occured.
         */
        // [DllImport("libsmb2", SetLastError = true)]
        // int smb2_readlink_async(IntPtr smb2, string path, smb2_command_cb cb, IntPtr cb_data);

        /*
         * Sync readlink()
         */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern int smb2_readlink(IntPtr smb2, string path, IntPtr buf, UInt32 bufsiz);



        /*
         * Async echo()
         *
         * Returns
         *  0     : The operation was initiated. Result of the operation will be
         *          reported through the callback function.
         * -errno : There was an error. The callback function will not be invoked.
         *
         * When the callback is invoked, status indicates the result:
         *      0 : Success.
         * -errno : An error occured.
         */
        // int smb2_echo_async(IntPtr smb2, smb2_command_cb cb, void *cb_data);

        /*
         * Sync echo()
         *
         * Returns:
         * 0      : successfully send the message and received a reply.
         * -errno : Failure.
         */
        [DllImport("libsmb2", SetLastError = true)]
        internal static extern int smb2_echo(IntPtr smb2);



        [DllImport("libsmb2", SetLastError = true)]
        internal static extern int poll(IntPtr fds, ulong nfds, int timeout);
    }
}
