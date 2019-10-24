using System;
using RomTool.Interfaces;
using System.Runtime.InteropServices;

namespace RomTool.Headers
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1, Size = 0x18)]
    public struct FileHeader : IHeader
    {
        public Guid FileGuid;

        public byte HeaderChecksum;

        public byte DataChecksum;

        public byte Type;

        public byte Attributes;

        [MarshalAs(UnmanagedType.Struct)]
        public UInt24 fullSize;

        public byte State;
    }
}
