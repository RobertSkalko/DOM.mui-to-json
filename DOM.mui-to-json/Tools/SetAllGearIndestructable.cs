using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json.Tools
{
    internal class SetAllGearIndestructable
    {
        public static void Create()
        {
            DomFile file = MainClass.GetDOMFiles("items.mui")[0];

            foreach (DomLine line in file.lineObjects)
            {
                if (line.dict.ContainsKey("Item_durability"))
                {
                    if (line.dict["Item_durability"] != "0")
                    {
                        line.dict["Item_durability"] = "-1";
                    }
                }
            }

            file.CreateMuiFile();
        }
    }
}