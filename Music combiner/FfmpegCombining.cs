using System.Diagnostics;

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
            List<string> convertedSongOrder = new(); 
            convertedSongOrder = ChangeSongFormat(songOrder, outputDir, workingDir);

            //construct filtergraph dynamically



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

                    string convertedOutput = convertedDir + songNum + ".wav";
                    string command = $"ffmpeg -i {song} {convertedOutput}"; // ffmpeg command to run
                    ProcessStartInfo cmdsi = new ProcessStartInfo("cmd.exe"); // run cmd
                    cmdsi.Arguments = command;
                    Process cmd = Process.Start(cmdsi);
                    cmd.WaitForExit();

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

    }
}


/*
 
 How we want to do it:

1. convert all mp3 songs to wav in a temp folder

2. construct filtergraph with crossfades for n songs

3. construct final ffmpeg command
    0. ffmpeg +
    i. -i (list of all songs in the songOrder order)
    ii. -filter_complex + constructed filtergraph
    iii. + -map "[out]" (userOutDir)output.mp3

4. move final file to chosen output dir and delete any temporary files
 
 ffmpeg -i song1.mp3 -i song2.mp3 -i song3.mp3 -i song4.mp3 -filter_complex "[0:a][1:a]acrossfade=d=10:c1=tri:c2=tri[a1];[a1][2:a]acrossfade=d=10:c1=tri:c2=tri[a2];[a2][3:a]acrossfade=d=10:c1=tri:c2=tri[out]" -map "[out]" output.mp3 // does actually work

 
 */