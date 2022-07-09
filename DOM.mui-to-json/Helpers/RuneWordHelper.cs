using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json.Helpers
{
    // materials seem to have identifier-s ?
    // runewords arent just runewords, but unique item stats ect. blame the devs on the weird naming lol
    internal class RuneWordHelper : BaseHelper
    {
        public RuneWordHelper(DomLine line)
        {
            this.line = line;
        }

        new public string GetType()
        {
            if (line.dict.ContainsKey("type"))
            {
                return line.dict["type"];
            }
            else
            {
                return "";
            }
        }

        public bool IsStarterWeapon()
        {
            return this.GetID().Contains("startup");
        }

        public bool IsUniqueStats()
        {
            return GetType() == "UNIQUE_ITEM" && !IsStarterWeapon() && !IsBodyMorphing();
        }

        public bool IsItemSet()
        {
            return GetType() == "ITEM_SET";
        }

        public bool IsMaterial()
        {
            return GetType() == "MATERIAL";
        }

        public bool IsRuneWord()
        {
            return GetType() == "RUNE_WORD";
        }

        public bool IsNotUndisiredMaterial()
        {
            return this.GetID() != "material_04" && this.GetID() != "material_42";
        }

        public void AddMaterialNamePrefix(string prefix)
        {
            string name = this.line.dict["lit_name_result"];

            this.line.dict["lit_name_result"] = "«" + prefix + name.Substring(1, name.Length - 2) + "»";
        }

        public bool IsBodyMorphing()
        {
            return GetType() == "MORPHING" || this.GetID().Contains("full");
        }

        public void RemoveBoostNumStats()
        {
            foreach (string key in this.line.dict.Keys.ToList())
            {
                if (line.dict.ContainsKey(key))
                {
                    String str = line.dict[key];

                   if (str.Contains("boost_num"))
                    {
                        line.dict[key] = "";
                    }
                }              
            }
        }

        public void IncreaseSecondValuesByTimes(int times)
        {
            foreach (string key in this.line.dict.Keys.ToList())
            {
                if (IsSecondValueOfStatStrength(key))
                {
                    if (line.dict.ContainsKey(key))
                    {
                        int num = this.GetInt(key, 0);
                        if (num > 0)
                        {
                            line.dict[key] = (num * times) + "";
                        }
                    }
                }
            }
        }

        public void IncreaseFirstValuesByTimes(int times)
        {
            foreach (string key in this.line.dict.Keys.ToList())
            {
                if (IsFirstValueOfStatStrength(key))
                {
                    if (line.dict.ContainsKey(key))
                    {
                        int num = this.GetInt(key, 0);
                        if (num > 0)
                        {
                            line.dict[key] = (num * times) + "";
                        }
                    }
                }
            }
        }

        public void IncreaseSecondValuesBy(int x)
        {
            foreach (string key in this.line.dict.Keys.ToList())
            {
                if (IsSecondValueOfStatStrength(key))
                {
                    if (line.dict.ContainsKey(key))
                    {
                        int num = this.GetInt(key, 0);
                        if (num > 0)
                        {
                            line.dict[key] = (num + x) + "";
                        }
                    }
                }
            }
        }

        public void IncreaseFirstValuesBy(int x)
        {
            foreach (string key in this.line.dict.Keys.ToList())
            {
                if (IsFirstValueOfStatStrength(key))
                {
                    if (line.dict.ContainsKey(key))
                    {
                        int num = this.GetInt(key, 0);
                        if (num > 0)
                        {
                            line.dict[key] = (num + x) + "";
                        }
                    }
                }
            }
        }

        private bool IsFirstValueOfStatStrength(string name)
        {
            return name.Equals("rune_lvl_1_1") || name.Equals("rune_lvl_2_1") || name.Equals("rune_lvl_3_1") || name.Equals("rune_lvl_4_1") || name.Equals("rune_lvl_5_1") || name.Equals("rune_lvl_6_1");
        }

        private bool IsSecondValueOfStatStrength(string name)
        {
            return name.Equals("rune_lvl_1_10") || name.Equals("rune_lvl_2_10") || name.Equals("rune_lvl_3_10") || name.Equals("rune_lvl_4_10") || name.Equals("rune_lvl_5_10") || name.Equals("rune_lvl_6_10");
        }
    }
}