﻿using System;
using System.Runtime.InteropServices;

namespace LibSMB2Sharp.Native
{
    public class Methods
    {
        // public static smb2_context? smb2_init_context()
        // {
        //     IntPtr returnPtr = IntPtr.Zero;

        //     try
        //     {
        //         returnPtr = _smb2_init_context();

        //         if (returnPtr == IntPtr.Zero)
        //             return null;

        //         return Marshal.PtrToStructure<smb2_context>(returnPtr);
        //     }
        //     finally
        //     {
        //         if (returnPtr != IntPtr.Zero)
        //             Marshal.FreeHGlobal(returnPtr);
        //     }
        // }

        /// <summary>
        ///     Create an SMB2 context
        /// </summary>
        /// <returns>null on failure, smb_context structure on success</returns>
        [DllImport("libsmb2.so.3.0.0", EntryPoint = "smb2_init_context", SetLastError = true)]
        public static extern IntPtr smb2_init_context();



        /// <summary>
        ///     Destroy an smb2 context.
        ///
        ///     Any open "struct smb2fh" will automatically be freed. You can not reference
        ///     any "struct smb2fh" after the context is destroyed. Any open "struct smb2dir" 
        ///     will automatically be freed. You can not reference any "struct smb2dir" after 
        ///     the context is destroyed. Any pending async commands will be aborted with -ECONNRESET.
        /// </summary>
        /// <param name="smb2">smb2 context</param>
        [DllImport("libsmb2.so.3.0.0", SetLastError = true)]
        public static extern void smb2_destroy_context(IntPtr smb2);



        /*
        * The following three functions are used to integrate libsmb2 in an event
        * system.
        */
        /*
        * Returns the file descriptor that libsmb2 uses.
        */
        // t_socket smb2_get_fd(struct smb2_context *smb2);



        /*
        * Returns which events that we need to poll for for the smb2 file descriptor.
        */
        // int smb2_which_events(struct smb2_context *smb2);



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
        // int smb2_service(struct smb2_context *smb2, int revents);



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
        [DllImport("libsmb2.so.3.0.0", SetLastError = true)]
        public static extern void smb2_set_security_mode(IntPtr smb2, UInt16 security_mode);
        


        /// <summary>
        ///     Set the username that we will try to authenticate as.
        ///     Default is to try to authenticate as the current user. 
        /// </summary>
        /// <param name="smb2">smb2 context</param>
        /// <param name="user">user ID to use for authentication</param>
        [DllImport("libsmb2.so.3.0.0", SetLastError = true)]
        public static extern void smb2_set_user(IntPtr smb2, string user);



        /// <summary>
        ///     Set the password that we will try to authenticate as.
        /// </summary>
        /// <param name="smb2">smb2 context</param>
        /// <param name="workstation">Password to authenticate with</param>
        [DllImport("libsmb2.so.3.0.0", SetLastError = true)]
        public static extern void smb2_set_password(IntPtr smb2, string password);

        

        /// <summary>
        ///     Set the domain that we will try to authenticate as.
        /// </summary>
        /// <param name="smb2">smb2 context</param>
        /// <param name="domain">Domain to authenticate with</param>
        [DllImport("libsmb2.so.3.0.0", SetLastError = true)]
        public static extern void smb2_set_domain(IntPtr smb2, string domain);



        /// <summary>
        ///     Set the workstation when authenticating.
        /// </summary>
        /// <param name="smb2">smb2 context</param>
        /// <param name="workstation">Workstation to authenticate as</param>        
        [DllImport("libsmb2.so.3.0.0", SetLastError = true)]
        public static extern void smb2_set_workstation(IntPtr smb2, string workstation);



        /*
        * Returns the client_guid for this context.
        */
        // const char *smb2_get_client_guid(struct smb2_context *smb2);



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
        // int smb2_connect_async(struct smb2_context *smb2, const char *server, smb2_command_cb cb, void *cb_data);



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
        // int smb2_connect_share_async(struct smb2_context *smb2, const char *server, const char *share, const char *user, smb2_command_cb cb, void *cb_data);



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
        [DllImport("libsmb2.so.3.0.0", SetLastError = true)]
        public static extern int smb2_connect_share(IntPtr smb2, string server, string share, string user);


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
        // int smb2_disconnect_share_async(struct smb2_context *smb2, smb2_command_cb cb, void *cb_data);



        /// <summary>
        ///     Sync call to disconnect from a share
        /// </summary>
        /// <param name="smb2">smb2 context</param>
        /// <returns>
        ///     0      : Disconnected from the share successfully.
        ///     -errno : Failure.
        /// </returns>
        [DllImport("libsmb2.so.3.0.0", EntryPoint = "smb2_disconnect_share", SetLastError = true)]
        public static extern int smb2_disconnect_share(IntPtr smb2);



        /* Convert an smb2/nt error code into a string */
        // const char *nterror_to_str(uint32_t status);



        /* Convert an smb2/nt error code into an errno value */
        // int nterror_to_errno(uint32_t status);


        
        


