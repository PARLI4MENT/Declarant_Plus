namespace ToDo_Task.Classes
{
    public class TableTaskCurrent
    {
        //public static string table_name { get; } = "TaskCurrent";

        public string ID { get; set; }
        public string TitleTask { get; set; }
        public string TextTask { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateEnd { get; set; }
        public uint Status { get; set; }
    }
}
