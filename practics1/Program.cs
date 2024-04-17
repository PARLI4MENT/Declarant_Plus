using System.Diagnostics;
using ClosedXML.Excel;
using System.Text.RegularExpressions;

using BaseClass;

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

            Console.ReadKey();
        }

    }
}