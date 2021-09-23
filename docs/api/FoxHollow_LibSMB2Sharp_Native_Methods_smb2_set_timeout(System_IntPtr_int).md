#### [LibSMB2Sharp](index.md 'index')
### [FoxHollow.LibSMB2Sharp.Native](FoxHollow_LibSMB2Sharp_Native.md 'FoxHollow.LibSMB2Sharp.Native').[Methods](FoxHollow_LibSMB2Sharp_Native_Methods.md 'FoxHollow.LibSMB2Sharp.Native.Methods')
## Methods.smb2_set_timeout(IntPtr, int) Method
Set the timeout in seconds after which a command will be aborted with  
SMB2_STATUS_IO_TIMEOUT.  
If you use timeouts with the async API you must make sure to call  
smb2_service() at least once every second.  
  
Default is 0: No timeout.  
```csharp
internal static void smb2_set_timeout(System.IntPtr smb2, int seconds);
```
#### Parameters
<a name='FoxHollow_LibSMB2Sharp_Native_Methods_smb2_set_timeout(System_IntPtr_int)_smb2'></a>
`smb2` [System.IntPtr](https://docs.microsoft.com/en-us/dotnet/api/System.IntPtr 'System.IntPtr')  
  
<a name='FoxHollow_LibSMB2Sharp_Native_Methods_smb2_set_timeout(System_IntPtr_int)_seconds'></a>
`seconds` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')  
  
