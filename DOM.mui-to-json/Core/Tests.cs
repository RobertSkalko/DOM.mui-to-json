using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json
{
    internal class Tests
    {
        public static void runTests()
        {
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            foreach (string file in Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories))
            {
                if (file.EndsWith(".mui"))
                {
                    Stream stream = new FileStream(file, FileMode.Open);
                    var testlist = CsvParser.parseCSV(stream);
                    Console.WriteLine(testlist.Count());
                }
            }
        }
    }
}