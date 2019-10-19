using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RomTool
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1, Size = 0x4)]
    public struct BaseSectionHeader : ISectionHeader
    {
        [MarshalAs(UnmanagedType.Struct)]
        private Uint24 fullSize;
        private byte type;
        
        public uint FullSize => fullSize.Value;
        public uint Size => (uint) GetType().StructLayoutAttribute.Size;
        public SectionType Type => (SectionType) type;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1, Size = 0x6)]
    public struct VersionSectionHeader : ISectionHeader
    {
        [MarshalAs(UnmanagedType.Struct)]
        private Uint24 fullSize;
        private byte type;
        public ushort BuildNumber;
        
        public uint FullSize => fullSize.Value;
        public uint Size => (uint) GetType().StructLayoutAttribute.Size;
        public SectionType Type => (SectionType) type;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1, Size = 0x14)]
    public struct GuidSectionHeader : ISectionHeader
    {
        [MarshalAs(UnmanagedType.Struct)]
        private Uint24 fullSize;
        private byte type;
        public Guid SectionGuid;
        
        public uint FullSize => fullSize.Value;
        public uint Size => (uint) GetType().StructLayoutAttribute.Size;
        public SectionType Type => (SectionType) type;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1, Size = 0x18)]
    public struct GuidDefinedSectionHeader : ISectionHeader
    {
        [MarshalAs(UnmanagedType.Struct)]
        private Uint24 fullSize;
        private byte type;
        public Guid SectionGuid;
        public ushort DataOffset;
        public ushort Attributes;
        
        public uint FullSize => fullSize.Value;
        public uint Size => (uint) GetType().StructLayoutAttribute.Size;
        public SectionType Type => (SectionType) type;
    }
    
    public class Section
    {
        public ISectionHeader Header;
        public byte[] Body;
        
        public Section(byte[] data)
        {
            Header = Utils.ByteArrayToStruct<BaseSectionHeader>(data);
            switch (Header.Type)
            {
                case SectionType.Version:
                    Header = Utils.ByteArrayToStruct<VersionSectionHeader>(data);
                    break;
                case SectionType.FreeformGuid:
                    Header = Utils.ByteArrayToStruct<GuidSectionHeader>(data);
                    break;
                case SectionType.GuidDefined:
                    Header = Utils.ByteArrayToStruct<GuidDefinedSectionHeader>(data);
                    InitSubSections();
                    break;
            }

            Body = data.SubArray(Header.Size, Header.FullSize - Header.Size);
        }

        public Dictionary<uint, Section> SubSections = new Dictionary<uint, Section>();

        private void InitSubSections()
        {
            uint i = 0;
            var dat = new Lzma().Decompress(Body).Result;
            while (i < dat.LongLength - 0x4)
            {
                SubSections.Add(i, new Section(dat.SubArray(i)));
                i += SubSections[i].Header.FullSize;
            }
        }
        
    }
}