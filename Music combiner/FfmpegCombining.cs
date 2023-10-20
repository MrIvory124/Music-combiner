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
 
            return "this doesnt work if nothing is here and i cbf fixing it";

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