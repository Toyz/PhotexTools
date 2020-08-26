using System.IO;

namespace PhotexEngine.POD
{

    public class PodDataStructure
    {
        public uint FileCount { get; private set; }

        public string Comment { get; private set; }

        public PodDataFileStructure[] Files { get; private set; }

        public PodDataStructure(uint fileCount, string comment)
        {
            FileCount = fileCount;
            Comment = comment;
            Files = new PodDataFileStructure[fileCount];
        }
    }

    public class PodDataFileStructure
    {
        public string Path { get; private set; }
        public uint Size { get; private set; }
        public uint Offset { get; private set; }
        public byte[] Content { get; private set; }

        public PodDataFileStructure(string path, uint size, uint offset, byte[] content)
        {
            Path = path.Replace("\0", string.Empty).Trim();
            Size = size;
            Offset = offset;
            Content = content;
        }

        public void Save(string outputDirectory = "./")
        {
            if (Path == "") return;

            var outFilePath = System.IO.Path.Combine(outputDirectory, Path).Trim();

            var f = new FileInfo(outFilePath);
            f.Directory.Create();

            File.WriteAllBytes(outFilePath, Content);
        }
    }
}
