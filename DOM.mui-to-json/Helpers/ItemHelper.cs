using DOM.mui_to_json.Helpers;
using DOM.mui_to_json.Helpers.LvlRarityVariations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json
{
    internal class ItemHelper : BaseHelper
    {

        public String getDesc()
        {
            return this.line.dict["Item_lit_desc"];
        }

        public void setDesc(String desc)
        {
            this.line.dict["Item_lit_desc"] = desc;
        }
        public ItemHelper(DomLine line)
        {
            this.line = line;
        }

        public void markAsEnchanted()
        {
            this.SetID("ench_" + GetID());
        }

        public Boolean isEnchanted()
        {
            return GetID().Contains("ench_");
        }

        public string getSubclass()
        {
            if (this.line.dict.ContainsKey("Item_subclass"))
            {
                return this.line.dict["Item_subclass"];
            }
            return "";
        }

        public void SetPrice(int price)
        {
            line.dict["Item_price"] = price + "";
        }

        private bool IsUniqueItem()
        {
            return line.get("Unique") == "1";
        }

        public string GetName()
        {
            return line.dict["Item_lit_name"];
        }

        public void SetDurability(int dura)
        {
            line.dict["Item_durability"] = dura + "";
        }

        public void SetDurability(ItemLvl lvl, ItemStar star)
        {
            int itemdura = getDurability();

            if (itemdura > 0) // if not indesctructable or not using durability like earrings
            {
                int dura = itemdura + (lvl.statBoost + 1 + star.stars) * 25;
                this.SetDurability(dura);
            }
        }

        public int getDurability()
        {
            return int.Parse(line.dict["Item_durability"]);
        }

        public void SetPrice(ItemLvl lvl, ItemStar star)
        {
            int price = 25 * lvl.lvl * (star.stars + 1);
            this.SetPrice(price);
        }

        public void SetAlwaysHighlight()
        {
            line.dict["Always_highlight"] = 1 + "";
        }

        public void MultiplyBaseStats(ItemLvl lvl, ItemStar star)
        {
            float multi = lvl.coreStatMulti;

            int res = this.GetInt("Resistance", 0);
            float imm = this.GetFloat("Immunity", 0);
            int min = this.GetInt("Dmg_min", 0);
            int max = this.GetInt("Dmg_max", 0);

            if (res > 0)
            {
                line.dict["Resistance"] = ((int)(res * multi)).ToString();
            }
            if (imm > 0)
            {
                line.dict["Immunity"] = (imm * multi).ToString();
            }
            if (min > 0)
            {
                line.dict["Dmg_min"] = ((int)(min * multi)).ToString();
            }
            if (max > 0)
            {
                line.dict["Dmg_max"] = ((int)(max * multi)).ToString();
            }
        }

        public void FixName()
        {
            if (this.GetName().Contains("Epic The"))
            {
                this.SetName(this.GetName().Replace("Epic The", "The Epic"));
            }
        }

        public void SetName(string name)
        {
            line.dict["Item_lit_name"] = name;
        }

        public void AllowAsRandomDrop()
        {
            line.dict["generate_by_name"] = "0";
        }

        public bool IsNotTotem()
        {
            return line.get("Item_lit_name").ToLower().Contains("totem") == false;
        }

        public bool IsValidUniqueItem()
        {
            if (!line.dict.ContainsKey("Item_lit_name"))
            {
                return false;
            }
            if (this.GetID().Contains("cross_full"))
            {
                return false;
            }

            return this.IsUniqueItem() && line.get("RuneWord_socketed").Length > 2;
        }

    
        public void SetLevelRequiremnt(int lvl)
        {
            line.dict["use_char_required"] = "LEVEL";
            line.dict["Use_char_value"] = lvl + "";
        }
        public void SetRequiremnt(int lvl)
        {
             line.dict["Use_char_value"] = lvl + "";
        }

        public void SetDropChance(float f)
        {
            line.dict["Monster_chance"] = f + "";
        }

        public void MultiplyUseRequirement(ItemLvl itemlvl)
        {
            int num = 1;
            try
            {
                num = int.Parse(this.line.dict["Use_char_value"]);
            }
            catch (Exception e)
            {
                num = 0;
            }

            float req = num * itemlvl.reqMulti;

            this.line.dict["Use_char_value"] = req + "";
        }

        public void SetDropStartsAtLvl(int f)
        {
            line.dict["Available_from_level"] = f + "";
        }
    }
}