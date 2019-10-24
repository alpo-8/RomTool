using System.Collections.Generic;
using RomTool.Interfaces;
using static RomTool.Util;

namespace RomTool.Blocks
{
    public class Block : IBlock
    {
        public byte[] Body;

        public int Size => Body.Length;

        public Block(byte[] data) => Body = data;
    }

    public abstract class Block<T> : Block where T : class
    {
        public Block(byte[] data) : base(data) { }

        public List<T> Subs = new List<T>();

        public abstract void InitSubs();
    }

    public abstract class HeadedBlock<U, T> : Block<T> where U : struct, IHeader where T : class
    {
        public U Header;

        public new int Size => Header.FullSize;

        public HeadedBlock(byte[] data) : base(data)
        {
            Header = ByteArrayToStruct<U>(data);
            Body = data[Header.Size..Size];
            InitSubs();
        }
    }
}
