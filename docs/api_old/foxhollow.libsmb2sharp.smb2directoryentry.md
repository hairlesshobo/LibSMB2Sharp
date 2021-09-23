# Smb2DirectoryEntry

Namespace: FoxHollow.LibSMB2Sharp



```csharp
public class Smb2DirectoryEntry : Smb2Entry, System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Smb2Entry](./foxhollow.libsmb2sharp.smb2entry.md) → [Smb2DirectoryEntry](./foxhollow.libsmb2sharp.smb2directoryentry.md)<br>
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

## Methods

### **GetEntries(Boolean)**



```csharp
public IEnumerable<Smb2Entry> GetEntries(bool ignoreSelfAndParent)
```

#### Parameters

`ignoreSelfAndParent` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[IEnumerable&lt;Smb2Entry&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

### **CreateFile(String)**



```csharp
public Smb2FileEntry CreateFile(string name)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Smb2FileEntry](./foxhollow.libsmb2sharp.smb2fileentry.md)<br>

### **GetFile(String, Boolean)**



```csharp
public Smb2FileEntry GetFile(string name, bool createNew)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`createNew` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[Smb2FileEntry](./foxhollow.libsmb2sharp.smb2fileentry.md)<br>

### **GetDirectory(String, Boolean)**



```csharp
public Smb2DirectoryEntry GetDirectory(string name, bool throwOnMissing)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`throwOnMissing` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[Smb2DirectoryEntry](./foxhollow.libsmb2sharp.smb2directoryentry.md)<br>

### **GetEntry(String, Boolean)**



```csharp
public Smb2Entry GetEntry(string name, bool throwOnMissing)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`throwOnMissing` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[Smb2Entry](./foxhollow.libsmb2sharp.smb2entry.md)<br>

### **Remove()**



```csharp
public void Remove()
```

### **CreateDirectory(String)**



```csharp
public Smb2DirectoryEntry CreateDirectory(string name)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Smb2DirectoryEntry](./foxhollow.libsmb2sharp.smb2directoryentry.md)<br>

### **Move(String)**



```csharp
public void Move(string newPath)
```

#### Parameters

`newPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Dispose()**



```csharp
public void Dispose()
```

### **CreateOrTruncateFile(String)**



```csharp
internal void CreateOrTruncateFile(string name)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **GetDirEnt(String, Boolean)**



```csharp
internal Nullable<smb2dirent> GetDirEnt(string name, bool throwOnMissing)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`throwOnMissing` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[Nullable&lt;smb2dirent&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
