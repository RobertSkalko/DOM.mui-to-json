using CsvHelper;
using DOM.mui_to_json.ModOneToBuild;
using DOM.mui_to_json.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json
{
    internal class MainClass
    {
        public static Encoding ENCODING = Encoding.Default;

        private static Dictionary<string, Action> commands = new Dictionary<string, Action>()
        {          
            {"tomui", new Action(JsonToMui)},
            {"tojson", new Action(MuiToJson)},
            {"openfolder", new Action(OpenFolder)},
            {"filter uniques", new Action(SetupFileWithAllUniques.Create)},
            {"create epic uniques", new Action(CreateEpicVersionsOfUniques.Create)},
            {"create epic stats for uniques",new Action(CreateEpicUniqueStatVersions.Create)},
            {"create combos", new Action(CreateLvlAndRarityVariationsOfStatsAndItems.Create)},
            {"create mat combos", new Action(CreateMaterialVariations.Create) },
            {"remove traders",new Action(MakeAllTradersPotionSellers.Create) },
            {"override stat req",new Action(OverrideStatRequirements.Create) },
            {"gen elite rune recipes",new Action(CreateEliteRuneRecipes.Create) },
            {"remove rune drops",new Action(RemoveRuneDrops.Create) },
            {"create mod",new Action(ModBuilder.Create) },

    };

        public MainClass()
        {
        }

        private static void Main(string[] args)
        {
            PrintCommands();

            while (true)
            {
                string input = Console.ReadLine();

                if (commands.ContainsKey(input))
                {
                    commands[input].Invoke();
                }
                else
                {
                    Console.WriteLine("Unknown Command");
                    PrintCommands();
                }
            }
        }

        private static void PrintCommands()
        {
            Console.WriteLine("Available Commands:");

            int i = 1;
            foreach (string key in commands.Keys)
            {
                Console.WriteLine(i + ") " + key);
                i++;
            }
        }

        private static void OpenFolder()
        {
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            System.Diagnostics.Process.Start(path);
        }

        public static List<DomFile> GetDOMFiles(string ext = "")
        {
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            List<DomFile> domFiles = new List<DomFile>();

            foreach (string file in Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories))
            {
                String name =  Path.GetFileName(file);


                if (ext.Length == 0 || file.EndsWith(ext))
                {
                    DomFile dom = DomFile.Load(file);

                    if (dom != null)
                    {
                        domFiles.Add(dom);
                    }
                }
            }

            return domFiles;
        }
        public static DomFile GetFileFromName(string name )
        {
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

          
            foreach (string file in Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories))
            {
                String getname = Path.GetFileName(file);

                if (name == getname)
                {
                    DomFile dom = DomFile.Load(file);

                    if (dom != null)
                    {
                        return dom;
                    }
                }
            }

            return null;
        }

        private static void JsonToMui()
        {
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            List<DomFile> domFiles = GetDOMFiles(".json");

            if (domFiles.Count > 0)
            {
                foreach (DomFile dom in domFiles)
                {
                    dom.CreateMuiFile();
                }

                Console.WriteLine("Success!");
            }
            else
            {
                Console.WriteLine("Failed, No files");
            }
        }

        // (((?<quote>")|(?(quote)((?<-quote>")|[^"]))*)*|[^;"])*?(?=;|$)
        // (((?<quote>(?<!\\)")|(?(quote)((?<-quote>(?<!\\)")|.))*)*|[^;])*?(?=;|$)

        private static void MuiToJson()
        {
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            List<DomFile> domFiles = GetDOMFiles(".mui");

            if (domFiles.Count > 0)
            {
                foreach (DomFile dom in domFiles)
                {
                    dom.CreateJsonFile();
                }

                Console.WriteLine("Success!");
            }
            else
            {
                Console.WriteLine("Failed, No files");
            }
        }
    }
}