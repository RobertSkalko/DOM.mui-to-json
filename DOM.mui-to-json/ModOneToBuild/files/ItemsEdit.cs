using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json.ModOneToBuild
{
    public class ItemsEdit
    {
        public static void Edit(DomFile file, DomFile adder)
        {

            file.addOrOverrideEntriesFrom(adder);

            foreach (DomLine line in file.lineObjects.ToList())
            {
                ItemHelper helper = new ItemHelper(line);

                if (helper.getSubclass() == "isc_rune" || helper.getSubclass() == "isc_material")
                {
                    helper.SetDropChance(0);
                    helper.SetRequiremnt(999);

                }

                // i can use traders and smiths, so why disable durability?
                /*
                if (line.dict.ContainsKey("Item_durability"))
                {
                    if (line.dict["Item_durability"] != "0")
                    {
                        line.dict["Item_durability"] = "-1";
                    }
                }
                */

            }

        }
        public static void AddSetLabelsToItemNames(DomFile file, DomFile sets)
        {

            foreach (DomLine line in sets.lineObjects.ToList())
            {
                ItemHelper helper = new ItemHelper(line);

                for (int i = 1; i < 9; i++)
                {

                    String key = "set_item_" + i;

                    String itemID = helper.line.get(key);

                    if (itemID != null && itemID.Length > 0)
                    {
                        if (file.containsId(itemID))
                        {
                            int index = file.indexOfWithId(itemID);
                            ItemHelper item = new ItemHelper(file.lineObjects[index]);

                            String SetNameAdd = "[Set]";

                            if (!item.GetName().Contains(SetNameAdd))
                            {
                                item.SetName(item.GetName() + " " + SetNameAdd);
                            }
                        }
                    }

                }

            }

        }

    }
}
