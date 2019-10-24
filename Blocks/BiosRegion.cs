using System;
using System.Linq;
using RomTool.Data;
using System.Collections.Generic;
using RomTool.Interfaces;

namespace RomTool.Blocks
{
    public class BiosRegion : Block<Volume>
    {
        public BiosRegion(byte[] data) : base(data) { }
        
        public override void InitSubs()
        {
            for (var i = 0; i < Size - 0x20; i += 0x1000)
                if (new Guid(Body.Sub(0x10, 0x10)).Equals(GuidStore.FsFfSv2))
                {
                    Subs.Add(new Volume(Body[i..]));
                    i += 0x1000 * (Subs.Last().Size / 0x1000);
                }
        }
    }
}
