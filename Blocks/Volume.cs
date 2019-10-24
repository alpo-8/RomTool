using System;
using System.Linq;
using RomTool.Headers;
using RomTool.Interfaces;
using System.Collections.Generic;
using static RomTool.Util;

namespace RomTool.Blocks
{
    public class Volume : HeadedBlock<VolumeHeader, File>
    {
        public Volume(byte[] data) : base(data) { }

        public override void InitSubs()
        {
            int i = 0;
            while (i < Body.LongLength - 0x50 && !Body.Sub(i,0x50).SequenceEqual(EmPad))
            {
                Subs.Add(new File(Body[i..]));
                i += Subs.Last().Size;
            }
        }
    }
}