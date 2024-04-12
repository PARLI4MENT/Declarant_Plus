
namespace practics1
{
    public class Parcels
    {
        /// <summary>  Код => [0] </summary>
        public string? Code {  get; set; }

        /// <summary> Кол-во => [2] </summary>
        public uint Count { get; set; }

        /// <summary> Цена => [3] </summary>
        public double Cost { get; set; }

        /// <summary> Стоимость => [4] </summary>
        public double Weight { get; set; }

        /// <summary> Вес => [5] </summary>
        public double Weight2 { get; set; }

        /// <summary> Трек => [6] </summary>
        public string? Track { get; set; }

        public string GetOutData() => $"{this.Code}\t{this.Count}\t{this.Cost}\t{this.Weight}\t{this.Weight2}\t{this.Track}\n";

    }
}
