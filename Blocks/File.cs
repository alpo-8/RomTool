using System.Linq;
using RomTool.Headers;
using RomTool.Interfaces;
using System.Collections.Generic;
using static RomTool.Util;

namespace RomTool.Blocks
{
    public class File
    {
        public IHeader Header;
        public byte[] Body;

        public int Size => Header.FullSize;

        public File(byte[] data)
        {
            Header = Util.ByteArrayToStruct<FileHeader>(data);
            Body = data[Header.Size..Size];
            InitSections();
        }

        public List<Section> Sections = new List<Section>();

        private void InitSections()
        {
            for (var i = 0; i < Body.LongLength - 0x4; i++)
            {
                Sections.Add(new Section(Body[i..]));
                i += Sections.Last().Header.FullSize;
            }
        }
    }
}