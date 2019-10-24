using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using RomTool.Blocks;

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
            
            img.Count.Out("Firmware Image bytes");
            
            parsed.ME.LongLength.Out("ME Region bytes");
            parsed.BIOS.Size.Out("Bios Region bytes");

            var vols = parsed.BIOS.Subs;
            vols.Count.Out("FFSv2 Volumes recognized");

            var files = vols.SelectMany(x => x.Subs);
            files.Count().Out("Files recognized");

            var secs = files.SelectMany(x => x.Subs);
            secs.Count().Out("Sections recognized");

            var subsecs = secs.SelectMany(x => x.SubSections);
            subsecs.Count().Out("SubSections recognized");

            //OutBytes(Image.ToArray(), 2048);
            Console.ReadKey();
        }
        
        private static void Out<T>(this T data, string text)
            => Console.WriteLine($"{text,-40} :\t{data.ToString()}");
        
        private static void OutBytes(byte[] bData, int? len = null)
        {
            int i, j = 0;
            var outText = new StringBuilder("        ", 16 * 4 + 8);
            
            for (i = 0; i < (len ?? bData.Length); i++)
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
