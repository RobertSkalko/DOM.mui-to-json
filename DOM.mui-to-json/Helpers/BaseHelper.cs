using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json.Helpers
{
    internal abstract class BaseHelper

    {
        public DomLine line;

        public string GetID()
        {
            if (line.dict.ContainsKey("@HEAD"))
            {
                return line.get("@HEAD");
            }
            else if (line.dict.ContainsKey("@head"))
            {
                return line.get("@head");
            }

            return "";
        }

        public void SetID(string id)
        {
            line.dict["@head"] = id;
        }

        public int GetInt(string key, int fallback)
        {
            if (line.dict.ContainsKey(key))
            {
                string val = line.dict[key];

                int num = 0;

                if (int.TryParse(val, out num))
                {
                    return num;
                }
            }
            return fallback;
        }

        public float GetFloat(string key, float fallback)
        {
            if (line.dict.ContainsKey(key))
            {
                string val = line.dict[key];

                float num = 0;

                if (float.TryParse(val, out num))
                {
                    return num;
                }
            }
            return fallback;
        }
    }
}