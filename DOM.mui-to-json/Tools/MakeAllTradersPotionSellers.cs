using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json.Tools
{
    internal class MakeAllTradersPotionSellers
    {
        public static void Create()
        {
            DomFile dom = MainClass.GetDOMFiles("character_quest.mui")[0];

            dom.AddFileNameSuffix("-altered");

            foreach (DomLine line in dom.lineObjects.ToList())
            {
                ItemHelper helper = new ItemHelper(line);

                if (helper.GetID().Contains("jewish") == false)
                {
                    foreach (string key in line.dict.Keys.ToList())
                    {
                        if (key.ToLower().Contains("menutext"))
                        {
                            if (line.dict[key].Equals("Trade"))
                            {
                                string last = key.Substring(key.Length - 1);

                                int num = -1;
                                bool can = int.TryParse(last, out num);

                                if (can)
                                {
                                    line.dict["MenuEvent" + num] = "jewish_trade";
                                }
                            }
                        }
                    }
                }
            }

            dom.CreateMuiFile();
        }
    }
}