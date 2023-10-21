namespace Music_combiner
{

    public class MusicFileProcessor //class for pulling all the names and files and putting them into a list
    {
        public static void Main(string[] args) // how things are run in order
        {
            string userDir;
            bool scanSubFolders;
            string outputDir = "";


            (userDir, scanSubFolders, outputDir) = GetUserInput(); // run the user input method

            (List<string> nonMusicFiles, List<string> musicFiles) = ListFiles(userDir, scanSubFolders); //TODO: make it so that files can be excluded, lists all files

            List<string> songOrder = Randomizer(musicFiles); // send the listed songs to the randomizer method

            var encoder = new Music_combiner.Combiner();

            encoder.SongEncoding(songOrder, outputDir); // calls the method that starts the encoding process
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Done");
            Console.ForegroundColor = ConsoleColor.White;

        }

        static Tuple<string?, bool, string> GetUserInput() // grab input from user TODO: get user to name final output file name, split into seperate methods for readability
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


                    }
                    else // if not
                    {
                        Console.WriteLine(fileNum + " is not music");
                        nonMusicFiles.Add(file);
                        fileNum++;

                    }
                }
                folderNum++;


                if (scanSubFolders == true) // if user wants sub folders to be scanned do this as well
                {
                    int filesBefore = fileNum;
                    int foldersBefore = folderNum;
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Scanning sub folders");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    foreach (string folder in Directory.GetDirectories(userDir)) // for each folder in the specified folder
                    {

                        Console.WriteLine("Scanning: " + folder);
                        foreach (string file in Directory.GetFiles(folder)) // for each file in the folder
                        {
                            Random rnd = new Random();
                            int random = rnd.Next(10, 50);
                            Console.WriteLine("Scanning: " + fileNum);
                            if (fileExtentions.Contains(Path.GetExtension(file))) // if the file contains the mentioned file extentions
                            {
                                Console.WriteLine(fileNum + " is music");
                                musicFiles.Add(file);
                                fileNum++;


                            }
                            else // if not
                            {
                                Console.WriteLine(fileNum + " is not music");
                                nonMusicFiles.Add(file);
                                fileNum++;

                            }
                        }
                        folderNum++;
                    }

                    if (foldersBefore == folderNum)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine("No sub folders to scan!");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    else if (filesBefore == fileNum)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine("No files found in sub folders!");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
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
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("[{0}]", string.Join(",\n", musicFiles)); // write output to console formatted
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Other files found:");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("[{0}]", string.Join(",\n", nonMusicFiles)); // if no non music files, then will appear empty
            Console.WriteLine("");

            System.Threading.Thread.Sleep(5000); //give user time to read songs in list (is useless if long list)
            return Tuple.Create(nonMusicFiles, musicFiles); // pass variables on
        }

        static List<string> Randomizer(List<string> musicFiles) // method that randomizes the order of the songs that are picked
        {
            int iterate = musicFiles.Count;
            iterate--;
            var randomNum = new Random();
            Console.WriteLine(iterate);
            List<string> songOrder = new();
            for (int i = 0; i <= iterate; i++)
            {
                int musicFileNum = randomNum.Next(musicFiles.Count);
                songOrder.Add(musicFiles[musicFileNum]);

                musicFiles.RemoveAt(musicFileNum);
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Song order: ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("[{0}]", string.Join(",\n", songOrder));
            return songOrder;

        }

    }
}
