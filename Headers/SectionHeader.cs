using System;
using RomTool.Interfaces;
using System.Runtime.InteropServices;

namespace RomTool.Headers
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1, Size = 0x4)]
    public struct BaseSectionHeader : IHeader
    {
        [MarshalAs(UnmanagedType.Struct)]
        public UInt24 fullSize;

        public byte Type;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1, Size = 0x6)]
    public struct VersionSectionHeader : IHeader
    {
        [MarshalAs(UnmanagedType.Struct)]
        public UInt24 fullSize;

        public byte Type;

        public ushort BuildNumber;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1, Size = 0x14)]
    public struct GuidSectionHeader : IHeader
    {
        [MarshalAs(UnmanagedType.Struct)]
        public UInt24 fullSize;

        public byte Type;

        public Guid SectionGuid;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1, Size = 0x18)]
    public struct GuidDefinedSectionHeader : IHeader
    {
        [MarshalAs(UnmanagedType.Struct)]
        public UInt24 fullSize;

        public byte Type;

        public Guid SectionGuid;

        public ushort DataOffset;

        public ushort Attributes;
    }
}
