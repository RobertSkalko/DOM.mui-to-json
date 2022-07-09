using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json.Tools
{
    public class RemoveRuneDrops

    { 
        public static void Create()
    {
        DomFile dom = MainClass.GetDOMFiles("items.mui")[0];

        //dom.AddFileNameSuffix("-altered");

        foreach (DomLine line in dom.lineObjects.ToList())
        {
            ItemHelper helper = new ItemHelper(line);

            if (helper.getSubclass() == "isc_rune")
            {
                    helper.SetDropChance(0);
            }
        }

        dom.CreateMuiFile();
    }

    }
}
