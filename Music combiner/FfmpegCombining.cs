using System;
using System.Diagnostics;
using System.Text;

namespace Music_combiner
{
    public class Combiner
    {

        public string SongEncoding(List<string> songOrder, string outputDir) // take in the list of songs and combine them into a single file
        {
            long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds(); //get a number that *cannot* be reproduced easily 
            string workingDir = outputDir + @"\" + milliseconds.ToString() + "TempFolder"; //make a temporary folder with this random number in the name
            Directory.CreateDirectory(workingDir);

            // convert to wav if not already
            Console.WriteLine("converting songs");
            List<string> convertedSongOrder = ChangeSongFormat(songOrder, outputDir, workingDir);

            //construct filtergraph dynamically
            Console.WriteLine("constructing filtergraph");
            var filtergraph = ConstructFiltergraph(convertedSongOrder, workingDir);

            //write full ffmpeg command to bat file

            Console.WriteLine("writing to batch file");
            var executionFilePath = WriteToBatFile(workingDir, filtergraph);

            // run program
            Console.WriteLine("running program");
            var output = RunBatchFile(executionFilePath, workingDir);
            Console.WriteLine(output);

            return "this doesnt work if nothing is here and i cbf fixing it";

        }

        public List<string> ChangeSongFormat(List<string> songOrder, string outputDir, string workingDir)
        {
            List<string> newSongOrder = new();
            string fileExtention = ".mp3"; // 
            var songNum = 0;
            string convertedDir = workingDir + @"\converted_songs";
            


            foreach (var song in songOrder) // for all songs in songOrder
            {
                bool areThereMp3 = false;
                if (fileExtention.Contains(Path.GetExtension(song))) // if its an mp3 convert it to wav
                {
                     if (areThereMp3 == false) // run this once to create a dir if an mp3 song is detected
                     {
                         Directory.CreateDirectory(convertedDir);
                         areThereMp3 = true;
                         
                     }
                    //code to run ffmpeg for converting
                    string convertedOutput = convertedDir + @"\" + songNum + ".wav";
                    Console.WriteLine($"{convertedOutput}");
                    string commandText = $"/C ffmpeg -hide_banner -loglevel warning -i \"{song}\" \"{convertedOutput}\""; // ffmpeg command to run
                    System.Diagnostics.Process process = new();
                    System.Diagnostics.ProcessStartInfo startInfo = new()
                    {
                        WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                        FileName = "cmd.exe",
                        Arguments = commandText
                    };
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();

                    Console.WriteLine("Conveted " + song);
                    newSongOrder.Add(convertedOutput); // add to new list we are creating

                }
                else
                {
                    newSongOrder.Add(song); // if already wav add to list
                }
                songNum++;


            }

            return newSongOrder;
        }

        public string ConstructFiltergraph(List<string> convertedSongOrder, string workingDir)
        {
            StringBuilder ffmpegCommand = new();
            // construct input part of command
            ffmpegCommand.Append($"ffmpeg -hide_banner -loglevel warning ");

            // adding each song as an input into the command
            foreach (var song in convertedSongOrder)
            {
                ffmpegCommand.Append($"-i \"{song}\" ");
                Console.WriteLine("added" + convertedSongOrder.IndexOf(song) + " with id of: " + song);
            }

            int n = convertedSongOrder.Count;
            ffmpegCommand.Append("-filter_complex \"");
            // Add normalization filters
            for (int i = 0; i < n; i++)
            {
                ffmpegCommand.Append($"[{i}:a]loudnorm[a{i}]; ");
            }

            //construct crossfade part of song

            ffmpegCommand.Append($"[a0][a1]acrossfade=d=10:c1=exp:c2=tri[a01]; ");
            for (int i = 1; i < (n-2); i++)
            {
                ffmpegCommand.Append($"[a0{i}][a{i + 1}]acrossfade=d=10:c1=exp:c2=tri[a0{i + 1}]; ");
            }

            ffmpegCommand.Append($"[a0{n-2}][a{n-1}]acrossfade=d=10:c1=exp:c2=tri[out]\"");

            // Specify the output mapping and file
            ffmpegCommand.Append($" -map \"[out]\" \"{workingDir}/output_combined.wav\"");

            Console.WriteLine("constructed filtergraph to " + ffmpegCommand.ToString());

            return ffmpegCommand.ToString();
        }

        public string WriteToBatFile(string workingdir, string filtergrapgh)
        {
            //write ffmpeg command to batch file
            string filePath = workingdir + @"\ffmpeg_command.bat";

            File.WriteAllText(filePath, filtergrapgh);
            Console.WriteLine("Wrote to file" + filePath);

            return filePath;
        }

        public string RunBatchFile(string executionFilePath, string workingDir)
        {
            //start a new process to run a batch file
            Process process = new();
            process.StartInfo.FileName = executionFilePath;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string errors = process.StandardError.ReadToEnd();

            process.WaitForExit();
            Console.WriteLine("finished running program");

            return output;
        }

    }
}

/*
 TODO: If I end up using this a lot more I might rewrite this section again to make
the combining process work with more files, as currently I am running into a FFMPEG 
memory limitation
*/