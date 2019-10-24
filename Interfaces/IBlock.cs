using static RomTool.Util;

namespace RomTool.Interfaces
{
    public interface IBlock
    {
        int Size { get; }
    }

    public abstract class Block : IBlock
    {
        public byte[] Body;

        public int Size => Body.Length;

        protected Block(byte[] data)
        {
            Body = data;
            InitActivity();
        }

        public abstract void InitActivity();
    }

    public abstract class HeadedBlock<T> : Block where T : struct, IHeader
    {
        public T Header;

        public new int Size => Header.FullSize;

        public HeadedBlock(byte[] data) : base(data)
        {
            Header = ByteArrayToStruct<T>(data);
            
            Body = data[Header.Size..Size];
            InitActivity();
        }
    }

    
}