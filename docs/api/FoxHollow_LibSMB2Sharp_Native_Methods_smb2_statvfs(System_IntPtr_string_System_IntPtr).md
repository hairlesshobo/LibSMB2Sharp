#### [LibSMB2Sharp](index.md 'index')
### [FoxHollow.LibSMB2Sharp.Native](FoxHollow_LibSMB2Sharp_Native.md 'FoxHollow.LibSMB2Sharp.Native').[Methods](FoxHollow_LibSMB2Sharp_Native_Methods.md 'FoxHollow.LibSMB2Sharp.Native.Methods')
## Methods.smb2_statvfs(IntPtr, string, IntPtr) Method
statvfs  
```csharp
internal static int smb2_statvfs(System.IntPtr smb2, string path, System.IntPtr statvfs);
```
#### Parameters
<a name='FoxHollow_LibSMB2Sharp_Native_Methods_smb2_statvfs(System_IntPtr_string_System_IntPtr)_smb2'></a>
`smb2` [System.IntPtr](https://docs.microsoft.com/en-us/dotnet/api/System.IntPtr 'System.IntPtr')  
smb2 context
  
<a name='FoxHollow_LibSMB2Sharp_Native_Methods_smb2_statvfs(System_IntPtr_string_System_IntPtr)_path'></a>
`path` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
path to stat
  
<a name='FoxHollow_LibSMB2Sharp_Native_Methods_smb2_statvfs(System_IntPtr_string_System_IntPtr)_statvfs'></a>
`statvfs` [System.IntPtr](https://docs.microsoft.com/en-us/dotnet/api/System.IntPtr 'System.IntPtr')  
pointer to smb2_statvfs object
  
#### Returns
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')  
