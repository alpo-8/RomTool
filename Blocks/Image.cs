using RomTool.Headers;
using System.Runtime.InteropServices;
using static RomTool.Util;

namespace RomTool.Blocks
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public struct Image
    {
        [MarshalAs(UnmanagedType.Struct, SizeConst = 0x1000)]
        public DescRegion Descriptor;

        public byte[] ME;
        public byte[] DevExp;
        public byte[] GbE;
        public byte[] PTT;

        [MarshalAs(UnmanagedType.Struct)]
        public BiosRegion BIOS;

        public Image(byte[] data)
        {
            Descriptor = ByteArrayToStruct<DescRegion>(data[..0x1000]);
            ME = data[Descriptor.MeRange];
            DevExp = data[Descriptor.DevExpRange];
            GbE = data[Descriptor.GbeRange];
            PTT = data[Descriptor.PttRange];
            BIOS = new BiosRegion(data[Descriptor.PttRange.End..]);
        }
    }
}
