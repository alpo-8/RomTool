using System;
using System.Runtime.InteropServices;
using RomTool.Interfaces;

namespace RomTool.Headers
{
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode, Pack = 1, Size = 0x1000)]
    public struct DescRegion : IHeader
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        [FieldOffset(0x10)]
        public byte[] Identifier; // {0x5a, 0xa5, 0xf0, 0x0f};

        [FieldOffset(0x48)]
        private ushort meStart;    // 0x1(000)

        [FieldOffset(0x4a)]
        private ushort meEnd;    // 0xA25(FFF)

        [FieldOffset(0x54)]
        private ushort devExpStart;    // 0xA26(000)

        [FieldOffset(0x56)]
        private ushort devExpEnd;    // 0xA35(FFF)

        [FieldOffset(0x6c)]
        private ushort gbeStart;    // 0xA36(000)

        [FieldOffset(0x6e)]
        private ushort gbeEnd;    // 0xFEF(FFF)

        [FieldOffset(0x7c)]
        private ushort pttStart;    // 0xFF0(000)

        [FieldOffset(0x7e)]
        private ushort pttEnd;    // 0xFFF(FFF)
        
        public Range MeRange => Multiplied(meStart, meEnd);
        public Range DevExpRange => Multiplied(devExpStart, devExpEnd);
        public Range GbeRange => Multiplied(gbeStart, gbeEnd);
        public Range PttRange => Multiplied(pttStart, pttEnd);

        Range Multiplied(int first, int last)
            => (first * 0x1000)..((last + 1) * 0x1000);
    }
}
