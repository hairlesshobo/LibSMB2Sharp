# Smb2FileEntry

Namespace: FoxHollow.LibSMB2Sharp



```csharp
public class Smb2FileEntry : Smb2Entry, System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Smb2Entry](./foxhollow.libsmb2sharp.smb2entry.md) → [Smb2FileEntry](./foxhollow.libsmb2sharp.smb2fileentry.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)

## Properties

### **Name**



```csharp
public string Name { get;  set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **RelativePath**



```csharp
public string RelativePath { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Directory**



```csharp
public string Directory { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **UncPath**



```csharp
public string UncPath { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Size**



```csharp
public ulong Size { get;  set; }
```

#### Property Value

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **AcccessDtm**



```csharp
public DateTimeOffset AcccessDtm { get;  set; }
```

#### Property Value

[DateTimeOffset](https://docs.microsoft.com/en-us/dotnet/api/system.datetimeoffset)<br>

### **ChangeDtm**



```csharp
public DateTimeOffset ChangeDtm { get;  set; }
```

#### Property Value

[DateTimeOffset](https://docs.microsoft.com/en-us/dotnet/api/system.datetimeoffset)<br>

### **CreateDtm**



```csharp
public DateTimeOffset CreateDtm { get;  set; }
```

#### Property Value

[DateTimeOffset](https://docs.microsoft.com/en-us/dotnet/api/system.datetimeoffset)<br>

### **ModifyDtm**



```csharp
public DateTimeOffset ModifyDtm { get;  set; }
```

#### Property Value

[DateTimeOffset](https://docs.microsoft.com/en-us/dotnet/api/system.datetimeoffset)<br>

### **Type**



```csharp
public Smb2EntryType Type { get;  set; }
```

#### Property Value

[Smb2EntryType](./foxhollow.libsmb2sharp.smb2entrytype.md)<br>

### **Share**



```csharp
public Smb2Share Share { get; }
```

#### Property Value

[Smb2Share](./foxhollow.libsmb2sharp.smb2share.md)<br>

### **Context**



```csharp
public Smb2Context Context { get; }
```

#### Property Value

[Smb2Context](./foxhollow.libsmb2sharp.smb2context.md)<br>

## Constructors

### **Smb2FileEntry()**



```csharp
public Smb2FileEntry()
```

## Methods

### **Dispose()**



```csharp
public void Dispose()
```

### **OpenReader()**



```csharp
public Smb2FileReader OpenReader()
```

#### Returns

[Smb2FileReader](./foxhollow.libsmb2sharp.smb2filereader.md)<br>

### **OpenWriter()**



```csharp
public Smb2FileWriter OpenWriter()
```

#### Returns

[Smb2FileWriter](./foxhollow.libsmb2sharp.smb2filewriter.md)<br>

### **Move(String)**



```csharp
public void Move(string newPath)
```

#### Parameters

`newPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Remove()**



```csharp
public void Remove()
```

### **LockFile()**



```csharp
internal void LockFile()
```

### **UnlockFile()**



```csharp
internal void UnlockFile()
```
