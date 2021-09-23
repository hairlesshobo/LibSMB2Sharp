#### [LibSMB2Sharp](index.md 'index')
### [FoxHollow.LibSMB2Sharp.Native](FoxHollow_LibSMB2Sharp_Native.md 'FoxHollow.LibSMB2Sharp.Native').[Methods](FoxHollow_LibSMB2Sharp_Native_Methods.md 'FoxHollow.LibSMB2Sharp.Native.Methods')
## Methods.smb2_set_user(IntPtr, string) Method
Set the username that we will try to authenticate as.  
Default is to try to authenticate as the current user.   
```csharp
internal static void smb2_set_user(System.IntPtr smb2, string user);
```
#### Parameters
<a name='FoxHollow_LibSMB2Sharp_Native_Methods_smb2_set_user(System_IntPtr_string)_smb2'></a>
`smb2` [System.IntPtr](https://docs.microsoft.com/en-us/dotnet/api/System.IntPtr 'System.IntPtr')  
smb2 context
  
<a name='FoxHollow_LibSMB2Sharp_Native_Methods_smb2_set_user(System_IntPtr_string)_user'></a>
`user` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
user ID to use for authentication
  
