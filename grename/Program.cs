using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grename
{
    internal class Program
    {
        public static bool test = false;
        static void Main(string[] args)
        {

            if (args.Length < 1)
            {
                printHelp();
                Environment.Exit(0);
            }

            foreach (string arg in args)
            {
                if (arg == "/h" || arg == "-h" || arg == "/help" || arg == "-help")
                {
                 printHelp();
                 Environment.Exit(0);
                }
            }
            int i = 0;

            string pattern = "";
            string newname = "";
            string format = "0";
            int index = 1;


            foreach (string arg in args)
            {
                if(arg == "-test") { test = true; continue; }
                switch (i)
                {
                    case 0: pattern = arg; break;
                    case 1: newname = arg; break;
                    case 2: format = arg; break;
                    case 3:
                        if (!int.TryParse(arg, out index))
                        {
                            Console.WriteLine("Start index argument must be an integer. Cannot parse: '"+arg+"'.");
                            Environment.Exit(0);
                        }
                        break;
                    default:
                        Console.WriteLine("Too many arguments. Try 'grename /h' for help.");
                        Environment.Exit(0);
                        break;
                }
                i++;
            }

            if(i == 1)
            {
                testpattern(pattern);
                Environment.Exit(0);
            }
            if (i < 2)
            {
                Console.WriteLine("Missing arguments. Try 'grename /h' for help.");
                Environment.Exit(0);
            }
            if (!newname.Contains("?"))
            {
                Console.WriteLine("[newname] argument missing '?'. Try 'grename /h' for help.");
                Environment.Exit(0);
            }
            rename(pattern, newname, format, index);

        }

        static void printHelp()
        {
            Console.WriteLine("grename by Kjetil Larsen, Global Digital Media.");
            Console.WriteLine("grename is used to rename files to a numbered sequence");
            Console.WriteLine("");
            Console.WriteLine("grename [-test] [searchpattern] [newname] [string indexformat] [int startindex]");
            Console.WriteLine("grename [searchpattern] -> TEST searchpattern only: outputs list of this searchpattern in the order grename will process it.");
            Console.WriteLine("grename -test : insert ' -test ' anywhere in argument list to do a dry run, showing each file and potential new name without actually renaming");
            Console.WriteLine("[newname] : e.g newname?.png (- ? will be replaced with sequence)");
            Console.WriteLine("");
            Console.WriteLine("Examples:");
            Console.WriteLine("grename *.png -> outputs list of files found with searchpattern, in the order grename will process them");
            Console.WriteLine("grename *.png newname?.png 000 1 -> rename all files found by *.png (ordered alphabetically) to 'newname001.png, 002, etc");
            Console.WriteLine("grename -test *.png newname?.png 000 1 -> list all files found and new name it will get when you run without -test param.");
            Console.WriteLine("grename *.jpg new?file.jpg 00 1 -> rename all files found by *.jpg (ordered alphabetically) to 'new01file.jpg, new02file.jpg, etc");
            Console.WriteLine("grename *.jpg new?.jpeg -> rename *.jpg to new?.jpeg using default format ('0') and default startindex (1)");

        }
        public static void testpattern(string search)
        {
            Console.WriteLine("List for search pattern '" + search + "':");
            IEnumerable<string> list = System.IO.Directory.EnumerateFiles(Environment.CurrentDirectory, search, System.IO.SearchOption.TopDirectoryOnly);
            Console.WriteLine("Found " + list.Count() + " files.");
            foreach (string file in list)
            {
                Console.WriteLine(file);
            }
        }

        public static void rename(string search, string newname, string format, int index)
        {
             
            string[] list = System.IO.Directory.GetFiles(Environment.CurrentDirectory, search, System.IO.SearchOption.TopDirectoryOnly);


            if (test)
            {
                Console.WriteLine("grename " + search + " TEST RUN.");
            }
            else
            {
                Console.WriteLine("grename " + search);
            }
            Console.WriteLine("Found " + list.Count() + " files.");

            string i_newname = newname;
            int actual_index = index;
            string destfile = "";
            foreach (string file in list)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(file);
                if(fi.Name == "grename.exe")
                {
                    Console.WriteLine("Renaming grename.exe not allowed. Test your pattern with grename [pattern] first. No changes have been made.");
                    Environment.Exit(0);
                }
            }
            foreach (string file in list)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(file);
                destfile = newname.Replace("?", actual_index.ToString(format));
                Console.WriteLine(fi.Name + " -> " + destfile);
                if (!test) { fi.MoveTo(System.IO.Path.Combine(fi.Directory.ToString(), destfile)); }
                actual_index++;
            }

        }
    }
}
