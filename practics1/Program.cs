using System;
using System.Diagnostics;
using ClosedXML.Excel;


namespace practics1
{

    internal class Program
    {
        static readonly string strPath = "C:\\TaskParcels\\Parcels.xlsx";

        static void Main(string[] args)
        {
            var parcels = new List<Parcels>();

            using (var workbook = new XLWorkbook(strPath))
            {

                //workbook.ShowZeros = false;
                var dataRow = workbook.Worksheet(1).SetShowRowColHeaders(false).RowsUsed();

                int i = 0;
                foreach (var row in dataRow)
                {
                    Console.WriteLine($"{row.Cell(1).Value.ToString()}\t{row.Cell(3).Value.ToString()}" +
                        $"\t{row.Cell(4).Value.ToString()}\t{row.Cell(5).Value.ToString()}\t{row.Cell(6).Value.ToString()}" +
                        $"\t{row.Cell(7).Value.ToString()} \t{row.Cell(8).Value.ToString()}");
                    //parcels.Add(new Parcels()
                    //{
                    //    Code = row.Cell(1).Value.ToString(),
                    //    Count = Convert.ToUInt16(row.Cell(3).Value),
                    //    Cost = Convert.ToDouble(row.Cell(4).Value),
                    //    Weight = Convert.ToDouble(row.Cell(5).Value),
                    //    Weight2 = Convert.ToDouble(row.Cell(6).Value),
                    //    Track = row.Cell(7).Value.ToString().ToUpper()
                    //});
                    //Console.WriteLine($"{i} => {parcels[i].Code}\t{parcels[i].Count}\t" +
                    //    $"{parcels[i].Cost}\t{parcels[i].Weight}\t{parcels[i].Weight2}\t" +
                    //    $"{parcels[i].Track}\n");
                    i++;
                }
            }

            Console.ReadKey();
        }
    }
}
