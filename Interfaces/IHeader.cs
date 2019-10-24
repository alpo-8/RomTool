using RomTool.Data;

namespace RomTool.Interfaces
{
    public interface IHeader
    {
        static UInt24 fullSize;
        int FullSize => fullSize;
        int Size => GetType().StructLayoutAttribute.Size;
    }

    public interface ITypedHeader : IHeader
    {
        static byte type;
        BlockType Type => (BlockType)type;
    }
}