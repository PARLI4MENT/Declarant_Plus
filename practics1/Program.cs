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
                var dataRow = workbook.Worksheet(1).RowsUsed().Skip(1);

                int i = 0;
                foreach (var row in dataRow)
                {
                    Debug.WriteLine($"{row.Cell(1).Value.ToString()}\t" +
                        $"{row.Cell(3).Value.ToString()}" +
                        $"\t{row.Cell(4).Value.ToString()}\t" +
                        $"{row.Cell(5).Value.ToString()}\t" +
                        $"{row.Cell(6).Value.ToString()}" +
                        $"\t{row.Cell(7).Value.ToString()}\t" +
                        $"{row.Cell(8).Value.ToString()}");

                    parcels.Add(new Parcels()
                    {
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

                Console.ReadLine();
            }

            Console.ReadKey();
        }
    }
}
