#### [LibSMB2Sharp](index.md 'index')
### [FoxHollow.LibSMB2Sharp.Native](FoxHollow_LibSMB2Sharp_Native.md 'FoxHollow.LibSMB2Sharp.Native').[Methods](FoxHollow_LibSMB2Sharp_Native_Methods.md 'FoxHollow.LibSMB2Sharp.Native.Methods')
## Methods.smb2_readdir(IntPtr, IntPtr) Method
Read the next item in the open directory  
```csharp
internal static System.IntPtr smb2_readdir(System.IntPtr smb2, System.IntPtr dir);
```
#### Parameters
<a name='FoxHollow_LibSMB2Sharp_Native_Methods_smb2_readdir(System_IntPtr_System_IntPtr)_smb2'></a>
`smb2` [System.IntPtr](https://docs.microsoft.com/en-us/dotnet/api/System.IntPtr 'System.IntPtr')  
smb2 context
  
<a name='FoxHollow_LibSMB2Sharp_Native_Methods_smb2_readdir(System_IntPtr_System_IntPtr)_dir'></a>
`dir` [System.IntPtr](https://docs.microsoft.com/en-us/dotnet/api/System.IntPtr 'System.IntPtr')  
directory context
  
#### Returns
[System.IntPtr](https://docs.microsoft.com/en-us/dotnet/api/System.IntPtr 'System.IntPtr')  
null if no further files in the directory or  
smb2dirent structure describing the entry  
