#### [LibSMB2Sharp](index.md 'index')
### [FoxHollow.LibSMB2Sharp.Native](FoxHollow_LibSMB2Sharp_Native.md 'FoxHollow.LibSMB2Sharp.Native').[Methods](FoxHollow_LibSMB2Sharp_Native_Methods.md 'FoxHollow.LibSMB2Sharp.Native.Methods')
## Methods.smb2_connect_share(IntPtr, string, string, string) Method
Sync call to connect to a share.  
On unix, if user is NULL then default to the current user.  
```csharp
internal static int smb2_connect_share(System.IntPtr smb2, string server, string share, string user);
```
#### Parameters
<a name='FoxHollow_LibSMB2Sharp_Native_Methods_smb2_connect_share(System_IntPtr_string_string_string)_smb2'></a>
`smb2` [System.IntPtr](https://docs.microsoft.com/en-us/dotnet/api/System.IntPtr 'System.IntPtr')  
smb2 context
  
<a name='FoxHollow_LibSMB2Sharp_Native_Methods_smb2_connect_share(System_IntPtr_string_string_string)_server'></a>
`server` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
server to connect to
  
<a name='FoxHollow_LibSMB2Sharp_Native_Methods_smb2_connect_share(System_IntPtr_string_string_string)_share'></a>
`share` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
share to connect to
  
<a name='FoxHollow_LibSMB2Sharp_Native_Methods_smb2_connect_share(System_IntPtr_string_string_string)_user'></a>
`user` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
user to connect to share as
  
#### Returns
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')  
0      : Connected to the share successfully.  
-errno : Failure.  
