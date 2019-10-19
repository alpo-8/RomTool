using System;

namespace RomTool
{
    /*
     
    public abstract class TypedHeader<T> : BaseHeader where T : Enum
    {
        private byte type;
        public T Type => (T)Enum.GetValues(typeof(T)).GetValue(type);
    }

    public class StructWrapper<T> where T : struct { }
    
    */
    
    public interface ISectionHeader : IBlock
    {
        uint FullSize { get; }
        SectionType Type { get; }
    }
}