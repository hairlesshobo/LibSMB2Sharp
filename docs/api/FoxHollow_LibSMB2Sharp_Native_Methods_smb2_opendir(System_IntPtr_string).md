#### [LibSMB2Sharp](index.md 'index')
### [FoxHollow.LibSMB2Sharp.Native](FoxHollow_LibSMB2Sharp_Native.md 'FoxHollow.LibSMB2Sharp.Native').[Methods](FoxHollow_LibSMB2Sharp_Native_Methods.md 'FoxHollow.LibSMB2Sharp.Native.Methods')
## Methods.smb2_opendir(IntPtr, string) Method
Open a remote directory. Must be called after a share has been first opened.  
```csharp
internal static System.IntPtr smb2_opendir(System.IntPtr smb2, string path);
```
#### Parameters
<a name='FoxHollow_LibSMB2Sharp_Native_Methods_smb2_opendir(System_IntPtr_string)_smb2'></a>
`smb2` [System.IntPtr](https://docs.microsoft.com/en-us/dotnet/api/System.IntPtr 'System.IntPtr')  
smb2 context
  
<a name='FoxHollow_LibSMB2Sharp_Native_Methods_smb2_opendir(System_IntPtr_string)_path'></a>
`path` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
Path to open
  
#### Returns
[System.IntPtr](https://docs.microsoft.com/en-us/dotnet/api/System.IntPtr 'System.IntPtr')  
null on error, smb2dir structure on success
