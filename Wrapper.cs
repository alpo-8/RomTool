namespace RomTool
{
    public class Wrapper<T> where T : struct
    {
        public T Value;

        public Wrapper(T value) => Value = value;

        public static implicit operator Wrapper<T>(T value)
            => new Wrapper<T>(value);

        public static implicit operator Wrapper<T>(byte[] data)
            => new Wrapper<T>(Util.ByteArrayToStruct<T>(data));

        public static explicit operator T(Wrapper<T> value)
            => value.Value;
        /*
        private Uint24 fullSize;
        private byte type;
        public uint FullSize => fullSize.Value;
        public uint Size => (uint) Value.GetType().StructLayoutAttribute.Size;
        public BlockType Type =>  (BlockType) type;
        */
    }
}
