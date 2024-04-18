using ClosedXML.Excel;

using BaseClass;
using DocumentFormat.OpenXml.Office.CustomUI;
using System.Diagnostics;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Math;

namespace std
{
    internal class Program
    {
        static readonly string strPathXLS = "C:\\TaskParcels\\1.xlsx";
        static readonly string strPathTNVED = "C:\\TaskParcels\\TNVED_1538.txt";
        static int EuroToRub = 100;
        static double Duty = 200;

        static void Main(string[] args)
        {
            List<Parcels> Parcels = new XlsFile().ParseXLS(strPathXLS);
            List<CodesTNVED> TNVED = new TxtFile().ParseTxt(strPathTNVED);

            // Set 1row & 9collumn Value => "Группа"
            var workbook = new XLWorkbook(strPathXLS);
            int collIndex = 9;
            while (!String.IsNullOrEmpty(workbook.Worksheet(1).Cell(1, collIndex).Value.ToString()))
            {
                Interlocked.Increment(ref collIndex);
#if (DEBUG)
                Debug.WriteLine($"IS-NULL-EMPTY => {collIndex}");
#endif
            }
            workbook.Worksheet(1).Cell(1, collIndex).Value = "Группа";
            workbook.Save();

            Check(ref Parcels, ref TNVED);

            Console.ReadKey();
        }

        private static void Check(ref List<Parcels> parcels, ref List<CodesTNVED> tnveds)
        {
            foreach (var parcel in parcels)
            {
                CheckGroup(parcel, ref tnveds);
            }
        }

        /// <summary> Проверка на совпадение первых знаков с номерами группы </summary>
        /// <returns>
        /// True => Проверяем в исключения => CheckSubGroup().
        /// False => То сразу ставим статус 1 - (Беспошленные)
        /// </returns>
        private static uint CheckGroup(Parcels parcel, ref List<CodesTNVED> TNVEDs)
        {
            Console.WriteLine(parcel.GetOutData());

            foreach (var tn in TNVEDs)
            {
                if (String.Equals(parcel.Code.Substring(0, 2), tn.GroupCode.ToString()))
                {
                    if (CheckSubGroup(ref parcel, tn))
                    {
                        Console.WriteLine($"PARCEL_Sub [{parcel.Code.Substring(0, 2)}] [{parcel.Code}] => tnvedCode [{tn.GroupCode}]\n");
                        return 1;
                    }
                    var i = CheckCost(parcel.CostFull);
                    return i;
                }
                Console.WriteLine($"PARCEL_Sub [{parcel.Code.Substring(0, 2)}] [{parcel.Code}] => tnvedCode [{tn.GroupCode}]\n");
                return 1;
            }
            Console.WriteLine($"PARCEL_Sub [{parcel.Code.Substring(0, 2)}] [{parcel.Code}]\n");
            return 1;
        }


        /// <summary> Проверка на исключения кодов из подгруппы </summary>
        /// <returns>
        /// true => Если совпадение найдено => Cтавим статус 1 - (Беспошленные)
        /// false => Если совпаденний не найдено => Проверяем на стоимость CheckCost()
        /// </returns>
        private static bool CheckSubGroup(ref Parcels parcel, CodesTNVED TNVED)
        {
            foreach (var subCode in TNVED.Codes)
            {
                if (subCode != null && parcel.Code.Substring(0, subCode.Length) == subCode)
                {
                    Console.WriteLine($"PARCEL_Sub [{parcel.Code.Substring(0, subCode.Length)}] [{parcel.Code}] => tnvedSubCode [{subCode}]\n");
                    return true;
                }
            }
            Console.WriteLine($"PARCEL_Sub [{parcel.Code}] [{parcel.Code}]\n");
            return false;
        }

        /// <summary> Проверка на стоимость Пошлинны (>=200 евро || <200 евро) </summary>
        /// <returns>
        /// true => меньше Пошлины (Duty)
        /// false => больше или равен Пошлине (Duty)
        /// </returns>
        private static uint CheckCost(double fullCost, double RubToEuro = 100)
        {
            if ((fullCost / RubToEuro) < Duty)
            {
                Console.WriteLine($"\t\t{(fullCost / RubToEuro)}");
                return 3;
            }
            
            Console.WriteLine($"\t\t{(fullCost / RubToEuro)}");
            return 2;
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