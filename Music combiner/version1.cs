using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Runtime.InteropServices;

namespace Music_combiner
{
    
    public class Settings
    {
        /*private string[] fileExtentions = { ".mp3", ".wav" };
        public string[] FileExtensions
        {
            get { return fileExtentions; }   // get method
            set { fileExtentions = value; }  // set method
        }  TODO: eventually set it up so that this top bit is the settings*/
    }


    public class FilePulling 
    {
        static void listFiles(string sDir)
        {
            string[] fileExtentions = { ".mp3", ".wav" }; 
            List<string> musicFiles;
            musicFiles = new List<string>();
            List<string> nonMusicFiles;
            nonMusicFiles = new List<string>();

            try
            {
                int folderNum = 0;
                int fileNum = 0;
                foreach (string folder in Directory.GetDirectories(sDir))
                {

                    Console.WriteLine("Scanning: " + sDir + ". Folder number: " + folderNum);
                    foreach (string file in Directory.GetFiles(folder))
                    {
                        //Console.WriteLine(file);
                        Console.WriteLine("Scanning: " + fileNum);
                        if (fileExtentions.Contains(Path.GetExtension(file)))
                        {
                            Console.WriteLine(fileNum + " is music");
                            musicFiles.Add(file);
                        } 
                        else
                        {
                            Console.WriteLine(fileNum + " is not music");
                            nonMusicFiles.Add(file);
                        }
                        fileNum++;
                    }
                    folderNum++;
                    Console.WriteLine("[{0}]", string.Join(", ", musicFiles));
                    Console.WriteLine("");
                    Console.WriteLine("[{0}]", string.Join(", ", nonMusicFiles));
                    listFiles(folder);
                }
            }
            catch (System.Exception except)
            {
                Console.WriteLine(except.Message);
            }
            Console.Write("\n");

        }

        static void Main(string[] args)
        {
            string sDir = (System.Environment.CurrentDirectory);
            listFiles(sDir);
        }


    }
}
