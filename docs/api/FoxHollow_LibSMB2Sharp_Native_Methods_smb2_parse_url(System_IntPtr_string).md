#### [LibSMB2Sharp](index.md 'index')
### [FoxHollow.LibSMB2Sharp.Native](FoxHollow_LibSMB2Sharp_Native.md 'FoxHollow.LibSMB2Sharp.Native').[Methods](FoxHollow_LibSMB2Sharp_Native_Methods.md 'FoxHollow.LibSMB2Sharp.Native.Methods')
## Methods.smb2_parse_url(IntPtr, string) Method
This function is used to parse an SMB2 URL into a smb2_url structure.  
  
SMB2 URL format:  
  smb2://[<domain>;][<username>@]<server>/<share>/<path>  
  
where <server> has the format:  
  <host>[:<port>].  
```csharp
internal static System.IntPtr smb2_parse_url(System.IntPtr smb2, string url);
```
#### Parameters
<a name='FoxHollow_LibSMB2Sharp_Native_Methods_smb2_parse_url(System_IntPtr_string)_smb2'></a>
`smb2` [System.IntPtr](https://docs.microsoft.com/en-us/dotnet/api/System.IntPtr 'System.IntPtr')  
  
<a name='FoxHollow_LibSMB2Sharp_Native_Methods_smb2_parse_url(System_IntPtr_string)_url'></a>
`url` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
  
#### Returns
[System.IntPtr](https://docs.microsoft.com/en-us/dotnet/api/System.IntPtr 'System.IntPtr')  
If there was a problem with the provided url format, a null will be returned. Otherwise,  
a structure containing each portion of the parsed url will be returned.  
