using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace RomTool
{
    public static class Program
    {
        const string File = @"C:\UBU\bios2.bin";
        private const int ChunkSize = 1024;
        
        static readonly List<byte> img = new List<byte>();
        
        static void Main()
        {
            if (System.IO.File.Exists(File))
                using (var fs = new FileStream(File, FileMode.Open, FileAccess.Read))
                using (var br = new BinaryReader(fs, new UTF8Encoding()))
                    while (fs.Position < fs.Length)
                        img.AddRange(br.ReadBytes(ChunkSize));
            
            
            var parsed = new Image(img.ToArray());
            
            Out("Firmware Image bytes", img.Count);
            
            Out("ME Region bytes", parsed.ME.LongLength);
            Out("Bios Region bytes", parsed.BIOS.Size);
            Out("Empty pad-files total bytes", parsed.BIOS.Paddings.Values.Sum(x=>x));
            var vols = parsed.BIOS.Volumes.Values;
            Out("FFSv2 Volumes recognized", vols.Count);
            var files = vols.SelectMany(x => x.Files.Values);
            Out("Files recognized", files.Count());
            var secs = files.SelectMany(x => x.Sections.Values);
            Out("Sections recognized", secs.Count());
            var subsecs = secs.SelectMany(x => x.SubSections.Values);
            Out("SubSections recognized", subsecs.Count());


            //OutBytes(Image.ToArray(), 2048);
        }
        
        private static void Out<T>(string text, T data)
            => Console.WriteLine($"{text,-40} :\t{data.ToString()}");
        
        private static void OutBytes(byte[] bData, int? len = null)
        {
            int i, j = 0;
            var outText = new StringBuilder("        ", 16 * 4 + 8);
            
            for (i = 0; i < (len??bData.Length); i++)
            {
                outText.Insert(j * 3, $"{(int)bData[i]:X2} ");
                var dChar = (char)bData[i];
                outText.Append(char.IsWhiteSpace(dChar) || char.IsControl(dChar) ? '.' : dChar);
                if (++j == 16)
                {
                    Console.WriteLine(outText);
                    outText.Length = 0;
                    outText.Append("        ");
                    j = 0;
                }
            }

            if (j > 0)
            {
                for (i = j; i < 16; i++)
                    outText.Insert(j * 3, "   ");
                Console.WriteLine(outText);
            }
        }
    }
}
