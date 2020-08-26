using PhotexEngine.POD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PODReader
{


    class Program
    {
        static void Main(string[] args)
        {
            var outputFolder = args[1];

            var pod = new PodFile(args[0]);

            var output = pod.Read();

            foreach(var item in output.Files)
            {
                Console.WriteLine(item.Path);
                item.Save(Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(args[0])));
            }
        }
    }
}
