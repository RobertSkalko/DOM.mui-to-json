using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json.ModOneToBuild.files
{
    public class EnchantedItemRecipes
    {
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

        }

    }
}