        /// <summary>
        ///     This function is used to parse an SMB2 URL into a smb2_url structure.
        /// 
        ///     SMB2 URL format:
        ///       smb2://[<domain;][<username>@]<server>/<share>/<path>
        /// 
        ///     where <server> has the format:
        ///       <host>[:<port>].
        /// </summary>
        /// <param name="smb2"></param>
        /// <param name="url"></param>
        /// <returns>
        ///     If there was a problem with the provided url format, a null will be returned. Otherwise,
        ///     a structure containing each portion of the parsed url will be returned.
        /// </returns>
        [DllImport("libsmb2.so.3.0.0", EntryPoint = "smb2_parse_url", SetLastError = true)]
        public static extern IntPtr smb2_parse_url(IntPtr smb2, string url);


        
        /// <summary>
        ///     Free up any resources used by the provided url structure
        /// </summary>
        /// <param name="url">pointer to the URL to destroy</param>
        [DllImport("libsmb2.so.3.0.0", EntryPoint = "smb2_destroy_url", SetLastError = true)]
        public static extern void smb2_destroy_url(IntPtr url);



        /// <summary>
        ///     This function returns a description of the last encountered error.
        /// </summary>
        /// <param name="smb2">smb2 context</param>
        /// <returns>String description of last error</returns>
        // TODO: Fix me
        public static string smb2_get_error(IntPtr smb2)
        {
            IntPtr ptr = _smb2_get_error(smb2);

            string error = Marshal.PtrToStringAuto(ptr);

            Console.WriteLine(error);

            return error;
        }

        [DllImport("libsmb2.so.3.0.0", EntryPoint = "smb2_get_error", SetLastError = true)]
        private static extern IntPtr _smb2_get_error(IntPtr smb2);
            // => "MEOW!!!";
            // => new string(smb2.error_string, 0, Array.IndexOf(smb2.error_string, (char)0));



   
        

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
        // private static extern int smb2_opendir_async(IntPtr smb2, string path, smb2_command_cb cb, IntPtr cb_data);



        /// <summary>
        ///     Open a remote directory. Must be called after a share has been first opened.
        /// </summary>
        /// <param name="smb2">smb2 context</param>
        /// <param name="path">Path to open</param>
        /// <returns>null on error, smb2dir structure on success</returns>
        [DllImport("libsmb2.so.3.0.0", EntryPoint = "smb2_opendir", SetLastError = true)]
        public static extern IntPtr smb2_opendir(IntPtr smb2, string path);



        /// <summary>
        ///     Close a directory that is currently open
        /// </summary>
        /// <param name="smb2">smb2 context</param>
        /// <param name="smb2dir">directory context</param>
        [DllImport("libsmb2.so.3.0.0", EntryPoint = "smb2_closedir", SetLastError = true)]
        public static extern void smb2_closedir(IntPtr smb2, IntPtr smb2dir);



        
        /// <summary>
        ///     Read the next item in the open directory
        /// </summary>
        /// <param name="smb2">smb2 context</param>
        /// <param name="dir">directory context</param>
        /// <returns>
        ///     null if no further files in the directory or
        ///     smb2dirent structure describing the entry
        /// </returns>
        [DllImport("libsmb2.so.3.0.0", EntryPoint = "smb2_readdir", SetLastError = true)]
        public static extern IntPtr smb2_readdir(IntPtr smb2, IntPtr dir);


        /*
        * rewinddir()
        */
        /*
        * smb2_rewinddir() never blocks, thus no async version is needed.
        */
        // void smb2_rewinddir(struct smb2_context *smb2, struct smb2dir *smb2dir);



        /*
        * telldir()
        */
        /*
        * smb2_telldir() never blocks, thus no async version is needed.
        */
        // long smb2_telldir(struct smb2_context *smb2, struct smb2dir *smb2dir);



        /*
        * seekdir()
        */
        /*
        * smb2_seekdir() never blocks, thus no async version is needed.
        */
        // void smb2_seekdir(struct smb2_context *smb2, struct smb2dir *smb2dir, long loc);



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
        // int smb2_open_async(struct smb2_context *smb2, const char *path, int flags, smb2_command_cb cb, void *cb_data);

        /*
         * Sync open()
         *
         * Returns NULL on failure.
         */
        // struct smb2fh *smb2_open(struct smb2_context *smb2, const char *path, int flags);


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
        // int smb2_close_async(struct smb2_context *smb2, struct smb2fh *fh, smb2_command_cb cb, void *cb_data);

        /*
         * Sync close()
         */
        // int smb2_close(struct smb2_context *smb2, struct smb2fh *fh);



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
        // int smb2_fsync_async(struct smb2_context *smb2, struct smb2fh *fh, smb2_command_cb cb, void *cb_data);



        /*
         * Sync fsync()
         */
        // int smb2_fsync(struct smb2_context *smb2, struct smb2fh *fh);



