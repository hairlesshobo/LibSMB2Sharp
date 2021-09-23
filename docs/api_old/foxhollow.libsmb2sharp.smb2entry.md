# Smb2Entry

Namespace: FoxHollow.LibSMB2Sharp



```csharp
public abstract class Smb2Entry
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Smb2Entry](./foxhollow.libsmb2sharp.smb2entry.md)

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

### **SetDetailsFromDirEnt(smb2dirent&)**



```csharp
protected void SetDetailsFromDirEnt(smb2dirent& dirEnt)
```

#### Parameters

`dirEnt` [smb2dirent&](./foxhollow.libsmb2sharp.native.smb2dirent&.md)<br>

### **GetParentDirectory()**



```csharp
public Smb2DirectoryEntry GetParentDirectory()
```

#### Returns

[Smb2DirectoryEntry](./foxhollow.libsmb2sharp.smb2directoryentry.md)<br>

### **RefreshDetails()**



```csharp
public void RefreshDetails()
```

### **Move(String, Boolean, Boolean)**



```csharp
protected void Move(string newPath, bool movingFile, bool isRename)
```

#### Parameters

`newPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`movingFile` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`isRename` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Rename(String)**



```csharp
public void Rename(string newName)
```

#### Parameters

`newName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ToString()**



```csharp
public string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
