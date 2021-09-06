using System;
using System.Runtime.InteropServices;
using LibSMB2Sharp.Exceptions;
using LibSMB2Sharp.Native;

namespace LibSMB2Sharp
{
    public class Smb2Context : IDisposable
    {
        private bool _started = false;
        internal IntPtr Pointer { get; private set; }
        private IntPtr _urlPtr = IntPtr.Zero;

        private Smb2Share _share = null;
        // private Task _asyncRunnerTask = Task.CompletedTask; // reserved for future async
        // private CancellationTokenSource _cts = new CancellationTokenSource(); // reserved for future async

        public string UncPath => $"smb://{this.Server}";
        public string ConnectionString { get; private set; }
        public string Server { get; private set; }
        public string Share { get; private set; }
        public string Domain { get; private set; }
        public string User { get; private set; }
        public ulong MaxReadSize { get; private set; }
        public ulong MaxWriteSize { get; private set; }

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

            // connection string was provided, parse it
            if (!String.IsNullOrWhiteSpace(connectionString))
            {
                _urlPtr = Methods.smb2_parse_url(this.Pointer, connectionString);

                if (_urlPtr == IntPtr.Zero)
                    throw new LibSmb2NativeMethodException(this.Pointer, "Failed to parse url");

                smb2_url url = Marshal.PtrToStructure<smb2_url>(_urlPtr);

                this.SetUser(url.user);
                this.SetDomain(url.domain);
                this.SetServer(url.server);
                this.SetShare(url.share);
            }

            this.SetUser(user);
            this.SetPassword(password);
            this.SetDomain(domain);
            this.SetServer(server);
            this.SetShare(share);
        }

        public Smb2Share OpenShare()
        {
            if (_share != null)
                return _share;

            int result = Methods.smb2_connect_share(this.Pointer, this.Server, this.Share, this.User);

            if (result < Const.EOK)
                throw new LibSmb2ConnectionException(this.Pointer, result);

            _share = new Smb2Share(this);

            this.MaxReadSize = Methods.smb2_get_max_read_size(this.Pointer);
            this.MaxWriteSize = Methods.smb2_get_max_write_size(this.Pointer);

            return _share;
        }


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


        #region Private Methods

        private void SetUser(string user)
        {
            if (_started)
                throw new LibSmb2AlreadyConnectedException();

            if (!String.IsNullOrWhiteSpace(user))
            {
                this.User = user;

                Methods.smb2_set_user(this.Pointer, this.User);
            }
        }

        private void SetPassword(string password)
        {
            if (_started)
                throw new LibSmb2AlreadyConnectedException();

            if (!String.IsNullOrWhiteSpace(password))
            {
                this.Password = password;

                Methods.smb2_set_password(this.Pointer, this.Password);
            }
        }

        private void SetDomain(string domain)
        {
            if (_started)
                throw new LibSmb2AlreadyConnectedException();

            if (!String.IsNullOrWhiteSpace(domain))
            {
                this.Domain = domain;

                Methods.smb2_set_domain(this.Pointer, this.Domain);
            }
        }

        private void SetServer(string server)
        {
            if (_started)
                throw new LibSmb2AlreadyConnectedException();

            if (!String.IsNullOrWhiteSpace(server))
                this.Server = server;
        }

        private void SetShare(string share)
        {
            if (_started)
                throw new LibSmb2AlreadyConnectedException();

            if (!String.IsNullOrWhiteSpace(share))
                this.Share = share;
        }
        
        #endregion
    }
}