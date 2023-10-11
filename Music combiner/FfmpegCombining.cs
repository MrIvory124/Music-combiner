using System.Diagnostics;

namespace Music_combiner
{
    public class Combiner
    {

        public string Splitter(List<string> songOrder, string outputDir)
        {

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("The output directory is " + outputDir);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Encoding begun");
            Console.ForegroundColor = ConsoleColor.DarkGray;

            long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string workingDir = outputDir + @"\" + milliseconds.ToString() + "TempFolder";
            Directory.CreateDirectory(workingDir);
            System.Threading.Thread.Sleep(50);

            string outputFileN;
            for (int i1 = 1; i1 <= songOrder.Count; i1++)
            {
                if (i1 == 1)
                {
                    string nInput1 = songOrder[0];
                    string nInput2 = songOrder[1];
                    outputFileN = workingDir + @"\output1.wav";
                    _ = Ffmpeg(nInput1, nInput2, outputFileN);
                    System.Threading.Thread.Sleep(100);
                    Console.WriteLine("File output: " + outputFileN);
                }
                else if (i1 == songOrder.Count)
                {
                    string nInput1 = workingDir + @"\output" + (i1 - 2) + ".wav";
                    string nInput2 = songOrder[(i1-1)];
                    outputFileN = workingDir + @"\output" + i1 + ".wav";
                    _ = Ffmpeg(nInput1, nInput2, outputFileN);
                    System.Threading.Thread.Sleep(100);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Final file output: " + outputFileN);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                else
                {
                    string nInput1 = workingDir + @"\output" + (i1 - 1) + ".wav";
                    string nInput2 = songOrder[i1];
                    outputFileN = workingDir + @"\output" + i1 + ".wav";
                    _ = Ffmpeg(nInput1, nInput2, outputFileN);
                    System.Threading.Thread.Sleep(100);
                    Console.WriteLine("File output: " + outputFileN);
                }

            }

            


            /* so here is the goal:
             1 take first 2 files and create a crossfade file with temp name then output in temp dir
            2 then take previous output, add a new file and encode (repeat until songOrder is empty) deleting previous file afterwards
            */

            /* string inputfile1 = songOrder[0];
             for (int i1 = 1; i1 < songOrder.Count; i1++)
             {
                 string inputfile2 = songOrder[i1];
                 // call ffmpeg thing, and also figure out how to actually input them
                 Console.Write(songOrder[i1]);
             }*/


            return "yes";
        }
        public string Ffmpeg(string inputFile1, string inputFile2, string outputFile) // method for running ffmpeg with crossfade filter
        {
            Console.WriteLine("Starting ffmpeg");
            int crossFadeLen = 10; // TODO: impliment user change crossfade length
            string command = "/C ffmpeg -loglevel error -i \"" + inputFile1 + "\"  -i \"" + inputFile2 + "\"  -vn -filter_complex \"acrossfade=d=" + crossFadeLen + ":c1=tri:c2=tri\" " + outputFile;
            ProcessStartInfo cmdsi = new ProcessStartInfo("cmd.exe");
            cmdsi.Arguments = command;
            Process cmd = Process.Start(cmdsi);
            cmd.WaitForExit();
            /*proc.StartInfo.FileName = "CMD.exe";
            proc.StartInfo.Arguments = "ffmpeg -i \"F:\\Music\\organised\\Anger\\ES_The Final Stroke - Jon Bjork.wav\" -i \"F:\\Music\\organised\\Anger\\ES_Painted Road - Jon Algar - Copy.wav\" -vn -filter_complex \"acrossfade=d=10:c1=tri:c2=tri\" \"F:\\Music\\organised\\Anger\\output.wav\"";
            */

            Console.WriteLine(outputFile);
            Console.WriteLine("Stopping ffmpeg");
            return outputFile;
        }
    }
}


