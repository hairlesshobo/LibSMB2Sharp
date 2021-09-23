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
using System.Runtime.InteropServices;
using FoxHollow.LibSMB2Sharp.Exceptions;
using FoxHollow.LibSMB2Sharp.Native;

namespace FoxHollow.LibSMB2Sharp
{
    /// <summary>
    ///     This class represents a "context" or "connection" to a smb server. It is,
    ///     therefore, the entrypoint for any interaction with a smb server. 
    /// </summary>
    public class Smb2Context : IDisposable
    {
        private bool _started = false;
        internal IntPtr Pointer { get; private set; }
        private IntPtr _urlPtr = IntPtr.Zero;

        private Smb2Share _share = null;

        private string _connectionString;
        private string _server;
        private string _shareName;
        private string _domain;
        private string _user;
        private string _workstation;
        private bool _enableEncryption;
        private bool _requireSigning;
        private int _timeout;
        private Smb2Version _version;
        // private Task _asyncRunnerTask = Task.CompletedTask; // reserved for future async
        // private CancellationTokenSource _cts = new CancellationTokenSource(); // reserved for future async

        /// <summary>
        ///     UNC path represented by this context
        /// </summary>
        public string UncPath => $"smb://{this.Server}";

        /// <summary>
        ///     Connection string used to connect to this smb server
        /// </summary>
        public string ConnectionString 
        { 
            get => _connectionString; 
            private set
            {
                // connection string was provided, parse it
                if (!String.IsNullOrWhiteSpace(value))
                {
                    _connectionString = value;

                    _urlPtr = Methods.smb2_parse_url(this.Pointer, this._connectionString);

                    if (_urlPtr == IntPtr.Zero)
                        throw new LibSmb2NativeMethodException(this.Pointer, "Failed to parse url");

                    smb2_url url = Marshal.PtrToStructure<smb2_url>(_urlPtr);

                    if (!String.IsNullOrWhiteSpace(url.user))
                        this.User = url.user;
                        
                    if (!String.IsNullOrWhiteSpace(url.domain))
                        this.Domain = url.domain;
                        
                    if (!String.IsNullOrWhiteSpace(url.server))
                        this.Server = url.server;
                        
                    if (!String.IsNullOrWhiteSpace(url.share))
                        this.Share = url.share;
                }
            } 
        }
        public string Server 
        { 
            get => _server; 
            private set
            {
                if (_started)
                    throw new LibSmb2AlreadyConnectedException();
                
                _server = value; 
            }
        }

        public string Share 
        { 
            get => _shareName; 
            private set
            {
                if (_started)
                    throw new LibSmb2AlreadyConnectedException();
                
                _shareName = value; 
            }
        }

        public string Domain 
        { 
            get => _domain; 
            private set
            {
                if (_started)
                    throw new LibSmb2AlreadyConnectedException();
                
                _domain = value; 
            }
        }

        public string User 
        { 
            get => _user; 
            private set
            {
                if (_started)
                    throw new LibSmb2AlreadyConnectedException();
                
                _user = value; 
            }
        }

        public string Workstation 
        { 
            get => _workstation; 
            private set
            {
                if (_started)
                    throw new LibSmb2AlreadyConnectedException();
                
                _workstation = value; 
            }
        }

        public bool EnableEncryption 
        { 
            get => _enableEncryption; 
            private set
            {
                if (_started)
                    throw new LibSmb2AlreadyConnectedException();
                
                _enableEncryption = value; 
            }
        }

        public bool RequireSigning 
        { 
            get => _requireSigning; 
            private set
            {
                if (_started)
                    throw new LibSmb2AlreadyConnectedException();
                
                _requireSigning = value; 
            }
        }

        public int Timeout 
        { 
            get => _timeout; 
            private set
            {
                if (_started)
                    throw new LibSmb2AlreadyConnectedException();
                
                _timeout = value; 
            }
        }

        public Smb2Version Version 
        { 
            get => _version; 
            private set
            {
                if (_started)
                    throw new LibSmb2AlreadyConnectedException();
                
                _version = value; 
            }
        }

        public ulong MaxReadSize { get; private set; }
        public ulong MaxWriteSize { get; private set; }
        public string ClientGuid { get; private set; }

