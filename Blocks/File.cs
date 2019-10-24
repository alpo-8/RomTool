using System.Linq;
using RomTool.Headers;
using RomTool.Interfaces;
using System.Collections.Generic;
using static RomTool.Util;

namespace RomTool.Blocks
{
    public class File : HeadedBlock<FileHeader, Section>
    {
        public File(byte[] data) : base(data) { }

        public override void InitSubs()
        {
            for (var i = 0; i < Body.LongLength - 0x4; i++)
            {
                Subs.Add(new Section(Body[i..]));
                i += Subs.Last().Size;
            }
        }
    }
}