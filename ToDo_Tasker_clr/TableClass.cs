using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_Tasker_clr
{
    public class TableTaskCurrent
    {
        public static string table_name { get; } = "TaskCurrent";

        public string ID { get; set; }
        public string TitleTask { get; set; }
        public string TextTask { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateEnd { get; set; }
        public uint Status { get; set; }
    }
}
