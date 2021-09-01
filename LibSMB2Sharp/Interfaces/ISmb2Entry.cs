using System;

namespace LibSMB2Sharp.Interfaces
{
    public interface ISmb2Entry : IDisposable
    {
        string Name { get; }
        ulong Size { get; }
        DateTimeOffset CreateDtm { get; }
        DateTimeOffset ModifyDtm { get; }
        Smb2EntryType Type { get; }
    }
}