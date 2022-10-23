using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using File = System.IO.File;

namespace DOM.mui_to_json.ModOneToBuild.files
{
    public class LuaFileChanges
    {

        static string runefunction = "\r\nfunction RuneNAME()\r\n\tmyRandomUniqueWithRune(\"NAME\")\r\nend\r\n";
        static string materialfunction = "\r\nfunction MaterialNAME()\r\n\tmyRandomUniqueWithMaterial(\"NAME\")\r\nend\r\n";
        // replace NAME with rune id

        public static void replaceAll(DomFile runewords, DomFile item)
        {
            String file = "item_generation.lua";

            String name = file;

            file = MainClass.SearchFileFromName(file);

            string str = File.ReadAllText(file);

            str = str.Replace("[RANDOM_RUNEWORD_FUNCTION]", randomRuneword(runewords));
            str = str.Replace("[ALL_SPELL_SCROLLS_FUNCTION]", allScrolls(item));
            str = str.Replace("[RANDOM_UNIQUE_FUNCTION]", randomUniqueFunction(item));
            str = str.Replace("[RANDOM_UNIQUE_WITH_SPECIFIC_RUNE_EACH_FOR_ALCHEMY_CSV]", randomRuneAndMatUniqueFunctions(item));

            File.WriteAllText(ModBuilder.BUILD_PATH + name, str);
        }

        static string randomRuneword(DomFile file)
        {
            String lua = "\nfunction addRandomRuneWord()\n";

            int index = 0;

            int total = -1;

            foreach (DomLine line in file.lineObjects.ToList())
            {
                ItemHelper helper = new ItemHelper(line);
                String id = helper.GetID();

                if (id.Length > 0)
                {
                    total++;
                }
            }

            lua += "local total = " + total + "\n";

            lua += "local index = Generate.RandomInt(0,total)\n";

            foreach (DomLine line in file.lineObjects.ToList())
            {

                ItemHelper helper = new ItemHelper(line);

                String id = helper.GetID();

                if (id.Length > 0)
                {

                    lua += "if index == " + index + " then\n";

                    for (int i = 1; i < 6; i++)
                    {

                        String key = "set_rune_" + i;

                        String val = helper.line.get(key);

                        if (val != null && val.Length > 0)
                        {
                            lua += "Generate.AddRune(\"" + val + "\")\n";
                        }

                    }

                    lua += "end\n";
                    index++;

                }

            }

            lua += "end\n";

            return lua;

        }

        static string allScrolls(DomFile file)
        {
            String lua = "\nfunction allSpellScrolls()\n";

            foreach (DomLine line in file.lineObjects.ToList())
            {
                ItemHelper helper = new ItemHelper(line);

                String id = helper.GetID();

                if (id.Length > 0)
                {
                    if (id.Contains("scroll_"))
                    {

                        lua += "Generate.CreateItemByName (" + "\"" + id + "\")" + ";" + "\n";

                    }
                }

            }

            lua += "end\n";

            return lua;

        }

        static string randomUniqueFunction(DomFile file)
        {

            String lua = "\nfunction myRandomUnique()\n";

            List<DomLine> uniques = new List<DomLine>();

            foreach (DomLine line in file.lineObjects.ToList())
            {
                ItemHelper helper = new ItemHelper(line);

                if (helper.IsValidUniqueItem())
                {
                    uniques.Add(helper.line);
                }
            }

            int total = uniques.Count;

            lua += "local total = " + total + "\n";

            lua += "local index = Generate.RandomInt(0,total)\n";

            int index = 0;

            foreach (DomLine line in uniques)
            {
                ItemHelper helper = new ItemHelper(line);

                String id = helper.GetID();

                if (id.Length > 0)
                {
                    if (helper.IsValidUniqueItem())
                    {
                        lua += "if index == " + index + " then\n";

                        lua += "Generate.CreateItemByName (" + "\"" + id + "\")" + ";" + "\n";

                        lua += "end\n";

                        index++;

                    }
                }

            }

            lua += "end\n";

            return lua;

        }
        static string randomRuneAndMatUniqueFunctions(DomFile items)
        {
            List<DomLine> runes = new List<DomLine>();

            foreach (DomLine line in items.lineObjects.ToList())
            {
                ItemHelper helper = new ItemHelper(line);

                if (helper.getSubclass() == "isc_rune")
                {
                    runes.Add(helper.line);
                }
            }

            List<DomLine> materials = new List<DomLine>();

            foreach (DomLine line in items.lineObjects.ToList())
            {
                ItemHelper helper = new ItemHelper(line);

                if (helper.getSubclass() == "isc_material")
                {
                    materials.Add(helper.line);
                }
            }

            string lua = "";

            foreach (DomLine line in runes)
            {
                lua += runefunction.Replace("NAME", new ItemHelper(line).GetID());
            }
            foreach (DomLine line in materials)
            {
                lua += materialfunction.Replace("NAME", new ItemHelper(line).GetID());
            }

            return lua;

        }

    }
}
