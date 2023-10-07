using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Music_combiner
{
    public class Combiner 
    {

        public string Splitter(List<string> nonMusicFiles, List<string> musicFiles, string outputDir)
        {
            Console.WriteLine("stripping items");
            foreach (string item in musicFiles)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("The output directory is " + outputDir);
            return "yes";
        }
        public string Ffmpeg(string inputFile1, string inputFile2)
        {

            inputFile1 = "F:\\Music\\organised\\Anger\\ES_The Final Stroke - Jon Bjork.wav";
            inputFile2 = "F:\\Music\\organised\\Anger\\ES_Painted Road - Jon Algar - Copy.wav";
            string outputFile = "\"F:\\Music\\organised\\Anger\\output.wav\"";
            
            /*/String command = "/C ffmpeg -hide_banner -loglevel error -i \"F:\\Music\\organised\\Anger\\ES_The Final Stroke - Jon Bjork.wav\" -i \"F:\\Music\\organised\\Anger\\ES_Painted Road - Jon Algar - Copy.wav\" -vn -filter_complex \"acrossfade=d=10:c1=tri:c2=tri\" \"F:\\Music\\organised\\Anger\\output.wav\"";
            String command = "/C ffmpeg -hide_banner -loglevel error -i \"" + inputFile1 + "\"  -i \"" + inputFile2 + "\"  -vn -filter_complex \"acrossfade=d=10:c1=tri:c2=tri\" " + outputFile;
            ProcessStartInfo cmdsi = new ProcessStartInfo("cmd.exe");
            cmdsi.Arguments = command;
            Process cmd = Process.Start(cmdsi);
            cmd.WaitForExit();
            /*proc.StartInfo.FileName = "CMD.exe";
            proc.StartInfo.Arguments = "ffmpeg -i \"F:\\Music\\organised\\Anger\\ES_The Final Stroke - Jon Bjork.wav\" -i \"F:\\Music\\organised\\Anger\\ES_Painted Road - Jon Algar - Copy.wav\" -vn -filter_complex \"acrossfade=d=10:c1=tri:c2=tri\" \"F:\\Music\\organised\\Anger\\output.wav\"";
            */

            Console.WriteLine("Songs that will be used:");
           // Console.WriteLine("[{0}]", string.Join(",\n", musicFiles)); // write output to console formatted
            Console.WriteLine("");
            Console.WriteLine("Other files found:");
            //Console.WriteLine("[{0}]", string.Join(", ", nonMusicFiles)); // if no non music files, then will appear empty

            Console.WriteLine(outputFile);
            return outputFile;
        }
    }
}


