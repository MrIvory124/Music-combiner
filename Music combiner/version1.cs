using System;

namespace Music_combiner
{

    public class FilePulling //class for pulling all the names and files and putting them into a list
    {
        static Tuple<string?, bool, string> UserInput() // grab input from user
        {
            //variables to use
            string userDir = "";
            string outputDir = "";
            bool scanSubFolders = false;
            bool confirm = false;


            try
            {
                do
                {

                    Console.WriteLine("Input a valid input directory: "); // get them to first input a directory
                    userDir = Console.ReadLine();
                    if (Directory.Exists(userDir) == true) // if the directory exists confirm its the correct one
                    {
                        Console.Write("Confirm this directory to use (y/n): ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(userDir + "\n");
                        Console.ForegroundColor = ConsoleColor.White;
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
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Please either choose (y/n)");
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Directory does not exist!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    bool canContinue = false;
                    if (confirm == true)
                    {
                        do // if the directory is the correct one, ask to scan sub folders
                        {
                            Console.WriteLine("Scan sub-folders too? (y/n)");
                            string Selection = Console.ReadLine();

                            switch (Selection)
                            {
                                case "y":
                                    scanSubFolders = true;
                                    canContinue = true;
                                    break;
                                case "n":
                                    scanSubFolders = false;
                                    canContinue = true;
                                    break;
                                case "Y":
                                    scanSubFolders = true;
                                    canContinue = true;
                                    break;
                                case "N":
                                    scanSubFolders = false;
                                    canContinue = true;
                                    break;
                                default:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Please either input (y/n)");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    scanSubFolders = false;
                                    canContinue = false;
                                    break;
                            }
                        } while (canContinue == false);
                    }

                    if (confirm == true) // if the directory is the correct one, ask for output folder
                    {
                        do
                        {
                            Console.WriteLine("Select output folder:");
                            outputDir = Console.ReadLine();
                            if (outputDir == userDir)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Output directory must be different from input!");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else if (Directory.Exists(outputDir) == false)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Output directory must exist!");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        while ((Directory.Exists(outputDir) == false) || (outputDir == userDir));
                    }

                }
                while (confirm == false);

            }

            catch (System.Exception except) // if there is a problem throw error
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An error occurred: " + except.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Environment.Exit(0);

            }
            return Tuple.Create(userDir, scanSubFolders, outputDir); // return variables, none of them should be null if error handling worked
        }
        static Tuple<List<string>, List<string>> ListFiles(string userDir, bool scanSubFolders)
        {
            string[] fileExtentions = { ".mp3", ".wav" }; // file extentions to use later on

            List<string> musicFiles = new();
            List<string> nonMusicFiles = new();

            try // scanning for files with mentioned extentions
            {
                int folderNum = 1;
                int fileNum = 1;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Scanning: " + userDir + " for files");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                foreach (string file in Directory.GetFiles(userDir)) //grab all files in the chosen directory
                {
                    Random rnd = new Random();
                    int random = rnd.Next(50, 500);
                    Console.WriteLine("Scanning: " + fileNum);
                    if (fileExtentions.Contains(Path.GetExtension(file))) // if the file contains the mentioned file extentions
                    {
                        Console.WriteLine(fileNum + " is music");
                        musicFiles.Add(file);
                        fileNum++;
                        System.Threading.Thread.Sleep(random);


                    }
                    else // if not
                    {
                        Console.WriteLine(fileNum + " is not music");
                        nonMusicFiles.Add(file);
                        fileNum++;
                        System.Threading.Thread.Sleep(random);

                    }
                }
                folderNum++;
                System.Threading.Thread.Sleep(100);


                if (scanSubFolders == true) // if user wants sub folders to be scanned do this as well
                {
                    int filesBefore = fileNum;
                    int foldersBefore = folderNum;
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Scanning sub folders");
                    System.Threading.Thread.Sleep(2000);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    foreach (string folder in Directory.GetDirectories(userDir)) // for each folder in the specified folder
                    {

                        Console.WriteLine("Scanning: " + folder);
                        foreach (string file in Directory.GetFiles(folder)) // for each file in the folder
                        {
                            Random rnd = new Random();
                            int random = rnd.Next(50, 500);
                            Console.WriteLine("Scanning: " + fileNum);
                            if (fileExtentions.Contains(Path.GetExtension(file))) // if the file contains the mentioned file extentions
                            {
                                Console.WriteLine(fileNum + " is music");
                                musicFiles.Add(file);
                                fileNum++;
                                System.Threading.Thread.Sleep(random);


                            }
                            else // if not
                            {
                                Console.WriteLine(fileNum + " is not music");
                                nonMusicFiles.Add(file);
                                fileNum++;
                                System.Threading.Thread.Sleep(random);

                            }
                        }
                        folderNum++;
                        System.Threading.Thread.Sleep(100);
                    }
                    
                    if (foldersBefore == folderNum)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine("No sub folders to scan!");
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            System.Threading.Thread.Sleep(1000);
                    }
                    else if (filesBefore == fileNum)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine("No files found in sub folders!");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        System.Threading.Thread.Sleep(1000);
                    }

                }
            }

            catch (System.Exception except) //handle errors
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An error occurred: " + except.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Songs that will be used:");
            System.Threading.Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("[{0}]", string.Join(",\n", musicFiles)); // write output to console formatted
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Other files found:");
            System.Threading.Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("[{0}]", string.Join(",\n", nonMusicFiles)); // if no non music files, then will appear empty
            Console.WriteLine("");
            System.Threading.Thread.Sleep(2000); //change this to give user more time to read songs
            return Tuple.Create(nonMusicFiles, musicFiles); // pass variables on
        }

        public static void Main(string[] args)
        {
            string userDir;
            bool scanSubFolders;
            string outputDir = "";

            var temp = new Music_combiner.Combiner();

            (userDir, scanSubFolders, outputDir) = UserInput();

            (List<string> nonMusicFiles, List<string> musicFiles) = ListFiles(userDir, scanSubFolders); //TODO: make it so that files can be excluded

            temp.Splitter(nonMusicFiles, musicFiles, outputDir);
            // TODO: Mix audio together with crossfade
            // currently understand how to concatenate them together
            // going to impliment some hacky ffmpeg thing
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Done");
            Console.ForegroundColor = ConsoleColor.White;

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