using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json
{
    internal class SetupFileWithAllUniques
    {
        public static void Create()
        {
            List<DomFile> domFiles = MainClass.GetDOMFiles();
            List<DomFile> end = MainClass.GetDOMFiles();

            foreach (DomFile file in domFiles)
            {
                DomFile newfile = new DomFile(file)
                {
                    lineObjects = file.lineObjects.Where(x => new ItemHelper(x).IsValidUniqueItem()).ToList()
                };

                newfile.AddFileNameSuffix("filtered");

                end.Add(newfile);
            }

            end.ForEach(x => x.CreateJsonFile());
        }
    }
}