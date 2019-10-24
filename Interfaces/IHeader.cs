using RomTool.Data;

namespace RomTool.Interfaces
{
    public interface IHeader
    {
        static UInt24 fullSize;
        int FullSize => FullSize;
        int Size => GetType().StructLayoutAttribute.Size;
    }

    public interface ITypedHeader : IHeader
    {
        byte type { get; }
        BlockType Type => (BlockType)type;
    }
}