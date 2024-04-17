using ClosedXML.Excel;

using BaseClass;
using DocumentFormat.OpenXml.Office.CustomUI;

namespace std
{
    internal class Program
    {
        static readonly string strPathXLS = "C:\\TaskParcels\\Parcels.xlsx";
        static readonly string strPathTNVED = "C:\\TaskParcels\\TNVED_1538.txt";

        static void Main(string[] args)
        {
            List<Parcels> Parcels = new XlsFile().ParseXLS(strPathXLS);
            List<CodesTNVED> TNVED = new TxtFile().ParseTxt(strPathTNVED);

            // Set 1row & 9collumn Value => "Группа"
            //var workbook = new XLWorkbook(strPathXLS);
            //workbook.Worksheet(1).Cell(1, 9).Value = "Группа";
            //workbook.Save();

            Console.ReadKey();
        }

        private void CheckCost()
        {

        }
    }
}
/*
 * 1 - Беспошленные
 * 
 * 2 - Пошленные, больше 200 евро
 * 
 * 3 - Пошленные, меньше 200 евро
 */