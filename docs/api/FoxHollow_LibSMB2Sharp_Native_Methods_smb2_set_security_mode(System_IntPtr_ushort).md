#### [LibSMB2Sharp](index.md 'index')
### [FoxHollow.LibSMB2Sharp.Native](FoxHollow_LibSMB2Sharp_Native.md 'FoxHollow.LibSMB2Sharp.Native').[Methods](FoxHollow_LibSMB2Sharp_Native_Methods.md 'FoxHollow.LibSMB2Sharp.Native.Methods')
## Methods.smb2_set_security_mode(IntPtr, ushort) Method
Set the security mode for the connection.  
  
Default is 0  
```csharp
internal static void smb2_set_security_mode(System.IntPtr smb2, ushort security_mode);
```
#### Parameters
<a name='FoxHollow_LibSMB2Sharp_Native_Methods_smb2_set_security_mode(System_IntPtr_ushort)_smb2'></a>
`smb2` [System.IntPtr](https://docs.microsoft.com/en-us/dotnet/api/System.IntPtr 'System.IntPtr')  
smb2 context
  
<a name='FoxHollow_LibSMB2Sharp_Native_Methods_smb2_set_security_mode(System_IntPtr_ushort)_security_mode'></a>
`security_mode` [System.UInt16](https://docs.microsoft.com/en-us/dotnet/api/System.UInt16 'System.UInt16')  
This is a combination of the flags SMB2_NEGOTIATE_SIGNING_ENABLED  
and  SMB2_NEGOTIATE_SIGNING_REQUIRED  
  
