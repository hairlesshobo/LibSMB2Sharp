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

namespace FoxHollow.LibSMB2Sharp.Native
{
    public enum smb2_sec 
    {
        SMB2_SEC_UNDEFINED = 0,
        SMB2_SEC_NTLMSSP,
        SMB2_SEC_KRB5,
    }

    public enum smb2_negotiate_version 
    {
        SMB2_VERSION_ANY  = 0,
        SMB2_VERSION_ANY2 = 2,
        SMB2_VERSION_ANY3 = 3,
        SMB2_VERSION_0202 = 0x0202,
        SMB2_VERSION_0210 = 0x0210,
        SMB2_VERSION_0300 = 0x0300,
        SMB2_VERSION_0302 = 0x0302
    }

    public enum smb2_recv_state 
    {
        SMB2_RECV_SPL = 0,
        SMB2_RECV_HEADER,
        SMB2_RECV_FIXED,
        SMB2_RECV_VARIABLE,
        SMB2_RECV_PAD,
    }
}