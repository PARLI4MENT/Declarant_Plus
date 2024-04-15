using System;
using System.Diagnostics;
using System.Text;
using ClosedXML.Excel;
using System.Text.RegularExpressions;

namespace practics1
{

    internal class Program
    {
        static readonly string strPath = "C:\\TaskParcels\\Parcels.xlsx";
        static readonly string strPathTNVED = "C:\\TaskParcels\\TNVED_1538.txt";

        static void Main(string[] args)
        {
            var parcels = new List<Parcels>();
            var TNVED = new List<string>();

            //PARSE XML FILE
            /*
            using (var workbook = new XLWorkbook(strPath))
            {
                //workbook.ShowZeros = false;
                var dataRow = workbook.Worksheet(1).RowsUsed().Skip(1);

                uint i = 2;
                foreach (var row in dataRow)
                {
                    #region Debug output data
                    Debug.WriteLine($"{row.Cell(1).Value.ToString()}\t" +
                        $"{row.Cell(3).Value.ToString()}" +
                        $"\t{row.Cell(4).Value.ToString()}\t" +
                        $"{row.Cell(5).Value.ToString()}\t" +
                        $"{row.Cell(6).Value.ToString()}" +
                        $"\t{row.Cell(7).Value.ToString()}\t" +
                        $"{row.Cell(8).Value.ToString()}");
                    #endregion

                    parcels.Add(new Parcels()
                    {
                        Index = i,
                        Code = row.Cell(1).Value.ToString(),
                        Count = Convert.ToUInt32(row.Cell(3).Value.ToString()),
                        Cost = Convert.ToDouble(row.Cell(4).Value.ToString()),
                        Cost2 = Convert.ToDouble(row.Cell(5).Value.ToString()),
                        Weight = Convert.ToDouble(row.Cell(6).Value.ToString()),
                        Weight2 = Convert.ToDouble(row.Cell(7).Value.ToString()),
                        Track = row.Cell(8).Value.ToString().ToUpper()
                    });
                    i++;
                }

                foreach (var item in parcels)
                {
                    Console.WriteLine(item.GetOutData());
                }

                Console.ReadLine();
            }
            */

            // PARSE TXT FILE
            //var reg = new Regex();

            //using (var fileStream = File.OpenRead(strPathTNVED))
            //{
            //    using (var streamReader = new  StreamReader(fileStream, Encoding.UTF8, true, 128))
            //    {
            //        String line;
            //        while((line = streamReader.ReadLine()) != null)
            //        {
            //            Console.WriteLine(line);
            //        }
            //    }
            //}

            string[] lines ;
            if (File.Exists(strPathTNVED))
            {
                lines = File.ReadAllLines(strPathTNVED);
                int i = 0;
                foreach (var line in lines)
                {
                    //Console.WriteLine($"{++i}\tBASE: {line}");
                }
                ParseByMask(ref lines);
            }



            Console.ReadKey();
        }

        static void ParseByMask(ref string[] lines)
        {
            #region Parse numb group
            string regGroup = @"^\d{2}";
            Regex group = new Regex(regGroup, RegexOptions.IgnoreCase & RegexOptions.Compiled);

            int i = 0;
            foreach (var line in lines)
            {
                MatchCollection match = group.Matches(line);
                if (match.Count == 1)
                {
                    i++;
                    string strMatch = match[0].Value;
                    //var arrs = Regex.Matches(line, regGroup)
                    //    .Cast<Match>()
                    //    .Select(m => m.Value)
                    //    .ToArray();
                    Console.WriteLine("ARR:\t{0}",strMatch);
                }
            }
            Console.WriteLine("Count numb of group {0}", i);
            #endregion

            #region Parse code of group

            #endregion
        }
    }
}
