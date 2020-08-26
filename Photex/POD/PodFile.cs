using System;
using System.IO;
using System.Text;

namespace PhotexEngine.POD
{
    public class PodFile
    {
        private readonly string _pod;

        public PodFile(string pod)
        {
            _pod = pod;
        }

        public PodDataStructure Read()
        {       
            using (var fs = new FileStream(_pod, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(fs))
                {
                    var fileCount = reader.ReadUInt32();
                    var comment = Encoding.ASCII.GetString(reader.ReadBytes(80)).Replace("\0", string.Empty).Trim();

                    var result = new PodDataStructure(fileCount, comment);

                    for (var i = 0; i < result.Files.Length; i++)
                    {
                        var path = Encoding.ASCII.GetString(reader.ReadBytes(32));
                        var size = reader.ReadUInt32();
                        var offset = reader.ReadUInt32();

                        reader.BaseStream.Seek(offset, SeekOrigin.Begin);
                        var contents = reader.ReadBytes((int)size);
                        reader.BaseStream.Seek(4 + (40 * (i + 1)), SeekOrigin.Begin);


                        var file = new PodDataFileStructure(path, size, offset, contents);

                        result.Files[i] = file;
                    }

                    return result;
                }
            }
        }
    }
}
