using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json.ModOneToBuild.files
{
    public class AlchemyEdit
    {

        public static void Edit(DomFile file)
        {

            foreach (DomLine line in file.lineObjects.ToList())
            {
                ItemHelper helper = new ItemHelper(line);

                String id = helper.GetID();

                if (
                    id.Contains("repair")
                    || id.Contains("runic_grow")
                    || id.Contains("material_grow")
                    || id.Contains("rnd_class")
                    )
                {

                    file.lineObjects.Remove(line);

                }

                if (helper.IsElementRecipe())
                {
                    helper.line.dict["function_bonus"] = "myEnchantAlways";
                }

            }

            file.CreateMuiOrCsvFile();

        }
    }
}
