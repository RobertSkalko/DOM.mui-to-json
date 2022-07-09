using DOM.mui_to_json.Helpers;
using DOM.mui_to_json.ModOneToBuild.files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json.ModOneToBuild
{
    public class ModBuilder
    {
        // runes don't drop
        // all gear is indestructable now
        // remove alchemy recipes that make it possible to create new runes on items
        // create runeword set lists for the lua file

        public static void Create()
        {

            DomFile ItemsFile = MainClass.GetFileFromName("items.mui");
            DomFile Sets = MainClass.GetFileFromName("item_sets.csv");                      
            DomFile RuneWordStatsFile = MainClass.GetFileFromName("rune_words.mui");
            DomFile alchemyFile = MainClass.GetFileFromName("alchemy_recipes.csv");
            DomFile runewordSetsFile = MainClass.GetFileFromName("rune_sets.csv");


            RuneWordsEdit.Edit(RuneWordStatsFile, MainClass.GetFileFromName("Add-rune_words.mui"));

            ItemsEdit.Edit(ItemsFile, MainClass.GetFileFromName("Add-items.mui"));

            AlchemyEdit.Edit(alchemyFile);

            RuneWordsForLua.Edit(runewordSetsFile);


            ItemsEdit.AddSetLabelsToItemNames(ItemsFile, Sets);

            Console.WriteLine("Mod Built!");

        }

    }

}
