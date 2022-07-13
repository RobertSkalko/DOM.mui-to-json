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
            List<DomLine> enchanteds = new List<DomLine>();



            foreach (DomLine line in items.lineObjects.ToList())
            {
                ItemHelper helper = new ItemHelper(line);


                if (helper.IsValidUniqueItem())
                {
                    if (!helper.GetName().Contains("[*]"))
                    {
                        uniques.Add(helper.line);

                        ItemHelper ench = new ItemHelper(line.Copy());

                        ench.SetID("enchanted_" + helper.GetID());
                        ench.SetName(helper.GetName() + " [*]");

                        enchanteds.Add(ench.line);


                    }
                }
                    
                    
                }



            items.lineObjects.AddRange(enchanteds);


            List<String> methods = new List<string>() { "RngAlchemyAddRunes","RngAlchemyAddMaterial"};

            foreach (DomLine unique in uniques)
            {
                foreach (String luamethod in methods)
                {

                    String mat = "";

                    if (luamethod.Equals("RngAlchemyAddRunes")){
                        mat = "crafting_mat_add_runes";
                    }
                    if (luamethod.Equals("RngAlchemyAddMaterial"))
                    {
                        mat = "crafting_mat_add_material";
                    }

                    ItemHelper helper = new ItemHelper(unique);

                    String result = "enchanted_" + helper.GetID();

                    string recipeline = "enchanted_" + helper.GetID() + ";" + helper.GetID() +";" + mat + ";;" + 100 + ";" + result + ";" +luamethod+ ";";

                    var newline = new DomLine
                    {
                        line = recipeline
                    };

                    alchemy.lineObjects.Add(newline);
                }
            }


        }




            }
        }
