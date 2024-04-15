
namespace practics1
{
    public class Parcels
    {
        public uint Index { get; set; }
        /// <summary>  Код => [1] </summary>
        public string? Code {  get; set; }

        /// <summary> Кол-во => [3] </summary>
        public uint Count { get; set; }

        /// <summary> Цена => [4] </summary>
        public double Cost { get; set; } 
        
        /// <summary> Цена2 => [5] </summary>
        public double Cost2 { get; set; }

        /// <summary> Вес => [6] </summary>
        public double WeightFull { get; set; }

        /// <summary> Вес2 => [7] </summary>
        public double Weight { get; set; }

        /// <summary> Трек => [8] </summary>
        public string? Track { get; set; }

        public string? GetOutData() =>
            $"#{this.Index}\t{this.Code}\t{this.Count}\t{this.Cost}\t{this.Cost2}\t{this.WeightFull}\t{this.Weight}\t{this.Track}\n";

    }
}
