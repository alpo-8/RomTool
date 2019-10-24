namespace RomTool.Interfaces
{
    public interface IBlock
    {
        public int Size { get; }
    }

    public abstract class Block : IBlock
    {
        public byte[] Body;

        public int Size => Body.Length;
    }

    public abstract class HeadedBlock : Block
    {
        public IHeader Header;
    }
}