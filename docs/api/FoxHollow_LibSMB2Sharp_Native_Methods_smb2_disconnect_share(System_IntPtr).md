#### [LibSMB2Sharp](index.md 'index')
### [FoxHollow.LibSMB2Sharp.Native](FoxHollow_LibSMB2Sharp_Native.md 'FoxHollow.LibSMB2Sharp.Native').[Methods](FoxHollow_LibSMB2Sharp_Native_Methods.md 'FoxHollow.LibSMB2Sharp.Native.Methods')
## Methods.smb2_disconnect_share(IntPtr) Method
Sync call to disconnect from a share  
```csharp
internal static int smb2_disconnect_share(System.IntPtr smb2);
```
#### Parameters
<a name='FoxHollow_LibSMB2Sharp_Native_Methods_smb2_disconnect_share(System_IntPtr)_smb2'></a>
`smb2` [System.IntPtr](https://docs.microsoft.com/en-us/dotnet/api/System.IntPtr 'System.IntPtr')  
smb2 context
  
#### Returns
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')  
0      : Disconnected from the share successfully.  
-errno : Failure.  
