using System;
using System.Runtime.InteropServices;
using LibSMB2Sharp.Exceptions;
using LibSMB2Sharp.Native;

namespace LibSMB2Sharp
{
    public class Smb2Context : IDisposable
    {
        private bool _started = false;
        private smb2_context _context;
        private IntPtr _contextPtr = IntPtr.Zero;
        private IntPtr _urlPtr = IntPtr.Zero;

        private Smb2Share _share = null;

        public string ConnectionString { get; private set; }
        public string Server { get; private set; }
        public string Share { get; private set; }
        public string Domain { get; private set; }
        public string User { get; private set; }

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
            _contextPtr = Methods.smb2_init_context();

            if (_contextPtr == IntPtr.Zero)
                throw new LibSmb2NativeMethodException("Failed to init context");

            Methods.smb2_set_security_mode(_contextPtr, Const.SMB2_NEGOTIATE_SIGNING_ENABLED);

            this.UpdateStructure();

            // connection string was provided, parse it
            if (!String.IsNullOrWhiteSpace(connectionString))
            {
                _urlPtr = Methods.smb2_parse_url(_contextPtr, connectionString);

                if (_urlPtr == IntPtr.Zero)
                    throw new LibSmb2NativeMethodException(_contextPtr);

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

            if (Methods.smb2_connect_share(_contextPtr, this.Server, this.Share, this.User) < 0)
                throw new LibSmb2ConnectionException(_contextPtr);

            _share = new Smb2Share(_contextPtr);

            return _share;
        }


        public void Dispose()
        {
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

            if (_contextPtr != IntPtr.Zero)
            {
                Methods.smb2_disconnect_share(_contextPtr);
                Methods.smb2_destroy_context(_contextPtr);
                _contextPtr = IntPtr.Zero;
            }
        }


        #region Private Methods

        private void UpdateStructure()
        {
            if (_contextPtr != IntPtr.Zero)
                _context = Marshal.PtrToStructure<smb2_context>(_contextPtr);
        }

        private void SetUser(string user)
        {
            if (_started)
                throw new LibSmb2AlreadyConnectedException();

            if (!String.IsNullOrWhiteSpace(user))
            {
                this.User = user;

                Methods.smb2_set_user(_contextPtr, this.User);
            }
        }

        private void SetPassword(string password)
        {
            if (_started)
                throw new LibSmb2AlreadyConnectedException();

            if (!String.IsNullOrWhiteSpace(password))
            {
                this.Password = password;

                Methods.smb2_set_password(_contextPtr, this.Password);
            }
        }

        private void SetDomain(string domain)
        {
            if (_started)
                throw new LibSmb2AlreadyConnectedException();

            if (!String.IsNullOrWhiteSpace(domain))
            {
                this.Domain = domain;

                Methods.smb2_set_domain(_contextPtr, this.Domain);
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