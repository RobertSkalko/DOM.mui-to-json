using DOM.mui_to_json.Helpers;
using DOM.mui_to_json.Helpers.LvlRarityVariations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json.Tools
{
    internal class CreateMaterialVariations

    {
        public static void Create()
        {
            DomFile file = MainClass.GetDOMFiles("rune_words.mui")[0];
            file.lineObjects = file.lineObjects.Where(x => new RuneWordHelper(x).IsMaterial() && new RuneWordHelper(x).IsNotUndisiredMaterial()).ToList();

            DomFile vars = new DomFile(file);
            vars.lineObjects.Clear();

            foreach (DomLine line in file.lineObjects)
            {
                foreach (ItemStar star in ItemStar.ALL)
                {
                    DomLine copy = line.Copy();

                    RuneWordHelper helper = new RuneWordHelper(copy);
                    helper.SetID(helper.GetID() + "_" + star.rarityName.ToLower());
                    helper.AddMaterialNamePrefix(star.rarityName + " ");
                    helper.IncreaseFirstValuesBy(star.stars + 1);
                    helper.IncreaseSecondValuesBy((star.stars + 1) * 2);
                    copy.dict["identifier"] = copy.dict["identifier"] + star.stars;

                    vars.lineObjects.Add(copy);
                }
            }

            vars.AddFileNameSuffix("_materials");
            vars.CreateJsonFile();
            vars.CreateMuiFile();
        }
    }
}