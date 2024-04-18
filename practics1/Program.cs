using BaseClass;
using ClosedXML.Excel;
using System.Diagnostics;

namespace std
{
    internal class Program
    {
        static readonly string strPathXLS = $"{AppDomain.CurrentDomain.BaseDirectory}" + "test\\Parcels.xlsx";
        static readonly string strPathTNVED = $"{AppDomain.CurrentDomain.BaseDirectory}" + "test\\TNVED_1538.txt";
        static double Duty = 200;
        static int collIndex = 1;

        static void Main(string[] args)
        {
            if(!File.Exists(strPathXLS) || !File.Exists(strPathTNVED))
            {
                Console.ReadKey();
                return;
            }

            List<Parcels> Parcels = new XlsFile().ParseXLS(strPathXLS);
            List<CodesTNVED> TNVED = new TxtFile().ParseTxt(strPathTNVED);

            // Устанавливаем в ближайшую пустую ячейку первой строки значение => "Группа"
            var workbook = new XLWorkbook(strPathXLS);
            while (!String.IsNullOrEmpty(workbook.Worksheet(1).Cell(1, collIndex).Value.ToString()))
            {
#if (DEBUG)
                Debug.WriteLine($"IS-NULL-EMPTY => {collIndex} => {workbook.Worksheet(1).Cell(1, collIndex).Value.ToString()}");
#endif
                Interlocked.Increment(ref collIndex);
            }
            workbook.Worksheet(1).Cell(1, collIndex).Value = "Группа";
            workbook.Save();

            Check(ref Parcels, ref TNVED);

            Console.WriteLine("\nALL TASKS DONE!!!\n");
            Console.ReadKey();
        }

        public static void Check(ref List<Parcels> parcels, ref List<CodesTNVED> tnveds)
        {
            var workbook = new XLWorkbook(strPathXLS);
            foreach (var parcel in parcels)
            {
                workbook.Worksheet(1).Cell(parcel.Index, collIndex).Value = CheckGroup(parcel, ref tnveds);
            }
            workbook.Save();
        }

        /// <summary> Проверка на совпадение первых знаков с номерами группы </summary>
        /// <returns>
        /// True => Проверяем в исключения => CheckSubGroup().
        /// False => То сразу ставим статус 1 - (Беспошленные)
        /// </returns>
        private static int CheckGroup(Parcels parcel, ref List<CodesTNVED> TNVEDs)
        {
            foreach (var tn in TNVEDs)
            {
                if (String.Equals(parcel.Code.Substring(0, 2), tn.GroupCode.ToString()))
                {
                    if (tn.Codes != null)
                    {
                        foreach (var subCode in tn.Codes)
                        {
                            if (parcel.Code.Substring(0, subCode.Length) == subCode)
                            {
#if (DEBUG)
                                Console.WriteLine($"PARCEL_Sub [{parcel.Code.Substring(0, subCode.Length)}] [{parcel.Code}] => tnvedSubCode [{subCode}]\n");
#endif
                                return 1;
                            }
                        }
                        return CheckCost(parcel.CostFull);
                    }
                    return CheckCost(parcel.CostFull);
                }
            }
            return 1;
        }

        /// <summary> Проверка на стоимость Пошлинны (>=200 евро || <200 евро) </summary>
        /// <returns>
        /// true => меньше Пошлины (Duty)
        /// false => больше или равен Пошлине (Duty)
        /// </returns>
        private static int CheckCost(double fullCost, double RubToEuro = 100)
            => (fullCost/RubToEuro) < Duty ? 3 : 2;
//        {
//            if ((fullCost / RubToEuro) < Duty)
//            {
//#if (DEBUG)
//                Console.WriteLine($"\t\t{(fullCost / RubToEuro)}");
//#endif
//                return 3;
//            }
//#if (DEBUG)            
//            Console.WriteLine($"\t\t{(fullCost / RubToEuro)}");
//#endif
//            return 2;
//        }
    }
}