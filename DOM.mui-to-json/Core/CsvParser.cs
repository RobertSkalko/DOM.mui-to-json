using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM.mui_to_json
{
    internal class CsvParser
    {
        public static List<List<string>> parseCSV(Stream stream)
        {
            var lists = ParseCsv(stream);

            List<List<string>> stringlistsLists = new List<List<string>>();

            foreach (List<byte[]> bytelists in lists)
            {
                List<string> stringlists = new List<string>();

                foreach (byte[] bytes in bytelists)
                {
                    stringlists.Add(MainClass.ENCODING.GetString(bytes));
                }

                stringlistsLists.Add(stringlists);
            }

            return stringlistsLists;
        }

        private static IEnumerable<List<byte[]>> ParseCsv(Stream stream)
        {
            var result = new List<List<byte[]>>();
            var row = new List<byte[]>();
            var inQuotes = false;
            var currentToken = new List<byte>();
            var shouldSkipByte = false;
            byte byteValue = 0;

            byte quote = 0;

            while (true)
            {
                if (!shouldSkipByte)
                {
                    var intByteValue = stream.ReadByte();
                    if (intByteValue == -1)
                        break;
                    byteValue = (byte)intByteValue;
                }
                shouldSkipByte = false;

                if (inQuotes)
                {
                    if (byteValue == '"')
                    {
                        var intByteValue = stream.ReadByte();
                        if (intByteValue == -1)
                        {
                            break;
                        }
                        currentToken.Add(quote);

                        byteValue = (byte)intByteValue;
                        if (byteValue != '"')
                        {
                            inQuotes = false;
                            shouldSkipByte = true;
                        }
                        else
                        {
                            currentToken.Add(byteValue);
                        }
                    }
                    else
                    {
                        currentToken.Add(byteValue);
                    }
                }
                else
                {
                    switch ((char)byteValue)
                    {
                        case '"':
                            inQuotes = true;
                            quote = byteValue;
                            currentToken.Add(quote);
                            break;

                        case ';':
                            row.Add(currentToken.ToArray());
                            currentToken = new List<byte>();
                            break;

                        case '\n':
                            row.Add(currentToken.ToArray());
                            currentToken = new List<byte>();
                            result.Add(row);
                            row = new List<byte[]>();
                            break;

                        case '\r':
                            row.Add(currentToken.ToArray());
                            currentToken = new List<byte>();
                            result.Add(row);
                            row = new List<byte[]>();
                            break;

                        default:
                            currentToken.Add(byteValue);
                            break;
                    }
                }
            }

            if (inQuotes || currentToken.Count != 0)
                Console.WriteLine("Bad CSV, Problem letter: " + System.Text.Encoding.UTF8.GetString(new Byte[] { byteValue }));
            // todo this was exception throwing before on rune_words.mui, I had no idea why so I made it not crash and it worked..?
            
            row.Add(currentToken.ToArray());
            result.Add(row);

            stream.Close();

            return result;
        }
    }
}