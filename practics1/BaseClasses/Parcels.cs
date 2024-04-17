
using ClosedXML.Excel;
using System.Diagnostics;

namespace BaseClass
{
    public class Parcels
    {
        public uint Index { get; set; }
        /// <summary>  Код => [1] </summary>
        public string? Code { get; set; }

        /// <summary> Кол-во => [3] </summary>
        public uint Count { get; set; }

        /// <summary> Цена => [4] </summary>
        public double Cost { get; set; }

        /// <summary> Цена2 => [5] </summary>
        public double CostFull { get; set; }

        /// <summary> Вес => [6] </summary>
        public double WeightFull { get; set; }

        /// <summary> Вес2 => [7] </summary>
        public double Weight { get; set; }

        /// <summary> Трек => [8] </summary>
        public string? Track { get; set; }

        public string? GetOutData() =>
            $"#{this.Index}\t{this.Code}\t{this.Count}\t{this.Cost}\t{this.CostFull}\t{this.WeightFull}\t{this.Weight}\t{this.Track}\n";
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
                    Parallel.ForEach(dataRow, row =>
                    {
                        try
                        {
                            //++i;
                            /// Пока ничего более лаконичного не прошло в голову:((
                            if (!string.IsNullOrEmpty(row.Cell(1).Value.ToString()) &&
                                !string.IsNullOrEmpty(row.Cell(3).Value.ToString()) &&
                                !string.IsNullOrEmpty(row.Cell(4).Value.ToString()) &&
                                !string.IsNullOrEmpty(row.Cell(5).Value.ToString()) &&
                                !string.IsNullOrEmpty(row.Cell(6).Value.ToString()) &&
                                !string.IsNullOrEmpty(row.Cell(7).Value.ToString()) &&
                                !string.IsNullOrEmpty(row.Cell(8).Value.ToString()))
                            {
                                tmpList.Add(new Parcels()
                                {
                                    Index = i,
                                    Code = row.Cell(1).Value.ToString(),
                                    Count = Convert.ToUInt32(row.Cell(3).Value.ToString()),
                                    Cost = Convert.ToDouble(row.Cell(4).Value.ToString()),
                                    CostFull = Convert.ToDouble(row.Cell(5).Value.ToString()),
                                    WeightFull = Convert.ToDouble(row.Cell(6).Value.ToString()),
                                    Weight = Convert.ToDouble(row.Cell(7).Value.ToString()),
                                    Track = row.Cell(8).Value.ToString().ToUpper()
                                });
#if DEBUG
                                Debug.WriteLine($"[SUCCESS]\t{row.Cell(1).Value.ToString()}" +
                                $"\t{row.Cell(3).Value.ToString()}" +
                                $"\t{row.Cell(4).Value.ToString()}" +
                                $"\t{row.Cell(5).Value.ToString()}" +
                                $"\t{row.Cell(6).Value.ToString()}" +
                                $"\t{row.Cell(7).Value.ToString()}" +
                                $"\t{row.Cell(8).Value.ToString()}");
#endif
                            }
#if DEBUG
                            else
                            {
                                Debug.WriteLine($"[ERROR]\t{row.Cell(1).Value.ToString()}" +
                                    $"\t{row.Cell(3).Value.ToString()}" +
                                    $"\t{row.Cell(4).Value.ToString()}" +
                                    $"\t{row.Cell(5).Value.ToString()}" +
                                    $"\t{row.Cell(6).Value.ToString()}" +
                                    $"\t{row.Cell(7).Value.ToString()}" +
                                    $"\t{row.Cell(8).Value.ToString()}");
                            }
                        } catch (Exception ex) { Console.WriteLine(ex.Message); }
#endif
                        Interlocked.Increment(ref i);
                    });
                }
                Debug.WriteLine("==============================> END DEBUG XLS <==============================");
                return tmpList;
            }
            Debug.WriteLine("==============================> END DEBUG XLS <==============================");
            return null;
        }

    }
}