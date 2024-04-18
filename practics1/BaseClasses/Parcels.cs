using ClosedXML.Excel;
using System.Diagnostics;

namespace BaseClass
{
    public class Parcels
    {
        public int Index { get; set; }
        /// <summary>  Код => [1] </summary>
        public string? Code { get; set; }

        /// <summary> Цена2 => [5] </summary>
        public double CostFull { get; set; }

        public string? GetOutData() =>
            $"#{this.Index}\t{this.Code}\t{this.CostFull}";

        public int GetIndexRow() => Index;
    }

    public class XlsFile
    {
        public XlsFile() { }

        public List<Parcels> ParseXLS(string path)
        {
            if (File.Exists(path))
            {
                Debug.WriteLine("\n==============================> START DEBUG XLS <==============================");

                var tmpList = new List<Parcels>();
                using (var workbook = new XLWorkbook(path))
                {
                    workbook.ShowZeros = false;
                    var dataRow = workbook.Worksheet(1).RowsUsed().Skip(1);

                    int i = 2;

                    foreach (var row in dataRow)
                    {
                        try
                        {
                            //++i;
                            /// Пока ничего более лаконичного не прошло в голову:((
                            if (!string.IsNullOrEmpty(row.Cell(1).Value.ToString()) &&
                                !string.IsNullOrEmpty(row.Cell(5).Value.ToString()))
                            {
                                tmpList.Add(new Parcels()
                                {
                                    Index = i,
                                    Code = row.Cell(1).Value.ToString(),
                                    CostFull = Convert.ToDouble(row.Cell(5).Value.ToString())
                                });
#if DEBUG
                                                    Debug.WriteLine($"[SUCCESS]\tINDEX [{i}]\t" +
                                                        $"Code [{row.Cell(1).Value.ToString()}]\t" +
                                                        $"Cost [{row.Cell(5).Value.ToString()}]");
#endif
                            }
#if DEBUG
                            else
                            {
                                Debug.WriteLine($"[ERROR]\tINDEX [{i}]\t" +
                                    $"Code [{row.Cell(1).Value.ToString()}]\t" +
                                    $"Cost [{row.Cell(5).Value.ToString()}]");
                            }
#endif
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        Interlocked.Increment(ref i);
                        };
                    }
                Debug.WriteLine("==============================> END DEBUG XLS <==============================");
                return tmpList;
            }
            Debug.WriteLine("==============================> END DEBUG XLS <==============================");
            return null;
        }

    }
}