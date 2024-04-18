using ClosedXML.Excel;
using System.Diagnostics;

namespace BaseClass
{
    public class Parcels
    {
        public uint Index { get; set; }
        /// <summary>  Код => [1] </summary>
        public string? Code { get; set; }

        /// <summary> Цена2 => [5] </summary>
        public double CostFull { get; set; }

        public string? GetOutData() =>
            $"#{this.Index}\t{this.Code}\t{this.CostFull}";

        public uint GetIndexRow() => Index;
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

                    uint i = 2;
                    object obj = new object();

                    lock (obj)
                    {
                        Parallel.ForEach(dataRow, row =>
                        {
                            try
                            {
                                // Пока ничего более лаконичного не прошло в голову: ((
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
                                    Debug.WriteLine($"[SUCCESS]\tINDEX => [{i}]\t" +
                                        $"{row.Cell(1).Value.ToString()}" +
                                        $"\t{row.Cell(5).Value.ToString()}");
#endif
                                }
#if DEBUG
                                else
                                {
                                    Debug.WriteLine($"[ERROR]\tINDEX => [{i}]\t" +
                                        $"{row.Cell(1).Value.ToString()}" +
                                        $"\t{row.Cell(5).Value.ToString()}");
                                }
#endif
                                Interlocked.Increment(ref i);
                            }
                            catch (Exception ex) { Console.WriteLine(ex.Message); }
                        });
                    }

                    //                    foreach(var row in dataRow)
                    //                    {
                    //                        try
                    //                        {
                    //                            //++i;
                    //                            /// Пока ничего более лаконичного не прошло в голову:((
                    //                            if (!string.IsNullOrEmpty(row.Cell(1).Value.ToString()) &&
                    //                                !string.IsNullOrEmpty(row.Cell(5).Value.ToString()))
                    //                            {
                    //                                tmpList.Add(new Parcels()
                    //                                {
                    //                                    Index = i,
                    //                                    Code = row.Cell(1).Value.ToString(),
                    //                                    CostFull = Convert.ToDouble(row.Cell(5).Value.ToString())
                    //                                });
                    //#if DEBUG
                    //                                Debug.WriteLine($"[SUCCESS]\tINDEX => [{i}]\t" +
                    //                                    $"{row.Cell(1).Value.ToString()}" +
                    //                                    $"\t{row.Cell(5).Value.ToString()}");
                    //#endif
                    //                            }
                    //#if DEBUG
                    //                            else
                    //                            {
                    //                                Debug.WriteLine($"[ERROR]\t{row.Cell(1).Value.ToString()}" +
                    //                                    $"\t{row.Cell(5).Value.ToString()}");
                    //                            }
                    //                        } catch (Exception ex) { Console.WriteLine(ex.Message); }
                    //#endif
                    //                        Interlocked.Increment(ref i);
                    //                    };
                }
                Debug.WriteLine("==============================> END DEBUG XLS <==============================");
                return tmpList;
            }
            Debug.WriteLine("==============================> END DEBUG XLS <==============================");
            return null;
        }

    }
}