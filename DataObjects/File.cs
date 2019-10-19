using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace RomTool
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1, Size = 0x18)]
    public struct FileHeader
    {
        public Guid FileGuid;
        public byte HeaderChecksum;
        public byte DataChecksum;
        private byte type;
        public byte Attributes;
        private Uint24 fullSize;
        public byte State;
        
        public uint FullSize => fullSize.Value;
        public uint Size => (uint) GetType().StructLayoutAttribute.Size;
        public FileType Type => (FileType) type;
    }
    
    public class File
    {
        [MarshalAs(UnmanagedType.Struct)]
        public FileHeader Header;
        
        public byte[] Body;
        
        public File(byte[] data)
        {
            Header = Utils.ByteArrayToStruct<FileHeader>(data);
            Body = data.SubArray(Header.Size, Header.FullSize - Header.Size);
            InitSections();
        }

        public Dictionary<uint, Section> Sections = new Dictionary<uint, Section>();

        private void InitSections()
        {
            uint i = 0;
            while (i < Body.LongLength - 0x4)
            {
                Sections.Add(i, new Section(Body.SubArray(i)));
                i += Sections[i].Header.FullSize;
            }
        }
    }
}