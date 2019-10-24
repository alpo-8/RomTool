using System;
using System.Linq;
using RomTool.Data;
using System.Collections.Generic;

namespace RomTool.Blocks
{
    public class BiosRegion
    {
        public byte[] Body;

        public int Size => Body.Length;

        public BiosRegion(byte[] data)
        {
            Body = data;
            InitVolumes();
        }

        public List<Volume> Volumes = new List<Volume>();

        public void InitVolumes()
        {
            for (var i = 0; i < Size - 0x20; i += 0x1000)
                if (new Guid(Body.Sub(0x10, 0x10)).Equals(GuidStore.FsFfSv2))
                {
                    Volumes.Add(new Volume(Body[i..]));
                    i += 0x1000 * (Volumes.Last().Size / 0x1000);
                }
        }
    }
}
