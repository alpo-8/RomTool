using System;
using System.Linq;
using System.Runtime.InteropServices;
using static System.Math;

namespace RomTool
{
    public static class Utils
    {
        public static byte[] EmPad = Enumerable.Repeat((byte) 0xFF, 0x50).ToArray();
        
        public static T[] SubArray<T>(this T[] data, uint index, uint? length = null)
        {
            var rLength = Min(length ?? data.LongLength - index, data.LongLength - index);
            var result = new T[rLength];
            Array.Copy(data, index, result, 0, rLength);
            return result;
        }
        
        public static unsafe T ByteArrayToStruct<T>(byte[] data) // where T : struct
        {
            fixed (byte* ptr = &data[0])
                return Marshal.PtrToStructure<T>((IntPtr) ptr);
        }
        
        public static T ByteArrayToStruct2<T>(byte[] data) // where T : struct
        {
            T stuff;
            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try
            {
                stuff = (T) Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
            return stuff;
        }
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1, Size = 3)]
    public struct Uint24
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        byte[] v;
        
        public uint Value => (uint) (v[0] + v[1] * 0x100 + v[2] * 0x10000);
    }
}