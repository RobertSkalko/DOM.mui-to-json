using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json.ModOneToBuild.files
{
    public class RuneWordsForLua
    {


        public static void Edit(DomFile file)
        {
            String lua = "function addRandomRuneWord()\n";


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

                    lua+= "if index == " + index + " then\n";


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

            file.createLuaFile(lua);
           
            //Console.Write(lua);

        }


        /*
        
        
        public static void Edit(DomFile file)
        {
            String lua = "RUNE_SETS = {\n";


            foreach (DomLine line in file.lineObjects.ToList())
            {

                ItemHelper helper = new ItemHelper(line);




                String id = helper.GetID();

                if (id.Length > 0)
                {
                    String str = "['" + id + "'] = {";


                    for (int i = 1; i < 6; i++)
                    {
                        String key = "set_rune_" + i;

                        String val = helper.line.get(key);

                        if (val != null && val.Length > 0)
                        {
                            str += "'" +val+ "'" + ",";
                        }

                    }

                    str = str.Substring(0, str.Length - 1); // todo

                    str += "},\n";

                    lua += str;

                }
            }

            lua += "}\n";


            file.createLuaFile(lua);
           
            //Console.Write(lua);

        }

        */

    }
}
