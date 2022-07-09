using DOM.mui_to_json.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json.Tools
{
    internal class CreateEpicUniqueStatVersions
    {
        public static void Create()
        {
            List<DomFile> domFiles = MainClass.GetDOMFiles();

            foreach (DomFile file in domFiles)
            {
                file.AddFileNameSuffix("-epics");

                List<DomLine> uniques = new List<DomLine>();

                foreach (DomLine line in file.lineObjects)
                {
                    RuneWordHelper helper = new RuneWordHelper(line);

                    if (helper.IsUniqueStats() && !helper.IsStarterWeapon())
                    {
                        helper.SetID(helper.GetID() + "_epic");

                        uniques.Add(helper.line);
                    }
                }

                file.lineObjects = uniques;
            }

            domFiles.ForEach(x => x.CreateJsonFile());
        }
    }
}