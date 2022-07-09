using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json.Helpers.LvlRarityVariations
{
    internal class ItemStar
    {
        public static List<ItemStar> ALL = new List<ItemStar>()
        {
            new ItemStar(1,0.0005F,"Rare"),
            new ItemStar(2,0.00025F,"Epic"),
            new ItemStar(3,0.0001F,"Legendary"),
            new ItemStar(4,0.00005F,"Mythical"),
            new ItemStar(5,0.00001F,"Godly")
        };

        public ItemStar(int stars, float dropchance, string rarityName)
        {
            this.stars = stars;
            this.idSuffix = "_stars_" + this.stars;
            this.nameSuffix = " [" + new string('*', this.stars) + "]";
            this.dropChance = dropchance;
            this.rarityName = rarityName;
        }

        public float dropChance;
        public string idSuffix;
        public string nameSuffix;
        public int stars;
        public string rarityName;

        public void ModifyItem(ItemHelper helper)
        {

            helper.setDesc(rarityName + " Item");
            helper.SetID(helper.GetID() + idSuffix);
           //helper.SetName(helper.GetName() + nameSuffix); the stars look ugly and remove immersion for some reason
            helper.line.dict["RuneWord_socketed"] = helper.line.dict["RuneWord_socketed"] + this.idSuffix;
            helper.SetDropChance(this.dropChance);
        }

        public void ModifyStats(RuneWordHelper helper)
        {
            helper.SetID(helper.GetID() + this.idSuffix);
            helper.IncreaseFirstValuesBy(this.stars - 1);
            helper.IncreaseSecondValuesBy(this.stars - 1);
        }
    }
}