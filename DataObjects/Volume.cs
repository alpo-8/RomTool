using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace RomTool
{
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode, Pack = 1, Size = 0x78)]
    public struct VolumeHeader
    {
        [FieldOffset(0x10)]
        public Guid FsGuid;

        [FieldOffset(0x20)]
        private Uint24 fullSize;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        [FieldOffset(0x28)]
        public byte[] Signature;
        
        //[FieldOffset(0x2c)] public uint Attributes;

        [FieldOffset(0x32)]
        public ushort Checksum;
        
        [FieldOffset(0x60)]
        public Guid VolumeGuid;
        
        public uint FullSize => fullSize.Value;
        public uint Size => (uint) GetType().StructLayoutAttribute.Size;
    }
    
    public class Volume
    {
        [MarshalAs(UnmanagedType.Struct)]
        public VolumeHeader Header;
        
        public byte[] Body;
        
        public Volume(byte[] data)
        {
            Header = Utils.ByteArrayToStruct<VolumeHeader>(data);
            Body = data.SubArray(Header.Size, Header.FullSize-Header.Size);
            InitFiles();
        }

        public Dictionary<uint, File> Files = new Dictionary<uint, File>();

        private void InitFiles()
        {
            var filler = false;
            uint i = 0;
            while (i < Body.LongLength - 0x50 && !filler)
            {
                if (Body.SubArray(i, 0x50).SequenceEqual(Utils.EmPad))
                    filler = true;
                else
                {
                    Files.Add(i, new File(Body.SubArray(i)));
                    i += Files[i].Header.FullSize;
                }
            }
        }
    }
}