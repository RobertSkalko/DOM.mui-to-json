using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json
{
    public class DomLine
    {
        public Dictionary<string, string> dict = new Dictionary<string, string>();
        public string line = "";

        public DomLine()
        {
        }

        public DomLine Copy()
        {
            DomLine copy = new DomLine
            {
                line = line,
                dict = new Dictionary<string, string>(dict)
            };

            return copy;
        }

        public string get(string key)
        {
            if (dict.ContainsKey(key))
            {
                return dict[key];
            }
            else
            {
                return "";
            }
        }

        public DomLine(List<string> parts, List<string> firstLineParts, char delimiter)
        {
            this.setupDict(parts, firstLineParts, delimiter);
        }

        public string toMuiLine(List<string> firstLineParts, char delimiter)
        {
            if (this.dict.Count > 0)
            {
                string line = "";
                foreach (string val in firstLineParts)
                {
                    line += this.dict[val];
                    line += delimiter;
                }

                if (line.Contains(Environment.NewLine) == false)
                {
                    line += Environment.NewLine;
                }

                if (line.EndsWith(delimiter.ToString()))
                {
                    line = line.Remove(line.Length - 1);
                }

                return line;
            }
            else
            {
                return this.line + Environment.NewLine;
            }
        }

        private void setupDict(List<string> partsVALUES, List<string> firstLinePartsKEYS, char delimiter)
        {
            if (partsVALUES.Count >= firstLinePartsKEYS.Count)
            {
                int i = 0;
                foreach (string key in firstLinePartsKEYS)
                {
                    this.dict[key] = partsVALUES[i];
                    i++;
                }
            }
            else
            {
                this.line = string.Join("", partsVALUES);
            }
        }
    }
}