// ;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRandombreaker
{
    class Program
    {
        public const string ArmorFilename = "entity_mods";
        public const string WeaponFilename = "weapon_stats";
        public const string ModFilename = "weapon_mod";

        public static string Version = "v1.0";


        static void Main(string[] _args)
        {
            Console.WriteLine("=== The Randombreaker " + Version + " ===");

            Console.WriteLine("  Found " + _args.Length + " file arguments:");
            foreach (string arg in _args)
            {
                Console.WriteLine("    " + arg);
            }
            Console.WriteLine();

            foreach (string arg in _args)
            {
                if (arg.Contains(ArmorFilename))
                {
                    ProcessArmorFile(arg);
                }
                else if (arg.Contains(WeaponFilename))
                {
                    ProcessWeaponFile(arg);
                }
                else if (arg.Contains(ModFilename))
                {
                    //ProcessModFile(arg);
                    ProcessWeaponFile(arg);

                }
                else
                {
                    Console.WriteLine("  File " + arg + " is not supported.");
                }




            }

            Console.WriteLine("============ Done ============");
            Console.Read();
        }

        private static void ProcessArmorFile(string _filepath)
        {
            int filenamepos = _filepath.LastIndexOf('\\') + 1;
            string filedir = _filepath.Substring(0, filenamepos);
            string filename = _filepath.Substring(filenamepos);
            string filenameout = filename.Insert(filename.Length - 4, "_out");

            Console.WriteLine("  Processing file " + filename + "");

            StreamReader reader = new StreamReader(_filepath);
            if (reader == null)
            {
                Console.WriteLine("    Could not open file " + filename + " to read.");
                return;
            }

            StreamWriter writer = new StreamWriter(filedir + filenameout);
            if (writer == null)
            {
                Console.WriteLine("    Could not open file " + filenameout + " to write.");
                reader.Close();
                return;
            }

            int writeCount = 0;
            bool flush = false;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();

                if (flush)
                {
                    if (line.Contains("{"))
                    {
                        writer.Write(line);
                    }
                    else if (line.Contains("}"))
                    {
                        writer.WriteLine(" }");
                        flush = false;
                    }
                    else
                    { }
                    continue;
                }

                writer.WriteLine(line);
                if (line.Contains("mod_flags"))
                {
                    ++writeCount;
                    flush = true;
                    Console.Write("\r    Processing " + writeCount + " items...");
                }

            }
            Console.Write("\r                                                  ");
            Console.WriteLine("\r    Processed " + writeCount + " items.");

            reader.Close();
            writer.Close();
            Console.WriteLine();
        }

        private static void ProcessWeaponFile(string _filepath)
        {
            int filenamepos = _filepath.LastIndexOf('\\') + 1;
            string filedir = _filepath.Substring(0, filenamepos);
            string filename = _filepath.Substring(filenamepos);
            string filenameout = filename.Insert(filename.Length - 4, "_out");

            Console.WriteLine("  Processing file " + filename + "");

            StreamReader reader = new StreamReader(_filepath);
            if (reader == null)
            {
                Console.WriteLine("    Could not open file " + filename + " to read.");
                return;
            }

            StreamWriter writer = new StreamWriter(filedir + filenameout);
            if (writer == null)
            {
                Console.WriteLine("    Could not open file " + filenameout + " to write.");
                reader.Close();
                return;
            }

            int writeCount = 0;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                writer.WriteLine(line);

                if (line.Contains("max_value"))
                {
                    ++writeCount;
                    reader.ReadLine();
                    string nextLine = line.Replace("max_value", "min_value");

                    writer.WriteLine(nextLine);
                    Console.Write("\r    Processing " + writeCount + " items...");
                }
            }    
            Console.Write("\r                                                  ");
            Console.WriteLine("\r    Processed " + writeCount + " items.");

            reader.Close();
            writer.Close();
            Console.WriteLine();
        }

        private static void ProcessModFile(string _filepath)
        {
            int filenamepos = _filepath.LastIndexOf('\\') + 1;
            string filedir = _filepath.Substring(0, filenamepos);
            string filename = _filepath.Substring(filenamepos);
            string filenameout = filename.Insert(filename.Length - 4, "_out");

            Console.WriteLine("  Processing file " + filename + "");

            StreamReader reader = new StreamReader(_filepath);
            if (reader == null)
            {
                Console.WriteLine("    Could not open file " + filename + " to read.");
                return;
            }

            StreamWriter writer = new StreamWriter(filedir + filenameout);
            if (writer == null)
            {
                Console.WriteLine("    Could not open file " + filenameout + " to write.");
                reader.Close();
                return;
            }

            int writeCount = 0;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                writer.WriteLine(line);

                if (line.Contains("max_value"))
                {
                    ++writeCount;
                    reader.ReadLine();
                    string nextLine = line.Replace("max_value", "min_value");

                    writer.WriteLine(nextLine);
                    Console.Write("\r    Processing " + writeCount + " items...");
                }
            }
            Console.Write("\r                                                  ");
            Console.WriteLine("\r    Processed " + writeCount + " items.");

            reader.Close();
            writer.Close();
            Console.WriteLine();
        }

        private struct ModItem
        {
            string RawName;
            string FuncType;
            string MaxValue;
            string MinValue;
            string Rarity;
            string StatType;
            string ValueType;

            //public ModItem(int _param)
            //{
            //    RawName = "";
            //    FuncType = "";
            //    MaxValue = "";
            //    MinValue = "";
            //    Rarity = "";
            //    StatType = "";
            //    ValueType = "";
            //}

            public override string ToString()
            {
                string s = "";
                s += RawName;
                s += "\n{\n\tWeaponDesc\n\t{\n";
                s += "\t\tfunction_type " + FuncType;
                s += "\t\tmax_value " + MaxValue;
                s += "\t\tmin_value " + MinValue;
                if (!string.IsNullOrEmpty(Rarity))
                {
                    s += "\t\trarity " + Rarity;
                }


                return s;
            }
        }
    }
}
