namespace Music_combiner
{

    public class FilePulling //class for pulling all the names and files and putting them into a list
    {
        static Tuple<string, bool> UserInput() // grab input from user
        {
            //variables to use
            string userDir = "";
            bool scanSubFolders = false;
            bool confirm = false;


            try
            {
                do
                {

                    Console.WriteLine("Input a valid directory: "); // get them to first input a directory
                    userDir = Console.ReadLine();
                    if (Directory.Exists(userDir) == true) // if the directory exists confirm its the correct one
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
                                Console.WriteLine("Something went wrong, please try again.");
                                break;
                        }
                    }
                    if (confirm == true) // if the directory is the correct one, ask to scan sub folders
                    {

                        Console.WriteLine("Scan sub-folders too?");
                        string Selection = Console.ReadLine();

                        switch (Selection)
                        {
                            case "y":
                                scanSubFolders = true;
                                break;
                            case "n":
                                scanSubFolders = false;
                                break;
                            case "Y":
                                scanSubFolders = true;
                                break;
                            case "N":
                                scanSubFolders = false;
                                break;
                            default:
                                scanSubFolders = false;
                                break;
                        }
                    }



                }
                while (confirm == false); 

            }

            catch (System.Exception except) // if there is a problem throw error
            {
                Console.WriteLine("An error occurred: " + except.Message);
                Environment.Exit(0);

            }
            return Tuple.Create(userDir, scanSubFolders); // return variables, none of them should be null if error handling worked
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
                Console.WriteLine("Scanning: " + userDir + " for files");
                foreach (string file in Directory.GetFiles(userDir)) //grab all files in the chosen directory
                {

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
                    Console.WriteLine("Scanning sub folders");
                    foreach (string folder in Directory.GetDirectories(userDir)) // for each folder in the specified folder
                    {

                        Console.WriteLine("Scanning: " + folder);
                        foreach (string file in Directory.GetFiles(folder)) // for each file in the folder
                        {
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
                }
            }

            catch (System.Exception except) //handle errors
            {
                Console.WriteLine("An error occurred: " + except.Message);
            }
            Console.WriteLine("Songs that will be used:");
            Console.WriteLine("[{0}]", string.Join(",\n", musicFiles)); // write output to console formatted
            Console.WriteLine("");
            Console.WriteLine("Other files found:");
            Console.WriteLine("[{0}]", string.Join(", ", nonMusicFiles)); // if no non music files, then will appear empty
            return Tuple.Create(nonMusicFiles, musicFiles); // pass variables on
        }

        public static void Main(string[] args)
        {
            //Tuple<string, bool> userInput = UserInput();
            string userDir;
            bool scanSubFolders;
            (userDir, scanSubFolders) = UserInput(); 
            Tuple<List<string>, List<string>> output = ListFiles(userDir, scanSubFolders); //TODO: make it so that files can be excluded


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