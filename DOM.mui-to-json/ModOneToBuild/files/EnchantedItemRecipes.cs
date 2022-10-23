using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json.ModOneToBuild.files
{
    public class EnchantedItemRecipes
    {
        public static string uniqueDismantleResult = "ic_garbage_dice";

        public static void edit(DomFile items, DomFile alchemy)
        {
            List<DomLine> uniques = new List<DomLine>();

            foreach (DomLine line in items.lineObjects.ToList())
            {
                ItemHelper helper = new ItemHelper(line);

                if (helper.IsValidUniqueItem())
                {
                    uniques.Add(helper.line);
                }
            }
            List<DomLine> runes = new List<DomLine>();

            foreach (DomLine line in items.lineObjects.ToList())
            {
                ItemHelper helper = new ItemHelper(line);

                if (helper.getSubclass() == "isc_rune")
                {
                    runes.Add(helper.line);
                }
            }

            List<DomLine> materials = new List<DomLine>();

            foreach (DomLine line in items.lineObjects.ToList())
            {
                ItemHelper helper = new ItemHelper(line);

                if (helper.getSubclass() == "isc_material")
                {
                    materials.Add(helper.line);
                }
            }
            foreach (DomLine unique in uniques)
            {

                ItemHelper helper = new ItemHelper(unique);

                String result = helper.GetID();

                String luamethod = "myEnchantAlways";

                // todo change 1 to 1000 (poison pots)
                string recipeline = "combine_unique_" + helper.GetID() + ";" + helper.GetID() + ";" + helper.GetID() + ";;" + 1000 + ";" + result + ";" + luamethod + ";";

                var newline = new DomLine
                {
                    line = recipeline
                };

                alchemy.lineObjects.Add(newline);

            }

            foreach (DomLine unique in uniques)
            {

                ItemHelper helper = new ItemHelper(unique);

                string recipeline = "dismantle_unique_" + helper.GetID() + ";" + helper.GetID() + ";" + ";;" + 1 + ";" + uniqueDismantleResult + ";" + ";";

                var newline = new DomLine
                {
                    line = recipeline
                };

                alchemy.lineObjects.Add(newline);

            }

            foreach (DomLine rune in runes)
            {

                ItemHelper helper = new ItemHelper(rune);

                string lua = "RuneNAME".Replace("NAME", helper.GetID());

                // todo price and mats add back
                string recipeline = "rnd_uniq_rune_" + helper.GetID() + ";" + helper.GetID() + ";" + uniqueDismantleResult + ";" + uniqueDismantleResult + ";" + 1000 + ";" + "" + ";" + lua + ";";

                var newline = new DomLine
                {
                    line = recipeline
                };

                alchemy.lineObjects.Add(newline);

            }
            foreach (DomLine material in materials)
            {

                ItemHelper helper = new ItemHelper(material);

                string lua = "MaterialNAME".Replace("NAME", helper.GetID());

                string recipeline = "rnd_uniq_mat_" + helper.GetID() + ";" + helper.GetID() + ";" + uniqueDismantleResult + ";" + uniqueDismantleResult + ";" + 1000 + ";" + "" + ";" + lua + ";";

                var newline = new DomLine
                {
                    line = recipeline
                };

                alchemy.lineObjects.Add(newline);

            }

        }

    }
}
