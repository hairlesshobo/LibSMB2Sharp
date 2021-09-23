#### [LibSMB2Sharp](index.md 'index')
### [FoxHollow.LibSMB2Sharp.Native](FoxHollow_LibSMB2Sharp_Native.md 'FoxHollow.LibSMB2Sharp.Native').[Methods](FoxHollow_LibSMB2Sharp_Native_Methods.md 'FoxHollow.LibSMB2Sharp.Native.Methods')
## Methods.smb2_destroy_context(IntPtr) Method
Destroy an smb2 context.  
  
Any open "struct smb2fh" will automatically be freed. You can not reference  
any "struct smb2fh" after the context is destroyed. Any open "struct smb2dir"   
will automatically be freed. You can not reference any "struct smb2dir" after   
the context is destroyed. Any pending async commands will be aborted with -ECONNRESET.  
```csharp
internal static void smb2_destroy_context(System.IntPtr smb2);
```
#### Parameters
<a name='FoxHollow_LibSMB2Sharp_Native_Methods_smb2_destroy_context(System_IntPtr)_smb2'></a>
`smb2` [System.IntPtr](https://docs.microsoft.com/en-us/dotnet/api/System.IntPtr 'System.IntPtr')  
smb2 context
  
