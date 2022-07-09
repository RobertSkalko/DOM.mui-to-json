using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json.Tools
{
    internal class CreateEliteRuneRecipes
    {
        public static void Create()
        {
            DomFile items = MainClass.GetDOMFiles("items.mui")[0];
            DomFile recipes = MainClass.GetDOMFiles("alchemy_recipes.csv")[0];
            DomFile subclasses = MainClass.GetDOMFiles("item_subclasses.mui")[0];

            List<string> all_subclasses = subclasses.lineObjects.Where(x => new ItemHelper(x).GetID().Length > 0).ToList().ConvertAll(x => new ItemHelper(x).GetID());

            foreach (DomLine line in recipes.lineObjects.ToList())
            {               
                    string id = new ItemHelper(line).GetID();

                    if (id.StartsWith("elite_rune_recipe_"))
                    {
                        {
                            recipes.lineObjects.Remove(line);
                        }
                    }
                
            }

            foreach (DomLine itemline in items.lineObjects.ToList())
            {
                var helper = new ItemHelper(itemline);


                if (helper.isEnchanted())
                {
                    continue;
                }

                string subclass = helper.getSubclass();

                if (all_subclasses.Contains(subclass))
                {
                    var subclassObj = subclasses.lineObjects.Where(x => new ItemHelper(x).GetID().Equals(subclass)).First();

                    int runeAmount = int.Parse(subclassObj.dict["runes_number"]);

                    if (runeAmount > 0)
                    {
                        string amountString = 1+"";

                        ItemHelper ench = new ItemHelper(helper.line.Copy());
                        ench.markAsEnchanted();

                        String result = helper.GetID();

                        if (helper.IsValidUniqueItem())
                        {
                            result = ench.GetID();

                            string recipeline = "elite_rune_recipe_" + helper.GetID() + ";" + helper.GetID() + ";;;" + amountString + ";" + result + ";AlchemyAddEliteRunes;";

                            var newline = new DomLine
                            {
                                line = recipeline
                            };

                            recipes.lineObjects.Add(newline);
                        }
                    }
                }
            }
            recipes.CreateMuiFile();
        }
    }
}