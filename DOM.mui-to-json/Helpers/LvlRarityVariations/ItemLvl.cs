using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json.Helpers.LvlRarityVariations
{
    internal class ItemLvl
    {
        public static List<ItemLvl> ALL = new List<ItemLvl>()
        {
            new ItemLvl(5,0,0.5F,1),new ItemLvl(20,1,0.75F,1.2F),new ItemLvl(30,2,1F,1.5F),new ItemLvl(50,3,1.25F,1.8F),
        };

        public ItemLvl(int lvl, int statboost, float coreStatMulti, float reqMulti)
        {
            this.lvl = lvl;
            this.idPrefix = "lvl_" + lvl + "_";
            this.namePrefix = ""; // "Lvl:[" + lvl + "] ";
            this.statBoost = statboost;
            this.coreStatMulti = coreStatMulti;
            this.reqMulti = reqMulti;
        }

        public string idPrefix;
        public string namePrefix;
        public int lvl;
        public int statBoost;
        public float coreStatMulti;
        public float reqMulti;

        public void ModifyItem(ItemHelper helper)
        {
            helper.SetID(idPrefix + helper.GetID());
            helper.SetName(namePrefix + helper.GetName());
            helper.line.dict["RuneWord_socketed"] = this.idPrefix + helper.line.dict["RuneWord_socketed"];
            helper.MultiplyUseRequirement(this);
            helper.SetDropStartsAtLvl(lvl - 5);
        }

        public void ModifyStats(RuneWordHelper helper)
        {
            helper.SetID(this.idPrefix + helper.GetID());

            if (this.statBoost > 0)
            {
                helper.IncreaseFirstValuesBy(statBoost);
                helper.IncreaseSecondValuesBy(statBoost);
            }
        }
    }
}