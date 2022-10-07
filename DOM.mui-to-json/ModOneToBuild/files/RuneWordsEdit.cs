using DOM.mui_to_json.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json.ModOneToBuild.files
{
    public class RuneWordsEdit
    {
        public static void Edit(DomFile file, DomFile adder)
        {

            file.addOrOverrideEntriesFrom(adder);

            foreach (DomLine line in file.lineObjects.ToList())
            {
                RuneWordHelper helper = new RuneWordHelper(line);
                helper.ReplaceBoostNumProjectilesWithAllStats();
            }

        }
    }
}
