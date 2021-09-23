# Smb2Context

Namespace: FoxHollow.LibSMB2Sharp

This class represents a "context" or "connection" to a smb server. It is,
 therefore, the entrypoint for any interaction with a smb server.

```csharp
public class Smb2Context : System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Smb2Context](./foxhollow.libsmb2sharp.smb2context.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)

## Properties

### **UncPath**

UNC path represented by this context

```csharp
public string UncPath { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ConnectionString**

Connection string used to connect to this smb server

```csharp
public string ConnectionString { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Server**



```csharp
public string Server { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Share**



```csharp
public string Share { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Domain**



```csharp
public string Domain { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **User**



```csharp
public string User { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Workstation**



```csharp
public string Workstation { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **EnableEncryption**



```csharp
public bool EnableEncryption { get; private set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **RequireSigning**



```csharp
public bool RequireSigning { get; private set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Timeout**



```csharp
public int Timeout { get; private set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Version**



```csharp
public Smb2Version Version { get; private set; }
```

#### Property Value

[Smb2Version](./foxhollow.libsmb2sharp.smb2version.md)<br>

### **MaxReadSize**



```csharp
public ulong MaxReadSize { get; private set; }
```

#### Property Value

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **MaxWriteSize**



```csharp
public ulong MaxWriteSize { get; private set; }
```

#### Property Value

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **ClientGuid**



```csharp
public string ClientGuid { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **Smb2Context(String, String, String, String, String, String)**



```csharp
public Smb2Context(string connectionString, string server, string user, string password, string domain, string share)
```

#### Parameters

`connectionString` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`server` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`user` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`password` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`domain` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`share` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Methods

### **OpenShare()**



```csharp
public Smb2Share OpenShare()
```

#### Returns

[Smb2Share](./foxhollow.libsmb2sharp.smb2share.md)<br>

# Example

```csharp
using (Smb2Context smb2 = new Smb2Context(connectionString: connString, password: password))
{
    Smb2Share share = smb2.OpenShare();
}
```

### **Echo()**



```csharp
public void Echo()
```

### **Dispose()**



```csharp
public void Dispose()
```

# Example

```csharp
using (Smb2Context smb2 = new Smb2Context(connectionString: connString, password: password))
{
    Smb2Share share = smb2.OpenShare();

    Smb2DirectoryEntry dirEntry = share.GetDirectory("/remote/path");
}
```
