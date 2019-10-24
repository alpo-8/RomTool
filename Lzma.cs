using System;
using System.IO;
using SevenZip.Compression.LZMA;

namespace RomTool
{
    public class Lzma
    {
        public static byte[] Compress(byte[] value)
        {
            using var sourceStream = new MemoryStream(value);
            using var destStream = new MemoryStream();
            var coder = new Encoder();
            
            coder.WriteCoderProperties(destStream);
            
            destStream.Write(BitConverter.GetBytes(sourceStream.Length), 0, 8);
            
            coder.Code(sourceStream, destStream, sourceStream.Length, -1, null);

            destStream.Flush();
            return destStream.ToArray();
        }
        
        public static byte[] Decompress(byte[] value)
        {
            using var sourceStream = new MemoryStream(value);
            using var destStream = new MemoryStream();
            var coder = new Decoder();
            
            var properties = new byte[5];
            sourceStream.Read(properties, 0, 5);
            
            var fileLengthBytes = new byte[8];
            sourceStream.Read(fileLengthBytes, 0, 8);
            var fileLength = BitConverter.ToInt64(fileLengthBytes, 0);
            
            coder.SetDecoderProperties(properties);
            coder.Code(sourceStream, destStream, sourceStream.Length, fileLength, null);
            
            destStream.Flush();
            return destStream.ToArray();
        }
    }
}