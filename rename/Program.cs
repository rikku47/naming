using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace rename
{
    class Program
    {
        static void Main(string[] args)
        {
            string dirInput = @"G:\downloads\mp3RenameTesting";
            string dirOutput = @"G:\downloads\mp3RenameTesting\output";
            string pattern = @"-\w+(?=\.mp3)";

            string[] fileEntires = Directory.GetFiles(dirInput);

            int countFiles = 0;

            foreach (var fileEntry in fileEntires)
            {
                FileInfo file = new FileInfo(fileEntry);

                string output = Regex.Replace(file.Name, pattern, "");

                string path = dirOutput + "\\" + output;

                try
                {
                    if (File.Exists(path))
                    {
                        var output0 = Regex.Matches(output, @"(.*)(?=\.mp3)");
                        path = dirOutput + "\\" + output0[0] + DateTime.Now.Ticks + file.Extension;
                    }
                    file.MoveTo(path);

                    int countWrite = 0;

                    do
                    {
                        if (countWrite == 0)
                        {
                            Console.Write("\rWriting file.");

                        }
                        else if (countWrite == 1)
                        {
                            Console.Write("\rWriting file..");
                        }
                        else if (countWrite == 2)
                        {
                            Console.Write("\rWriting file...");
                            countWrite = 0;
                        }

                        countWrite++;

                    } while (!File.Exists(path));

                    countFiles++;
                    Console.WriteLine("File " + countFiles + " from " + fileEntires.Length + " moved.");
                    Console.WriteLine("File old: " + fileEntry + " to file new: " + path);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
