using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace RomTool
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public struct Image
    {
        [MarshalAs(UnmanagedType.Struct, SizeConst = 0x1000)]
        public DescRegion Descriptor;
        
        public byte[] ME;
        public byte[] DevExp1;
        public byte[] GbE1;
        public byte[] PTT;
        
        [MarshalAs(UnmanagedType.Struct)]
        public BiosRegion BIOS;

        public Image(byte[] data)
        {
            Descriptor = Utils.ByteArrayToStruct<DescRegion>(data.SubArray(0, 0x1000));
            ME = data.SubArray(Descriptor.MeStart, Descriptor.MeSize);
            DevExp1 = data.SubArray(Descriptor.DevExp1Start, Descriptor.DevExp1Size);
            GbE1 = data.SubArray(Descriptor.Gbe1Start, Descriptor.Gbe1Size);
            PTT = data.SubArray(Descriptor.PttStart, Descriptor.PttSize);
            BIOS = new BiosRegion( data.SubArray(Descriptor.BiosStart) );
        }
    }
    
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode, Pack = 1, Size = 0x1000)]
    public class DescRegion : IBlock
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        [FieldOffset(0x10)]
        public byte[] Identifier = {0x5a, 0xa5, 0xf0, 0x0f};
        
        [FieldOffset(0x48)]
        private ushort meStart;    // 0x1000
        
        [FieldOffset(0x4a)]
        private ushort meEnd;    // 0xA25FFF
        
        [FieldOffset(0x54)]
        private ushort devExp1Start;    // 0xA26000
        
        [FieldOffset(0x56)]
        private ushort devExp1End;    // 0xA35FFF
        
        [FieldOffset(0x6c)]
        private ushort gbe1Start;    // 0xA36000
        
        [FieldOffset(0x6e)] 
        private ushort gbe1End;    // 0xFEFFFF
        
        [FieldOffset(0x7c)]
        private ushort pttStart;    // 0xFF0000
        
        [FieldOffset(0x7e)]
        private ushort pttEnd;    // 0xFFFFFF
        
        public uint Size => (uint) GetType().StructLayoutAttribute.Size;
        public uint MeSize => (uint) (meEnd - meStart + 1) * Size;
        public uint MeStart => meStart * Size;
        public uint DevExp1Size => (uint) (devExp1End - devExp1Start + 1) * Size;
        public uint DevExp1Start => devExp1Start * Size;
        public uint Gbe1Size => (uint) (gbe1End - gbe1Start + 1) * Size;
        public uint Gbe1Start => gbe1Start * Size;
        public uint PttSize => (uint) (pttEnd - pttStart + 1) * Size;
        public uint PttStart => pttStart * Size;
        public uint BiosStart => (uint) (pttEnd + 1) * Size;
    }
    
    public class BiosRegion : IBlock
    {
        public byte[] Body;
        
        public uint Size => (uint) Body.LongLength;

        public BiosRegion(byte[] data)
        {
            Body = data;
            InitVolumes();
            InitPaddings();
        }
        
        // dict with offset & length of empty paddings
        public Dictionary<uint, uint> Paddings = new Dictionary<uint, uint>();

        private void InitPaddings()
        {
            for (uint i = 0; i < Size - 0x50; i+=0x50)
                if (Body.SubArray(i, 0x50).SequenceEqual(Utils.EmPad))
                {
                    if (Paddings.ContainsKey(i - 1))
                        Paddings[i - 0x50] += 0x50;
                    else
                        Paddings.Add(i, 0x50);
                }
        }

        public Dictionary<uint, Volume> Volumes = new Dictionary<uint, Volume>();

        private void InitVolumes()
        {
            for (uint i = 0; i < Size - 0x20; i += 0x1000)
                if (new Guid(Body.SubArray(i + 0x10, 0x10)).Equals(GuidStore.Fs_FFSv2))
                {
                    Volumes.Add(i, new Volume(Body.SubArray(i)));
                    i += 0x1000 * (Volumes[i].Header.FullSize / 0x1000 - 1);
                }
        }
    }
}
