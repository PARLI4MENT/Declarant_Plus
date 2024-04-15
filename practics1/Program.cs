﻿using System;
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

            //PARSE TXT FILE
            string[] lines ;
            if (File.Exists(strPathTNVED))
            {
                lines = File.ReadAllLines(strPathTNVED);
                ParseByMask(ref lines);
            }



            Console.ReadKey();
        }

        static void ParseByMask(ref string[] lines)
        {
            List<CodesTNVED> codes = new List<CodesTNVED>();
            
            #region Parse numb group
            Regex group = new Regex(@"^\d{2}", RegexOptions.IgnoreCase & RegexOptions.Compiled);

            int i = 0;
            foreach (var line in lines)
            {
                #region Parse code of group
                if (line.Length > 3)
                {
                    MatchCollection match = group.Matches(line);
                    string[] arrSub = line.Substring(3).Split(',');
                    //Debug.WriteLine("ARR:\t{0}", match[0].Value);
                    foreach (var sub in arrSub)
                    {
                        //Debug.WriteLine("\tSub\t{0}", sub);
                        codes.Add(new CodesTNVED { GroupCode = Convert.ToUInt32(match[0].Value), Codes = arrSub });
                    }
                }
                #endregion
            }
            Console.WriteLine("Count numb of group {0}", i);

            #endregion
        }
    }
}