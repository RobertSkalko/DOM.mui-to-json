using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json.Helpers
{
    internal class RuneStatsHelper : BaseHelper
    {
        public RuneStatsHelper(DomLine line)
        {
            this.line = line;
        }

        public void SetSocketChanceUpgrade(float f)
        {
            line.dict["Socket_chance"] = f + "";
        }

        public void SetSocketChance()
        {
            string chance = line.dict["Socket_chance"];

            if (chance != "0")
            {
                line.dict["Socket_chance"] = "0.2";
            }
        }

        public static bool IsPositiveEffect(string name)
        {
            return !name.Contains("neg") && !name.Contains("reduce");
        }
    }
}