        private string Password { get; set; }
        public Smb2Context(
            string connectionString = null, 
            string server = null, 
            string user = null, 
            string password = null, 
            string domain = null, 
            string share = null
            )
        {
            this.Pointer = Methods.smb2_init_context();

            if (this.Pointer == IntPtr.Zero)
                throw new LibSmb2NativeMethodException("Failed to init context");

            Methods.smb2_set_security_mode(this.Pointer, Const.SMB2_NEGOTIATE_SIGNING_ENABLED);

            this.ConnectionString = connectionString;

            if (!String.IsNullOrWhiteSpace(user))
                this.User = user;

            if (!String.IsNullOrWhiteSpace(password))
                this.Password = password;

            if (!String.IsNullOrWhiteSpace(domain))
                this.Domain = domain;

            if (!String.IsNullOrWhiteSpace(server))
                this.Server = server;

            if (!String.IsNullOrWhiteSpace(share))
                this.Share = share;

        }

        public Smb2Share OpenShare()
        {
            if (_share != null)
                return _share;

            ConfigureConnection();

            int result = Methods.smb2_connect_share(this.Pointer, this.Server, this.Share, this.User);

            if (result < Const.EOK)
                throw new LibSmb2ConnectionException(this, result);

            _share = new Smb2Share(this);

            this.MaxReadSize = Methods.smb2_get_max_read_size(this.Pointer);
            this.MaxWriteSize = Methods.smb2_get_max_write_size(this.Pointer);
            this.ClientGuid = Methods.smb2_get_client_guid(this.Pointer);

            return _share;
        }

        public void Echo()
            => Methods.smb2_echo(this.Pointer);

        // /// <summary>
        // /// !!! ASYNC IS NOT YET SUPPORTED !!!
        // /// Start the async polling on the libsmb2 side
        // /// </summary>
        // private void StartAsync()
        // {
        //     _asyncRunnerTask = Task.Run(() => 
        //     {
        //         pollfd pfd = new pollfd();
        //         IntPtr pfdPtr = IntPtr.Zero;

        //         try
        //         {
        //             pfdPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(pollfd)));

        //             while (!_cts.Token.IsCancellationRequested) 
        //             {
        //                 pfd.fd = Methods.smb2_get_fd(this.Pointer);
        //                 pfd.events = (short)Methods.smb2_which_events(this.Pointer);

        //                 Marshal.StructureToPtr(pfd, pfdPtr, false);

        //                 int result = Methods.poll(pfdPtr, 1, 250);

        //                 if (result < Const.EOK)
        //                     throw new Win32Exception(Marshal.GetLastWin32Error());

        //                 pfd = Marshal.PtrToStructure<pollfd>(pfdPtr);

        //                 if (pfd.revents == 0)
        //                         continue;

        //                 result = Methods.smb2_service(this.Pointer, pfd.revents);

        //                 if (result < Const.EOK)
        //                     throw new LibSmb2NativeMethodException(this.Pointer, result);
        //             }
        //         }
        //         finally
        //         {
        //             if (pfdPtr != IntPtr.Zero)
        //                 Marshal.FreeHGlobal(pfdPtr);
        //         }
        //     });
        // }


        public void Dispose()
        {
            // if (_asyncRunnerTask != Task.CompletedTask && !_asyncRunnerTask.IsCompleted)
            //     _cts.Cancel();

            if (_share != null)
            {
                _share.Dispose();
                _share = null;
            }

            if (_urlPtr != IntPtr.Zero)
            {
                Methods.smb2_destroy_url(_urlPtr);
                _urlPtr = IntPtr.Zero;
            }

            if (this.Pointer != IntPtr.Zero)
            {
                Methods.smb2_disconnect_share(this.Pointer);
                Methods.smb2_destroy_context(this.Pointer);
                this.Pointer = IntPtr.Zero;
            }
        }

        private void ConfigureConnection()
        {
            if (!String.IsNullOrWhiteSpace(this.User))
                Methods.smb2_set_user(this.Pointer, this.User);
                
            if (!String.IsNullOrWhiteSpace(this.Workstation))
                Methods.smb2_set_workstation(this.Pointer, this.Workstation);

            if (!String.IsNullOrWhiteSpace(this.Password))
                Methods.smb2_set_password(this.Pointer, this.Password);

            if (!String.IsNullOrWhiteSpace(this.Domain))
                Methods.smb2_set_domain(this.Pointer, this.Domain);

            Methods.smb2_set_seal(this.Pointer, (this.EnableEncryption ? 1 : 0));
            Methods.smb2_set_sign(this.Pointer, (this.RequireSigning ? 1 : 0));
            Methods.smb2_set_timeout(this.Pointer, this.Timeout);
            Methods.smb2_set_version(this.Pointer, (smb2_negotiate_version)this.Version);
        }
    }

    public enum Smb2Version 
    {
        Any  = 0,
        AnyVersion2 = 2,
        AnyVersion3 = 3,
        Version0202 = 0x0202,
        Version0210 = 0x0210,
        Version0300 = 0x0300,
        Version0302 = 0x0302
    }
}