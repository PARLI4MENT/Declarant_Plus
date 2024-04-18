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
        static int Duty = 200;

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
            //foreach (var par in parcels)
            //{
            //    Console.WriteLine($"{par.Code}\t=>\t{par.Code.Substring(0, 2)}");
            //    par.CostFull.ToString();
            //}

            foreach (var c in parcels)
            {

                if(CheckGroup(c.Code, ref tnveds))
                {

                }
                else
                    Console.WriteLine($"Код посылки{c.Code} [{c.Code.Substring(0, 2)}] => ГРУППА КОД => 1");
            }
        }

        /// <summary> Проверка на совпадение первых знаков с номерами группы </summary>
        /// <returns>
        /// True => Проверяем в исключения => CheckSubGroup().
        /// False => То сразу ставим статус 1 - (Беспошленные)
        /// </returns>
        private static bool CheckGroup( string code, ref List<CodesTNVED> TNVEDs)
        {
            foreach (var tn in TNVEDs)
            {
                if (String.Equals(code.Substring(0, 2), tn.GroupCode.ToString()))
                {
                    if (CheckSubGroup(ref code, tn))
                    {
                        return true;
                    }
                    
                    ///
                }
                return false;
            }
            return false;
        }


        /// <summary> Проверка на исключения кодов из подгруппы </summary>
        /// <returns>
        /// true => Если совпадение найдено => Cтавим статус 1 - (Беспошленные)
        /// false => Если совпаденний не найдено => Проверяем на стоимость CheckCost()
        /// </returns>
        private static bool CheckSubGroup(ref string code, CodesTNVED TNVED)
        {
            foreach ( var subCode in TNVED.Codes)
            {
                if (subCode != null && code == subCode)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary> Проверка на стоимость Пошлинны (>=200 евро || <200 евро) </summary>
        /// <returns>
        /// true => меньше Пошлины (Duty)
        /// false => больше или равен Пошлине (Duty)
        /// </returns>
        private static bool CheckCost(double fullCost, int RubToEuro = 100)
            => ((fullCost / RubToEuro) < Duty) ? true : false;
    }
}
/*
 * 1 - Беспошленные
 * 
 * 2 - Пошленные, больше 200 евро
 * 
 * 3 - Пошленные, меньше 200 евро
 */