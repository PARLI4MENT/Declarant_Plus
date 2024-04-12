using System;
using ClosedXML.Excel;


namespace practics1
{

    internal class Program
    {
        static readonly string strPath = "C:\\TaskParcels\\Parcels.xlsx";

        static void Main(string[] args)
        {
            //var parcels = new List<Parcels>();



            //using (var workbook = new XLWorkbook(strPath))
            //{
            //    var dataRow = workbook.Worksheet(1).RowsUsed();
            //    int i = 0;
            //    foreach (var row in dataRow)
            //    {

            //        parcels.Add(new Parcels()
            //        {
            //            Code = row.Cell(0).Value.ToString(),

            //        });
            //        i++;
            //    }
            //}

            Console.ReadKey();
        }
    }
}
