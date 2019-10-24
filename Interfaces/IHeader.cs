using System.Runtime.CompilerServices;
using RomTool.Data;

namespace RomTool.Interfaces
{
    public interface IHeader
    {
        static UInt24 _fullSize;

        int FullSize 
            => _fullSize;

        int Size 
            => GetType().StructLayoutAttribute.Size;

        static void Load(UInt24 fullSize) 
            => _fullSize = fullSize;
    }

    public interface ITypedHeader : IHeader
    {
        static byte _type;

        BlockType Type 
            => (BlockType)_type;

        static void Load(UInt24 fullSize, byte type)
        {
            Load(fullSize);
            _type = type;
        }
    }
}