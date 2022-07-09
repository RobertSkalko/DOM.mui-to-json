using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json.Tools
{
    internal class OverrideStatRequirements
    {
        public static void Create()
        {
            DomFile original = MainClass.GetDOMFiles("original.mui")[0];
            DomFile toOverride = MainClass.GetDOMFiles("toOverride.json")[0];

            foreach (DomLine toOverLine in toOverride.lineObjects)
            {
                ItemHelper helper = new ItemHelper(toOverLine);

                string id = helper.GetID().Replace("_epic", "");

                foreach (DomLine originalLine in original.lineObjects)
                {
                    ItemHelper helper2 = new ItemHelper(originalLine);

                    if (helper2.GetID().Equals(id))
                    {
                        toOverLine.dict["use_char_required"] = originalLine.dict["use_char_required"];
                        toOverLine.dict["Use_char_value"] = originalLine.dict["Use_char_value"];
                    }
                }
            }

            toOverride.CreateJsonFile();
        }
    }
}