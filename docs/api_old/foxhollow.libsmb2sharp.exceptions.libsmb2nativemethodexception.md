# LibSmb2NativeMethodException

Namespace: FoxHollow.LibSMB2Sharp.Exceptions



```csharp
public class LibSmb2NativeMethodException : System.Exception, System.Runtime.Serialization.ISerializable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception) → [LibSmb2NativeMethodException](./foxhollow.libsmb2sharp.exceptions.libsmb2nativemethodexception.md)<br>
Implements [ISerializable](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iserializable)

## Properties

### **ErrorCode**



```csharp
public int ErrorCode { get; private set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **TargetSite**



```csharp
public MethodBase TargetSite { get; }
```

#### Property Value

[MethodBase](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase)<br>

### **StackTrace**



```csharp
public string StackTrace { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Message**



```csharp
public string Message { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Data**



```csharp
public IDictionary Data { get; }
```

#### Property Value

[IDictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.idictionary)<br>

### **InnerException**



```csharp
public Exception InnerException { get; }
```

#### Property Value

[Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br>

### **HelpLink**



```csharp
public string HelpLink { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Source**



```csharp
public string Source { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **HResult**



```csharp
public int HResult { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **LibSmb2NativeMethodException(String)**



```csharp
public LibSmb2NativeMethodException(string message)
```

#### Parameters

`message` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **LibSmb2NativeMethodException(IntPtr, String)**



```csharp
public LibSmb2NativeMethodException(IntPtr smb2, string message)
```

#### Parameters

`smb2` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

`message` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **LibSmb2NativeMethodException(IntPtr, Int32)**



```csharp
public LibSmb2NativeMethodException(IntPtr smb2, int result)
```

#### Parameters

`smb2` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

`result` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **LibSmb2NativeMethodException(Smb2Context, Int32)**



```csharp
public LibSmb2NativeMethodException(Smb2Context context, int result)
```

#### Parameters

`context` [Smb2Context](./foxhollow.libsmb2sharp.smb2context.md)<br>

`result` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
