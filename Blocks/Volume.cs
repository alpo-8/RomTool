using System;
using System.Linq;
using RomTool.Headers;
using RomTool.Interfaces;
using System.Collections.Generic;
using static RomTool.Util;

namespace RomTool.Blocks
{
    public class Volume
    {
        public IHeader Header;
        public byte[] Body;

        public int Size => Header.FullSize;

        public Volume(byte[] data)
        {
            Header = ByteArrayToStruct<VolumeHeader>(data);
            Body = data[Header.Size..Size];
            InitFiles();
        }

        public List<File> Files = new List<File>();

        private void InitFiles()
        {
            int i = 0;
            while (i < Body.LongLength - 0x50 && !Body.Sub(i,0x50).SequenceEqual(EmPad))
            {
                Files.Add(new File(Body[i..]));
                i += Files.Last().Header.FullSize;
            }
        }
    }
}