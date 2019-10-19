using System;
using System.IO;
using System.Threading.Tasks;
using SevenZip.Compression.LZMA;

namespace RomTool
{
    public class Lzma
    {
        public async Task<byte[]> Compress(byte[] value)
        {
            await using var sourceStream = new MemoryStream(value);
            await using var destStream = new MemoryStream();
            var coder = new Encoder();

            // Write the encoder properties
            coder.WriteCoderProperties(destStream);

            // Write the decompressed file size.
            destStream.Write(BitConverter.GetBytes(sourceStream.Length), 0, 8);

            // Encode the file.
            coder.Code(sourceStream, destStream, sourceStream.Length, -1, null);

            destStream.Flush();
            return destStream.ToArray();
        }
        
        public async Task<byte[]> Decompress(byte[] value)
        {
            await using var sourceStream = new MemoryStream(value);
            await using var destStream = new MemoryStream();
            var coder = new Decoder();
            
            // Read the decoder properties
            var properties = new byte[5];
            sourceStream.Read(properties, 0, 5);
            
            // Read in the decompressed file size.
            var fileLengthBytes = new byte[8];
            sourceStream.Read(fileLengthBytes, 0, 8);
            var fileLength = BitConverter.ToInt64(fileLengthBytes, 0);
            
            // Decode the file.
            coder.SetDecoderProperties(properties);
            coder.Code(sourceStream, destStream, sourceStream.Length, fileLength, null);
            
            destStream.Flush();
            return destStream.ToArray();
        }
    }
}