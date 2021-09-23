# Example

```csharp
using (Smb2Context smb2 = new Smb2Context(connectionString: connString, password: password))
{
    Smb2Share share = smb2.OpenShare();

    Smb2DirectoryEntry dirEntry = share.GetDirectory("/remote/path");
}
```