        /*
         * GetMaxReadWriteSize
         * SMB2 servers have a maximum size for read/write data that they support.
         */
        // uint32_t smb2_get_max_read_size(struct smb2_context *smb2);
        // uint32_t smb2_get_max_write_size(struct smb2_context *smb2);



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
        // int smb2_pread_async(struct smb2_context *smb2, struct smb2fh *fh, uint8_t *buf, uint32_t count, uint64_t offset, smb2_command_cb cb, void *cb_data);



        /*
         * Sync pread()
         * Use smb2_get_max_read_size to discover the maximum data size that the
         * server supports.
         */
        // int smb2_pread(struct smb2_context *smb2, struct smb2fh *fh, uint8_t *buf, uint32_t count, uint64_t offset);



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
        // int smb2_pwrite_async(struct smb2_context *smb2, struct smb2fh *fh, uint8_t *buf, uint32_t count, uint64_t offset, smb2_command_cb cb, void *cb_data);



        /*
         * Sync pwrite()
         * Use smb2_get_max_write_size to discover the maximum data size that the
         * server supports.
         */
        // int smb2_pwrite(struct smb2_context *smb2, struct smb2fh *fh, uint8_t *buf, uint32_t count, uint64_t offset);



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
        // int smb2_read_async(struct smb2_context *smb2, struct smb2fh *fh, uint8_t *buf, uint32_t count, smb2_command_cb cb, void *cb_data);



        /*
         * Sync read()
         */
        // int smb2_read(struct smb2_context *smb2, struct smb2fh *fh, uint8_t *buf, uint32_t count);



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
        // int smb2_write_async(struct smb2_context *smb2, struct smb2fh *fh, uint8_t *buf, uint32_t count, smb2_command_cb cb, void *cb_data);



        /*
         * Sync write()
         */
        // int smb2_write(struct smb2_context *smb2, struct smb2fh *fh, uint8_t *buf, uint32_t count);



        /*
         * Sync lseek()
         */
        /*
         * smb2_seek() never blocks, thus no async version is needed.
         */
        // int64_t smb2_lseek(struct smb2_context *smb2, struct smb2fh *fh, int64_t offset, int whence, uint64_t *current_offset);



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
        // int smb2_unlink_async(struct smb2_context *smb2, const char *path, smb2_command_cb cb, void *cb_data);



        /*
         * Sync unlink()
         */
        // int smb2_unlink(struct smb2_context *smb2, const char *path);



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
        // int smb2_rmdir_async(struct smb2_context *smb2, const char *path, smb2_command_cb cb, void *cb_data);



        /*
         * Sync rmdir()
         */
        // int smb2_rmdir(struct smb2_context *smb2, const char *path);



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
        // int smb2_mkdir_async(struct smb2_context *smb2, const char *path, smb2_command_cb cb, void *cb_data);



        /*
         * Sync mkdir()
         */
        // int smb2_mkdir(struct smb2_context *smb2, const char *path);



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
        // int smb2_statvfs_async(struct smb2_context *smb2, const char *path, struct smb2_statvfs *statvfs, smb2_command_cb cb, void *cb_data);



        /// <summary>
        ///     statvfs
        /// </summary>
        /// <param name="smb2">smb2 context</param>
        /// <param name="path">path to stat</param>
        /// <param name="statvfs">pointer to smb2_statvfs object</param>
        /// <returns></returns>
        [DllImport("libsmb2.so.3.0.0", SetLastError = true)]
        public static extern int smb2_statvfs(IntPtr smb2, string path, IntPtr statvfs);



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
        // int smb2_fstat_async(struct smb2_context *smb2, struct smb2fh *fh, struct smb2_stat_64 *st, smb2_command_cb cb, void *cb_data);



        /*
         * Sync fstat()
         */
        // int smb2_fstat(struct smb2_context *smb2, struct smb2fh *fh, struct smb2_stat_64 *st);



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
        // int smb2_stat_async(struct smb2_context *smb2, const char *path, struct smb2_stat_64 *st, smb2_command_cb cb, void *cb_data);


        /*
         * Sync stat()
         */
        // int smb2_stat(struct smb2_context *smb2, const char *path, struct smb2_stat_64 *st);



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
        // int smb2_rename_async(struct smb2_context *smb2, const char *oldpath, const char *newpath, smb2_command_cb cb, void *cb_data);



        /*
         * Sync rename()
         */
        // int smb2_rename(struct smb2_context *smb2, const char *oldpath, const char *newpath);



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
        // int smb2_truncate_async(struct smb2_context *smb2, const char *path, uint64_t length, smb2_command_cb cb, void *cb_data);



        /*
         * Sync truncate()
         * Function returns
         *      0 : Success
         * -errno : An error occured.
         */
        // int smb2_truncate(struct smb2_context *smb2, const char *path, uint64_t length);



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
        // int smb2_ftruncate_async(struct smb2_context *smb2, struct smb2fh *fh, uint64_t length, smb2_command_cb cb, void *cb_data);



        /*
         * Sync ftruncate()
         * Function returns
         *      0 : Success
         * -errno : An error occured.
         */
        // int smb2_ftruncate(struct smb2_context *smb2, struct smb2fh *fh, uint64_t length);
    }
}