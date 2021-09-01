using System;
using LibSMB2Sharp.Interfaces;

namespace LibSMB2Sharp
{
    public enum Smb2EntryType
    {
        File = 0,
        Directory = 1
    }

    public class Smb2Entry : ISmb2Entry
    {
        public string Name { get; internal protected set; }
        public string RelativePath { get; internal protected set; }

        public ulong Size { get; internal protected set; }

        public DateTimeOffset CreateDtm { get; internal protected  set; }

        public DateTimeOffset ModifyDtm { get; internal protected set; }
        public Smb2EntryType Type { get; internal protected set; }

    }
}