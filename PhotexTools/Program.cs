using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PODReader
{
    class PODFile
    {
        public string Path { get; private set; }
        public uint Size { get; private set; }
        public uint Offset { get; private set; }

        public PODFile(BinaryReader reader)
        {
            Path = Encoding.ASCII.GetString(reader.ReadBytes(32));
            Size = reader.ReadUInt32();
            Offset = reader.ReadUInt32();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var outputFolder = args[1];

            var b = new BinaryReader(File.OpenRead(args[0]));
            outputFolder = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(args[0]));

            var fileCount = b.ReadUInt32();
            Console.WriteLine($"POD File Count:\t{fileCount}");


            var comment = b.ReadBytes(80);
            Console.WriteLine($"POD Comment:\t{Encoding.ASCII.GetString(comment)}");

            List<PODFile> files = new List<PODFile>();

            for (var i = 0; i < fileCount; i++)
            {
                files.Add(new PODFile(b));
            }

            foreach(var file in files)
            {
                Console.WriteLine($"Extracting: {file.Path}");

                var f = new FileInfo(Path.Combine(outputFolder, file.Path.Replace("\0", string.Empty).Trim()));
                f.Directory.Create();
                File.WriteAllBytes(f.FullName, b.ReadBytes((int)file.Size));
            }

            b.Close();
        }
    }
}
