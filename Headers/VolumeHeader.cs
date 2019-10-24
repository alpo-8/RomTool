using System;
using RomTool.Interfaces;
using System.Runtime.InteropServices;

namespace RomTool.Headers
{
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode, Pack = 1, Size = 0x78)]
    public struct VolumeHeader : IHeader
    {
        [FieldOffset(0x10)]
        private Guid FsGuid;

        [MarshalAs(UnmanagedType.Struct)]
        [FieldOffset(0x20)]
        public UInt24 fullSize;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        [FieldOffset(0x28)]
        private byte[] Signature;

        [FieldOffset(0x32)]
        private ushort Checksum;

        [FieldOffset(0x60)]
        private Guid VolumeGuid;
    }
}
