using DOM.mui_to_json.Helpers;
using DOM.mui_to_json.Helpers.LvlRarityVariations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json.Tools
{
    internal class CreateLvlAndRarityVariationsOfStatsAndItems
    {
        public static void Create()
        {
            DomFile items = MainClass.GetDOMFiles("epic-items.json")[0];
            DomFile stats = MainClass.GetDOMFiles("epic-rune_words.json")[0];

            items.lineObjects = items.lineObjects.Where(x => new ItemHelper(x).IsValidUniqueItem()).ToList(); // only uniques
            RemoveUnusedStats(items, stats); // only stats for the uniques

            DomFile finalItems = new DomFile(items);
            DomFile finalStats = new DomFile(stats);

            finalStats.lineObjects.Clear();
            finalItems.lineObjects.Clear();

            foreach (Tuple<ItemLvl, ItemStar> tuple in getCombinations())
            {
                foreach (DomLine item in items.lineObjects)
                {
                    DomLine copy = item.Copy();

                    ItemHelper helper = new ItemHelper(copy);

                    tuple.Item1.ModifyItem(helper);
                    tuple.Item2.ModifyItem(helper);

                    helper.SetPrice(tuple.Item1, tuple.Item2);
                    helper.MultiplyBaseStats(tuple.Item1, tuple.Item2);
                    helper.SetDurability(-1);
                    helper.SetAlwaysHighlight();
                    helper.SetName(helper.GetName().Replace("Epic ", "")); // removes epic from name                 
                 

                    finalItems.lineObjects.Add(copy);


                    DomLine copy2 = copy.Copy();
                    ItemHelper helper2 = new ItemHelper(copy2);
                    helper2.markAsEnchanted();
                    helper2.setDesc("Enchanted " + helper2.getDesc());


                    finalItems.lineObjects.Add(copy2);

                }

                foreach (DomLine stat in stats.lineObjects)
                {
                    DomLine copy = stat.Copy();
                    tuple.Item1.ModifyStats(new RuneWordHelper(copy));
                    tuple.Item2.ModifyStats(new RuneWordHelper(copy));
                    finalStats.lineObjects.Add(copy);
                }
            }

            finalItems.AddFileNameSuffix("lvl_rarity_vars");
            finalStats.AddFileNameSuffix("lvl_rarity_vars");

            finalItems.lineObjects.ForEach(x => new ItemHelper(x).FixName());

            finalItems.CreateJsonFile();
            finalStats.CreateJsonFile();

            finalItems.CreateMuiFile();
            finalStats.CreateMuiFile();
        }

        public static void RemoveUnusedStats(DomFile items, DomFile stats)
        {
            List<string> allUsedStats = new List<string>();
            items.lineObjects.ForEach(x => allUsedStats.Add(x.dict["RuneWord_socketed"]));
            stats.lineObjects = stats.lineObjects.Where(x => x.dict.ContainsKey("@head") && allUsedStats.Contains(x.dict["@head"])).ToList();
        }

        public static List<Tuple<ItemLvl, ItemStar>> getCombinations()
        {
            List<Tuple<ItemLvl, ItemStar>> list = new List<Tuple<ItemLvl, ItemStar>>();

            foreach (ItemLvl lvl in ItemLvl.ALL)
            {
                foreach (ItemStar star in ItemStar.ALL)
                {
                    list.Add(Tuple.Create(lvl, star));
                                       
                }
            }
            return list;
        }
    }
}