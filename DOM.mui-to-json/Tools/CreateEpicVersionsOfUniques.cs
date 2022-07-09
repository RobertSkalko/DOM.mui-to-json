using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json
{
    internal class CreateEpicVersionsOfUniques
    {
        public static void Create()
        {
            List<DomFile> domFiles = MainClass.GetDOMFiles();

            foreach (DomFile file in domFiles)
            {
                file.AddFileNameSuffix("epics");

                foreach (DomLine line in file.lineObjects)
                {
                    ItemHelper helper = new ItemHelper(line);

                    helper.SetDropChance(1);
                    helper.SetDropStartsAtLvl(1);
                    helper.SetID(helper.GetID() + "_epic");
                    helper.SetDropStartsAtLvl(1);
                    helper.AllowAsRandomDrop();

                    line.dict["Item_lit_name"] = "Epic " + line.dict["Item_lit_name"];
                    line.dict["RuneWord_socketed"] = line.dict["RuneWord_socketed"] + "_epic";
                }
            }

            domFiles.ForEach(x => x.CreateJsonFile());
        }
    }
}