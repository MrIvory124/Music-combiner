using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.IO;

namespace Music_combiner
{
    public class Combiner
    {

        public string SongEncoding(List<string> songOrder, string outputDir) // take in the list of songs and combine them into a single file
        {

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("The output directory is " + outputDir);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Encoding begun");
            Console.ForegroundColor = ConsoleColor.DarkGray;

            long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds(); //get a number that *cannot* be reproduced easily 
            string workingDir = outputDir + @"\" + milliseconds.ToString() + "TempFolder"; //make a temporary folder with this random number in the name
            Directory.CreateDirectory(workingDir);
            System.Threading.Thread.Sleep(50);

            string outputFileN;
            for (int i1 = 1; i1 <= songOrder.Count; i1++) //TODO: tidy this up
            {
                if (i1 == 1) // on first run take the first 2 songs and combine them together
                {
                    string nInput1 = songOrder[0];
                    string nInput2 = songOrder[1];
                    outputFileN = workingDir + @"\output1.wav";
                    _ = Ffmpeg(nInput1, nInput2, outputFileN); // calls ffmpeg method
                    System.Threading.Thread.Sleep(100);
                    Console.WriteLine("File output: " + outputFileN);
                }
                else if (i1 == songOrder.Count) // every other run combine the previous result with the next song
                {
                    string nInput1 = workingDir + @"\output" + (i1 - 2) + ".wav";
                    string nInput2 = songOrder[(i1-1)];
                    outputFileN = workingDir + @"\output" + i1 + ".wav";
                    _ = Ffmpeg(nInput1, nInput2, outputFileN); // calls ffmpeg method
                    System.Threading.Thread.Sleep(100);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Final file output: " + outputFileN);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                else // upon final run output the path of the finished file into console
                {
                    string nInput1 = workingDir + @"\output" + (i1 - 1) + ".wav";
                    string nInput2 = songOrder[i1];
                    outputFileN = workingDir + @"\output" + i1 + ".wav";
                    _ = Ffmpeg(nInput1, nInput2, outputFileN); // calls ffmpeg method
                    System.Threading.Thread.Sleep(100);
                    Console.WriteLine("File output: " + outputFileN);
                }

            }


            return "this doesnt work if nothing is here and i cbf fixing it";
        }
        public string Ffmpeg(string inputFile1, string inputFile2, string outputFile) // method for running ffmpeg with crossfade filter TODO: replace this as its O^n
        {
            Console.WriteLine("Starting ffmpeg");
            int crossFadeLen = 10; // TODO: impliment user change crossfade length
            string command = "/C ffmpeg -loglevel error -i \"" + inputFile1 + "\"  -i \"" + inputFile2 + "\"  -vn -filter_complex \"acrossfade=d=" + crossFadeLen + ":c1=tri:c2=tri\" " + outputFile; // call ffmpeg using cmd with previously sorted files
            ProcessStartInfo cmdsi = new ProcessStartInfo("cmd.exe");
            cmdsi.Arguments = command;
            Process cmd = Process.Start(cmdsi);
            cmd.WaitForExit();

            Console.WriteLine(outputFile);
            Console.WriteLine("Stopping ffmpeg");
            return outputFile;
        }
    }
}


