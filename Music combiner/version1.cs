using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.Design;

namespace Music_combiner
{

    public class FilePulling //class for pulling all the names and files and putting them into a list
    {
        static Tuple<List<string>, List<string>> ListFiles(string userInput) //returns 2 lists, 1 for music and 1 for non music items in a folder
        {
            string[] fileExtentions = { ".mp3", ".wav" };
            List<string> musicFiles = new List<string>();
            List<string> nonMusicFiles = new List<string>();

            try
            {
                int folderNum = 0;
                int fileNum = 0;
                foreach (string folder in Directory.GetDirectories(userInput)) // for each folder in the specified folder
                {

                    Console.WriteLine("Scanning: " + userInput + ". Folder number: " + folderNum);
                    foreach (string file in Directory.GetFiles(folder)) // for each file in the folder
                    {
                        //Console.WriteLine(file);
                        // if folders scan folders and the host folder
                        // if no folders scan host folder
                        Console.WriteLine("Scanning: " + fileNum);
                        if (fileExtentions.Contains(Path.GetExtension(file))) // if the file contains the mentioned file extentions
                        {
                            Console.WriteLine(fileNum + " is music");
                            musicFiles.Add(file); 
                            fileNum++;
                            

                        } 
                        else
                        {
                            Console.WriteLine(fileNum + " is not music");
                            nonMusicFiles.Add(file);
                            fileNum++;
                            
                        }
                    }
                    folderNum++;
                    Console.WriteLine("[{0}]", string.Join(", ", musicFiles));
                    Console.WriteLine("");
                    Console.WriteLine("[{0}]", string.Join(", ", nonMusicFiles));
                    ListFiles(folder);
                    


                }

            }
            catch (System.Exception except)
            {
                Console.WriteLine(except.Message);
                

            }

            return Tuple.Create(nonMusicFiles, musicFiles);
        }

        
        static string UserInput()
        {
            //string userDir = (System.Environment.CurrentDirectory);
            string userDir = "";
            bool confirm = false;
            

            try
            {
                do
                {

                    Console.WriteLine("Input a valid directory: ");
                    userDir = Console.ReadLine();
                    if (Directory.Exists(userDir) == true)
                    {
                        Console.WriteLine("Confirm this directory to use (y/n): '" + userDir + "'");
                        string selection = Console.ReadLine();

                        switch (selection)
                        {
                            case "y":
                                confirm = true;
                                break;
                            case "n":
                                confirm = false;
                                break;
                            case "Y":
                                confirm = true;
                                break;
                            case "N":
                                confirm = false;
                                break;
                            default:
                                confirm = false; 
                                break;
                        }
                    }



                }
                while (confirm == false);

            }
            catch (System.Exception except)
            {
                Console.WriteLine("An error occurred: " + except.Message);
                Environment.Exit(0);

            }
         return userDir;
        }
        
        public static void Main(string[] args)
        {
            string userInput = UserInput();
            Tuple<List<string>, List<string>> output = ListFiles(userInput);


        }


    }
}

/*  while (true)
{
    string userDir = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(userDir))
    {
        Console.WriteLine("Must be a valid filepath");

    }
}
*/