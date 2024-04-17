using System;
using System.Diagnostics;
using System.Text;
using ClosedXML.Excel;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using System.Runtime.CompilerServices;
using DocumentFormat.OpenXml.Math;

namespace practics1
{
    internal class Program
    {
        static readonly string strPath = "C:\\TaskParcels\\Parcels.xlsx";
        static readonly string strPathTNVED = "C:\\TaskParcels\\TNVED_1538.txt";

        static void Main(string[] args)
        {
            List<Parcels> Parcels;
            List<CodesTNVED> TNVED;

            //PARSE XLS
            ParseXLS(strPath, Parcels = new List<Parcels>());
            Console.ReadKey();

            //PARSE TXT FILE
            ParseTNVED(strPathTNVED, TNVED = new List<CodesTNVED>());

            Console.ReadKey();
        }

        static void ParseXLS(string path, List<Parcels> parcels)
        {
            
            using (var workbook = new XLWorkbook(path))
            {
                workbook.ShowZeros = false;
                var dataRow = workbook.Worksheet(1).RowsUsed().Skip(1);

                uint i = 2;
                foreach (var row in dataRow)
                {
                    //Перебор всех ячеек
                    //foreach (var cell in row.Cells())
                    //{
                    //    for (int j = 1; j < 8; j++)
                    //        Console.WriteLine();
                    //}

                    /// Пока ничего более лаконичного не прошло в голову:((
                    if (!string.IsNullOrEmpty(row.Cell(1).Value.ToString()) &&
                        !string.IsNullOrEmpty(row.Cell(3).Value.ToString()) &&
                        !string.IsNullOrEmpty(row.Cell(4).Value.ToString()) &&
                        !string.IsNullOrEmpty(row.Cell(5).Value.ToString()) &&
                        !string.IsNullOrEmpty(row.Cell(6).Value.ToString()) &&
                        !string.IsNullOrEmpty(row.Cell(7).Value.ToString()) &&
                        !string.IsNullOrEmpty(row.Cell(8).Value.ToString()))
                    {
                        parcels.Add(new Parcels()
                        {
                            Index = i,
                            Code = row.Cell(1).Value.ToString(),
                            Count = Convert.ToUInt32(row.Cell(3).Value.ToString()),
                            Cost = Convert.ToDouble(row.Cell(4).Value.ToString()),
                            CostFull = Convert.ToDouble(row.Cell(5).Value.ToString()),
                            WeightFull = Convert.ToDouble(row.Cell(6).Value.ToString()),
                            Weight = Convert.ToDouble(row.Cell(7).Value.ToString()),
                            Track = row.Cell(8).Value.ToString().ToUpper()
                        });
#if DEBUG
                        Debug.WriteLine($"[SUCCESS]\t{row.Cell(1).Value.ToString()}" +
                        $"\t{row.Cell(3).Value.ToString()}" +
                        $"\t{row.Cell(4).Value.ToString()}" +
                        $"\t{row.Cell(5).Value.ToString()}" +
                        $"\t{row.Cell(6).Value.ToString()}" +
                        $"\t{row.Cell(7).Value.ToString()}" +
                        $"\t{row.Cell(8).Value.ToString()}");
#endif
                    }
#if DEBUG
                    else
                    {
                        Debug.WriteLine($"[ERROR]\t{row.Cell(1).Value.ToString()}" +
                            $"\t{row.Cell(3).Value.ToString()}" +
                            $"\t{row.Cell(4).Value.ToString()}" +
                            $"\t{row.Cell(5).Value.ToString()}" +
                            $"\t{row.Cell(6).Value.ToString()}" +
                            $"\t{row.Cell(7).Value.ToString()}" +
                            $"\t{row.Cell(8).Value.ToString()}");
                    }
#endif
                    i++;
                }
            }
        }

        static void ParseTNVED(string pathTNVED, List<CodesTNVED> codes)
        {
            if (File.Exists(strPathTNVED))
            {
                string[] lines = File.ReadAllLines(pathTNVED);

                #region Parse numb group
                int i = 0;
                foreach (var line in lines)
                {
                    #region Parse code of group
                    if (line.Length > 3)
                    {
                        i++;
                        MatchCollection match = new Regex(@"^\d{2}", RegexOptions.IgnoreCase & RegexOptions.Compiled).Matches(line);
                        string[] arrSub = line.Substring(3).Split(',');
                        ///
                        //var tmp = Convert.ToUInt32(match[0].Value.ToString());
                        Console.WriteLine(match[0].Value.ToString());
                        codes.Add(new CodesTNVED { GroupCode = Convert.ToUInt32(match[0].Value), Codes = arrSub });
#if DEBUG
                        Debug.WriteLine("ARR:\t{0}", match[0].Value);
                        foreach (var sub in arrSub)
                        {
                            Debug.WriteLine("\tSub\t{0}", sub);
                        }
#endif
                    }
                    #endregion
                }
#if DEBUG
                Debug.WriteLine("Count numb of group {0}", i);
#endif
                #endregion
            }
        }
    }
}