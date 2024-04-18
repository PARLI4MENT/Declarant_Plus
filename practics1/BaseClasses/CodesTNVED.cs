using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BaseClass
{
    public class CodesTNVED
    {
        public uint GroupCode { get; set; }
        public string[] Codes { get; set; }
    }

    public class TxtFile
    {
        public TxtFile() { }

        public List<CodesTNVED> ParseTxt(string path)
        {
            if (File.Exists(path))
            {
                Debug.WriteLine("\n==============================> START DEBUG TXT <==============================");
                string[] lines = File.ReadAllLines(path);
                List<CodesTNVED> codes = new List<CodesTNVED>();

                #region Parse numb group
                int i = 0;
                Parallel.ForEach(lines, line => {
                    #region Parse code of group
                    if (line.Length > 3)
                    {
                        i++;
                        MatchCollection match = new Regex(@"^\d{2}", RegexOptions.IgnoreCase & RegexOptions.Compiled).Matches(line);
                        string[] arrSub = line.Substring(3).Split(',');
                        codes.Add(new CodesTNVED { GroupCode = Convert.ToUInt32(match[0].Value), Codes = arrSub });
#if DEBUG
                        Debug.WriteLine($"\nGroupCode:\t{match[0].Value}");
                        Debug.WriteLine("{");
                        foreach (var sub in arrSub)
                        {
                            Debug.WriteLine($"\tSubСode:\t {sub}");
                        }
                        Debug.WriteLine("}");
#endif
                    }
                    #endregion
                });

                #region Заменен на параллельный перебор
                //                foreach (var line in lines)
                //                {
                //                    #region Parse code of group
                //                    if (line.Length > 3)
                //                    {
                //                        i++;
                //                        MatchCollection match = new Regex(@"^\d{2}", RegexOptions.IgnoreCase & RegexOptions.Compiled).Matches(line);
                //                        string[] arrSub = line.Substring(3).Split(',');
                //                        codes.Add(new CodesTNVED { GroupCode = Convert.ToUInt32(match[0].Value), Codes = arrSub });
                //#if DEBUG
                //                        Debug.WriteLine($"\nGroupCode:\t{match[0].Value}");
                //                        Debug.WriteLine("{");
                //                        foreach (var sub in arrSub)
                //                        {
                //                            Debug.WriteLine($"\tSubСode:\t {sub}");
                //                        }
                //                        Debug.WriteLine("}");
                //#endif
                //                    }
                //                    #endregion
                //                }
                #endregion
                Debug.WriteLine("==============================> END DEBUG TXT <==============================");
                return codes;
                #endregion
            }
            Debug.WriteLine("==============================> END DEBUG TXT <==============================");
            return null;
        }

    }
}
