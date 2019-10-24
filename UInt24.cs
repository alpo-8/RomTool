using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace RomTool
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1, Size = 3)]
    public struct UInt24
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        byte[] v;

        public UInt24(byte[] vIn)
            => v = vIn[..3];

        private int Value
            => v[0] + v[1] * 0x100 + v[2] * 0x10000;

        public static implicit operator int(UInt24 u)
            => u.Value;

        public static implicit operator UInt24(int i)
            => new UInt24(BitConverter.GetBytes(i).Take(3).ToArray());
    }
}
