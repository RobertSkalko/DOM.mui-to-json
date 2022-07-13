using DOM.mui_to_json.ModOneToBuild;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json
{
    public class DomFile
    {
        public List<string> firstLineParts = new List<string>();
        public List<DomLine> lineObjects = new List<DomLine>();

        public char delimiter = ';';
        public string path;

        public DomFile()
        {
        }

        public bool containsId(String id)
        {
            return indexOfWithId(id) > -1;
        }
        public int indexOfWithId(String id)
        {

           for (int i =0; i < lineObjects.Count; i++)
            {
                DomLine x = lineObjects[i];

                if (new  ItemHelper(x).GetID() == id) {

                    return i;

                }

            }

            return -1;
        }

        public void replace(String id, DomLine line)
        {
            int index = indexOfWithId(id);
            lineObjects[index] = line;
        }

        public DomFile(DomFile file)
        {
            this.delimiter = file.delimiter;
            this.firstLineParts = new List<string>(file.firstLineParts);
            this.path = file.path;
            this.lineObjects = new List<DomLine>(file.lineObjects);
        }

        public static DomFile Load(string path)
        {
            if (path.EndsWith(".json"))
            {
                DomFile dom = JsonConvert.DeserializeObject<DomFile>(File.ReadAllText(path, MainClass.ENCODING));

                return dom;
            }
            else if (path.EndsWith(".mui") || path.EndsWith(".csv"))
            {
                return new DomFile(path);
            }

            return null;
        }

        public void addOrOverrideEntriesFrom(DomFile adder)
        {

            foreach (DomLine obj in adder.lineObjects)
            {

                int index = indexOfWithId(new ItemHelper(obj).GetID());

                if (index > -1)
                {
                    lineObjects[index] = obj;
                }
                else
                {
                    lineObjects.Add(obj);
                }

            }

        }

        private DomFile(string path)
        {
            this.path = path;

            var csv = CsvParser.parseCSV(new FileStream(path, FileMode.Open));

            this.setupLineObjects(csv);
        }

        public String getWritePath()
        {
            String name = Path.GetFileName(path);

            string dir = ModBuilder.BUILD_PATH;
            
            return dir + name;

        }

        public void CreateJsonFile()
        {
            string jsonpath = this.getJsonPath();

            string json = JsonConvert.SerializeObject(this, Formatting.Indented);

            if (!File.Exists(jsonpath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(jsonpath));
                FileStream file = File.Create(jsonpath);
                file.Close();
            }

            File.WriteAllText(jsonpath, json, MainClass.ENCODING);
            Console.WriteLine("New .json at : " + this.path);
        }
        public void createLuaFile(String lua)
        {
            string jsonpath = this.getLuaPath();

            string json = lua;

            if (!File.Exists(jsonpath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(jsonpath));
                FileStream file = File.Create(jsonpath);
                file.Close();
            }

            File.WriteAllText(jsonpath, json, MainClass.ENCODING);
            Console.WriteLine("New .lua at : " + this.path);
        }

        public void CreateMuiOrCsvFile()
        {
            string muipath = getMuIOrCsvPath(); 

            string mui = this.getMuiString();

            if (!File.Exists(muipath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(muipath));
                FileStream file = File.Create(muipath);
                file.Close();
            }

            File.WriteAllText(muipath, mui, MainClass.ENCODING);
            Console.WriteLine("New .mui at : " + this.path);
        }
        public void CreateMuiFile()
        {
            string muipath = getMuiPath(); // todo idk if this breaks anything, this before used .csv if there was, and if not used mui

            string mui = this.getMuiString();

            if (!File.Exists(muipath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(muipath));
                FileStream file = File.Create(muipath);
                file.Close();
            }

            File.WriteAllText(muipath, mui, MainClass.ENCODING);
            Console.WriteLine("New .mui at : " + this.path);
        }

        public void CreateCsvFile()
        {
            string muipath = getCsvPath();

            string mui = this.getMuiString();

            if (!File.Exists(muipath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(muipath));
                FileStream file = File.Create(muipath);
                file.Close();
            }

            File.WriteAllText(muipath, mui, MainClass.ENCODING);
            Console.WriteLine("New .mui at : " + this.path);
        }

        private string getMuiString()
        {
            StringBuilder sb = new StringBuilder(string.Join(this.delimiter.ToString(), this.firstLineParts));

            if (sb.ToString().Contains(Environment.NewLine) == false)
            {
                sb.Append(Environment.NewLine);
            }

            foreach (DomLine line in this.lineObjects)
            {
                sb.Append(line.toMuiLine(this.firstLineParts, this.delimiter));
            }

            string contents = sb.ToString().TrimEnd();
            List<string> lines = contents.Split(new char[] { '\n', '\r' }).ToList();
            lines = lines.Where(x => x.Length > 3).ToList();

            return string.Join(Environment.NewLine, lines);
        }

        public string getMuIOrCsvPath()
        {
            if (path.EndsWith("csv"))
            {
                return getWritePath();
            }

            return Path.ChangeExtension(getWritePath(), ".mui");
        }

        public string getMuiPath()
        {
            return Path.ChangeExtension(getWritePath(), ".mui");
        }
        public string getCsvPath()
        {          
            return Path.ChangeExtension(getWritePath(), ".csv");
        }
        public string getJsonPath()
        {
            return Path.ChangeExtension(getWritePath(), ".json");
        }
        public string getLuaPath()
        {
            return Path.ChangeExtension(getWritePath(), ".lua");
        }

        private void setupLineObjects(List<List<string>> csv)
        {
            this.firstLineParts = csv[0];
            csv.RemoveAt(0);

            foreach (List<string> list in csv)
            {
                this.lineObjects.Add(new DomLine(list, firstLineParts, this.delimiter));
            }
        }

        public void AddFileNameSuffix(string suffix)
        {
            string ext = Path.GetExtension(this.path);
            string name = Path.GetFileNameWithoutExtension(this.path);
            string folder = Path.GetDirectoryName(this.path);
            this.path = folder + Path.DirectorySeparatorChar + name + suffix + ext;
        }
    }
}