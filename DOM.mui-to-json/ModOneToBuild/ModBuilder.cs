using DOM.mui_to_json.Helpers;
using DOM.mui_to_json.ModOneToBuild.files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace DOM.mui_to_json.ModOneToBuild
{
    public class ModBuilder
    {
        public static string BUILD_PATH = "C:/Users/PC/Desktop/Dawn of Magic MODDING/built_mod/";
        public static string MOD_SOURCE_PATH = "C:/Users/PC/Desktop/Dawn of Magic MODDING/DOM_MOD_SOURCE/src/";

        // runes and mats don't drop
        // all gear is indestructable now
        // remove alchemy recipes that make it possible to create new runes on items
        // create runeword set lists for the lua file
        // add [set] label to every item on a set list

        public static void Create()
        {

            DomFile ItemsFile = MainClass.GetFileFromName("items.mui");
            DomFile ItemsFileCSV = MainClass.GetFileFromName("items.mui");
            DomFile Sets = MainClass.GetFileFromName("item_sets.csv");
            DomFile RuneWordStatsFile = MainClass.GetFileFromName("rune_words.mui");
            DomFile alchemyFile = MainClass.GetFileFromName("alchemy_recipes.csv");
            DomFile runewordSetsFile = MainClass.GetFileFromName("rune_sets.csv");

            RuneWordsEdit.Edit(RuneWordStatsFile, MainClass.GetFileFromName("Add-rune_words.mui"));

            ItemsEdit.Edit(ItemsFile, MainClass.GetFileFromName("Add-items.mui"));
            //EnchantedItemRecipes.edit(ItemsFile, alchemyFile);

            AlchemyEdit.Edit(alchemyFile);
            RuneWordsForLua.Edit(runewordSetsFile);

            ItemsEdit.AddSetLabelsToItemNames(ItemsFile, Sets);

            alchemyFile.CreateMuiOrCsvFile();

            ItemsFile.CreateMuiOrCsvFile();
            RuneWordStatsFile.CreateMuiOrCsvFile();

            //ItemsFileCSV.overrideWithOtherFile(ItemsFile);
            //ItemsFileCSV.CreateMuiOrCsvFile();

            CopyToBuildPath("traders_addon.lua");

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = BUILD_PATH,
                UseShellExecute = true,
                Verb = "open"
            });

            Console.WriteLine("Mod Built!");

        }

        public static void ReplaceInFile(String file, String replaceMarker, String replacewith)
        {
            String name = file;

            file = MainClass.SearchFileFromName(file);

            string str = File.ReadAllText(file);
            str = str.Replace(replaceMarker, replacewith);

            File.WriteAllText(BUILD_PATH + name, str);

        }
        public static void CopyToBuildPath(String file)
        {
            String name = file;

            file = MainClass.SearchFileFromName(file);

            string str = File.ReadAllText(file);

            File.WriteAllText(BUILD_PATH + name, str);
        }

    }

}
