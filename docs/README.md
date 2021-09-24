# LibSMB2Sharp

## About
This library is meant to provide native bindings for the 
[libsmb2 C library](https://github.com/sahlberg/libsmb2). The idea being that you 
would then be able to access SMB shares from any C# project, regardless of OS platform.


## **Sasquatch Warning**
> I can guarantee that this code is incomplete, buggy, and will most likely cause you 
> to grow obscene amounts of hair on your forhead, feet, ears and every other part of 
> your body if you so much as even consider using this library.
>
> ... You have been warned!

## OS Support Status
At the moment, only linux support has been worked on. After linux support is complete,
Windows will be the next target. While I would like to work on macOS support as well,
I am unable to do so because I do not have access to any macOS capable hardware.
If you would like to help add support for macOS, please let me know!


## TODO
* Add ability to open a file for appending
* Determine why DllImport cannot find entry point for smb2_fsync

## API Implementation Status

Below I will list each API call that is available in libsmb2 and categorizes them based on
their current implementation status in this wrapper library.


#### Fully ported and functionality available
* smb2_close
* smb2_closedir
* smb2_connect_share
* smb2_destroy_context
* smb2_destroy_url
* smb2_disconnect_share
* smb2_echo
* smb2_ftruncate
* smb2_get_client_guid
* smb2_get_error
* smb2_get_max_read_size
* smb2_get_max_write_size
* smb2_init_context
* smb2_mkdir
* smb2_open
* smb2_opendir
* smb2_parse_url
* smb2_read
* smb2_readdir
* smb2_rename
* smb2_rmdir
* smb2_set_domain
* smb2_set_password
* smb2_set_seal
* smb2_set_security_mode
* smb2_set_sign
* smb2_set_timeout
* smb2_set_user
* smb2_set_version
* smb2_set_workstation
* smb2_stat
* smb2_statvfs
* smb2_unlink
* smb2_write


#### In PInvoke.cs but NOT ported yet (Future scope)
* smb2_close_async
* smb2_cmd_close_async
* smb2_cmd_create_async
* smb2_cmd_echo_async
* smb2_cmd_logoff_async
* smb2_cmd_negotiate_async
* smb2_cmd_query_directory_async
* smb2_cmd_query_info_async
* smb2_cmd_session_setup_async
* smb2_cmd_set_info_async
* smb2_cmd_tree_connect_async
* smb2_cmd_tree_disconnect_async
* smb2_connect_async
* smb2_connect_share_async
* smb2_disconnect_share_async
* smb2_fstat_async
* smb2_fsync_async
* smb2_ftruncate_async
* smb2_mkdir_async
* smb2_share_enum_async
* smb2_open_async
* smb2_opendir_async
* smb2_pread_async
* smb2_pwrite_async
* smb2_read_async
* smb2_readlink_async
* smb2_rmdir_async
* smb2_stat_async
* smb2_statvfs_async
* smb2_truncate_async
* smb2_rename_async
* smb2_unlink_async
* smb2_write_async
* smb2_echo_async
* smb2_service
* smb2_get_fd
* smb2_which_events
* smb2_service_fd
* smb2_fd_event_callbacks
* smb2_get_fds


#### Not Planned
* smb2_fstat
* smb2_lseek
* smb2_pread
* smb2_pwrite
* smb2_readlink
* smb2_rewinddir
* smb2_seekdir
* smb2_telldir
* smb2_truncate


#### Most likely not in scope
* smb2_fh_from_file_id
* smb2_free_data
* smb2_get_file_id
* smb2_get_file_id
* smb2_get_opaque
* smb2_set_authentication
* smb2_set_opaque


#### Not in scope
* smb2_add_compound_pdu
* smb2_free_pdu
* smb2_queue_pdu


## License
LibSMB2Sharp is licensed under the GNU Lesser General Public License (LGPL) v2.1 or later

Copyright © 2021 Steve Cross

> This program is free software; you can redistribute it and/or modify
> it under the terms of the GNU Lesser General Public License as published by
> the Free Software Foundation; either version 2.1 of the License, or
> (at your option) any later version.
>  
> This program is distributed in the hope that it will be useful,
> but WITHOUT ANY WARRANTY; without even the implied warranty of
> MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
> GNU Lesser General Public License for more details.
>  
> You should have received a copy of the GNU Lesser General Public License
> along with this program; if not, see <http://www.gnu.org/licenses/>.