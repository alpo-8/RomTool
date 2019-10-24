using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace RomTool
{
    public static class Util
    {
        public static byte[] EmPad = Enumerable.Repeat((byte)0xFF, 0x50).ToArray();

        public static T[] Sub<T>(this T[] data, int index, int? length = null)
        {
            var result = new T[length ?? data.Length - index];
            Array.Copy(data, index, result, 0, result.Length);
            return result;
        }

        public static unsafe T ByteArrayToStruct<T>(byte[] data) where T : struct
        {
            fixed (byte* ptr = &data[0])
                return Marshal.PtrToStructure<T>((IntPtr)ptr);
        }

        /*
        public static T ByteArrayToStructSafe<T>(byte[] data) where T : struct
        {
            T stuff;
            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try
            {
                stuff = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
            return stuff;
        }
        */
    }
}
