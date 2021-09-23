# Smb2Share

Namespace: FoxHollow.LibSMB2Sharp



```csharp
public class Smb2Share : Smb2DirectoryEntry, System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Smb2Entry](./foxhollow.libsmb2sharp.smb2entry.md) → [Smb2DirectoryEntry](./foxhollow.libsmb2sharp.smb2directoryentry.md) → [Smb2Share](./foxhollow.libsmb2sharp.smb2share.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)

## Properties

### **FsBlockSize**



```csharp
public uint FsBlockSize { get; private set; }
```

#### Property Value

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **FsFragementSize**



```csharp
public uint FsFragementSize { get; private set; }
```

#### Property Value

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **FsBlocks**



```csharp
public ulong FsBlocks { get; private set; }
```

#### Property Value

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **FsBlocksFree**



```csharp
public ulong FsBlocksFree { get; private set; }
```

#### Property Value

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **FsBlocksAvailable**



```csharp
public ulong FsBlocksAvailable { get; private set; }
```

#### Property Value

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **FsFiles**



```csharp
public uint FsFiles { get; private set; }
```

#### Property Value

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **FsFilesFree**



```csharp
public uint FsFilesFree { get; private set; }
```

#### Property Value

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **FsFilesAvailable**



```csharp
public uint FsFilesAvailable { get; private set; }
```

#### Property Value

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **FsID**



```csharp
public uint FsID { get; private set; }
```

#### Property Value

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **FsFlag**



```csharp
public uint FsFlag { get; private set; }
```

#### Property Value

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **FsNameMax**



```csharp
public uint FsNameMax { get; private set; }
```

#### Property Value

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **Context**



```csharp
public Smb2Context Context { get; }
```

#### Property Value

[Smb2Context](./foxhollow.libsmb2sharp.smb2context.md)<br>

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

## Methods

### **RefreshVfsDetails()**



```csharp
public void RefreshVfsDetails()
```

### **CreateDirectoryTree(String)**



```csharp
public Smb2DirectoryEntry CreateDirectoryTree(string path)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Smb2DirectoryEntry](./foxhollow.libsmb2sharp.smb2directoryentry.md)<br>

### **GetDirectory()**



```csharp
public Smb2DirectoryEntry GetDirectory()
```

#### Returns

[Smb2DirectoryEntry](./foxhollow.libsmb2sharp.smb2directoryentry.md)<br>

### **GetDirectory(String, Boolean)**



```csharp
public Smb2DirectoryEntry GetDirectory(string path, bool throwOnMissing)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`throwOnMissing` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[Smb2DirectoryEntry](./foxhollow.libsmb2sharp.smb2directoryentry.md)<br>

### **CreateFile(String)**



```csharp
public Smb2FileEntry CreateFile(string path)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Smb2FileEntry](./foxhollow.libsmb2sharp.smb2fileentry.md)<br>

### **GetFile(String, Boolean)**



```csharp
public Smb2FileEntry GetFile(string path, bool createNew)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`createNew` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[Smb2FileEntry](./foxhollow.libsmb2sharp.smb2fileentry.md)<br>

### **GetEntry(String, Boolean)**



```csharp
public Smb2Entry GetEntry(string path, bool throwOnMissing)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`throwOnMissing` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[Smb2Entry](./foxhollow.libsmb2sharp.smb2entry.md)<br>

### **CreateOrTruncateFile(String)**



```csharp
internal void CreateOrTruncateFile(string path)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **GetDirEnt(String, Boolean)**



```csharp
internal Nullable<smb2dirent> GetDirEnt(string path, bool throwOnMissing)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`throwOnMissing` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[Nullable&lt;smb2dirent&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Dispose()**



```csharp
public void Dispose()
```

### **RegisterOpenDirectory(Smb2DirectoryEntry)**



```csharp
internal void RegisterOpenDirectory(Smb2DirectoryEntry entry)
```

#### Parameters

`entry` [Smb2DirectoryEntry](./foxhollow.libsmb2sharp.smb2directoryentry.md)<br>
