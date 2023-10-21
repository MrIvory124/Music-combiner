# Music combiner
A little program I wrote in C# to take any mp3/wav files and randomly assort them into one file that you can put under VODS for twitch etc.

Note: I am aware that some things were not done to standards of the industry, and some things could be more effecicient but if it ain't broke don't fix it.
Also this is my first C# program after moving from python.

# Requirements
- Requires [FFMPEG](https://ffmpeg.org/download.html) to be installed and on path

# Limitations
- Only works on mp3 and wav files
- Can only work up to certain sizes (yet to determine how big that is) due to FFMPEG reaching memory limits
- If you have a lot of mp3 files it could fill up drives (provided you can get past the last limitation)
- Was written really sh*ttly by me

# Things left to do/fix
- Option for file exclusion
- Better algorithm for combining large numbers of files together
- Better user selection (listen for y or n presses instead of requiring enter to be pressed)
- Splitting up of the GetUserInput method due to it being long and doing multiple things
- Conversion/use of mp4 files
- Clean up files left over from the converting process
- A json or something that contains some other user preferences
