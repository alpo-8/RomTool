using System.Linq;
using RomTool.Data;
using RomTool.Headers;
using RomTool.Interfaces;
using System.Collections.Generic;
using static RomTool.Util;

namespace RomTool.Blocks
{
    public class Section
    {
        public IHeader Header;

        public byte[] Body;

        public int Size => Header.FullSize;

        public Section(byte[] data)
        {
            Header = (BlockType)data[3] switch
            {
                BlockType.Version => ByteArrayToStruct<VersionSectionHeader>(data),
                BlockType.FreeformGuid => ByteArrayToStruct<GuidSectionHeader>(data),
                BlockType.GuidDefined => ByteArrayToStruct<GuidDefinedSectionHeader>(data),
                _ => ByteArrayToStruct<BaseSectionHeader>(data)
            };
            if (Header is GuidDefinedSectionHeader)
                InitSubSections();

            Body = data[Header.Size..Size];
        }

        public List<Section> SubSections = new List<Section>();

        private void InitSubSections()
        {
            var dat = Lzma.Decompress(Body);
            for (var i = 0; i < dat.Length - 0x4; i++)
            {
                SubSections.Add(new Section(dat[i..]));
                i += SubSections.Last().Size;
            }
        }

    }
}