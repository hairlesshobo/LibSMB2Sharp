# Smb2FileReader

Namespace: FoxHollow.LibSMB2Sharp



```csharp
public class Smb2FileReader : System.IO.Stream, System.IDisposable, System.IAsyncDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [MarshalByRefObject](https://docs.microsoft.com/en-us/dotnet/api/system.marshalbyrefobject) → [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream) → [Smb2FileReader](./foxhollow.libsmb2sharp.smb2filereader.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable), [IAsyncDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncdisposable)

## Properties

### **Context**



```csharp
public Smb2Context Context { get; private set; }
```

#### Property Value

[Smb2Context](./foxhollow.libsmb2sharp.smb2context.md)<br>

### **CanRead**



```csharp
public bool CanRead { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **CanSeek**



```csharp
public bool CanSeek { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **CanWrite**



```csharp
public bool CanWrite { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Length**



```csharp
public long Length { get; }
```

#### Property Value

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **FileEntry**



```csharp
public Smb2FileEntry FileEntry { get; private set; }
```

#### Property Value

[Smb2FileEntry](./foxhollow.libsmb2sharp.smb2fileentry.md)<br>

### **Position**



```csharp
public long Position { get; set; }
```

#### Property Value

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **CanTimeout**



```csharp
public bool CanTimeout { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ReadTimeout**



```csharp
public int ReadTimeout { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **WriteTimeout**



```csharp
public int WriteTimeout { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **Smb2FileReader(Smb2Context, Smb2FileEntry)**

Create a new instance of the Smb2FileReader class, which allows for reading
 from a remote file on a smb share

```csharp
public Smb2FileReader(Smb2Context context, Smb2FileEntry entry)
```

#### Parameters

`context` [Smb2Context](./foxhollow.libsmb2sharp.smb2context.md)<br>
Smb2Context that the reader will operate against

`entry` [Smb2FileEntry](./foxhollow.libsmb2sharp.smb2fileentry.md)<br>
FileEntry that is to be opened for reading

## Methods

### **Flush()**



```csharp
public void Flush()
```

### **Read(Byte[], Int32, Int32)**



```csharp
public int Read(Byte[] buffer, int offset, int count)
```

#### Parameters

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

`offset` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`count` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Seek(Int64, SeekOrigin)**



```csharp
public long Seek(long offset, SeekOrigin origin)
```

#### Parameters

`offset` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

`origin` [SeekOrigin](https://docs.microsoft.com/en-us/dotnet/api/system.io.seekorigin)<br>

#### Returns

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **SetLength(Int64)**



```csharp
public void SetLength(long value)
```

#### Parameters

`value` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **Write(Byte[], Int32, Int32)**



```csharp
public void Write(Byte[] buffer, int offset, int count)
```

#### Parameters

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

`offset` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`count` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Close()**



```csharp
public void Close()
```
