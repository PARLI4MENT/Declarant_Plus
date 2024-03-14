using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_Tasker_clr
{
    public class TableTaskCurrent
    {
        string ID { get; }
        string Title { get; set; }
        string Text { get; set; }
        DateTime DateCreate { get; set; }
        DateTime DateEnd { get; set; }
    }
    public class TableTaskEnd
    {
        string ID { get; }
        string Title { get; set; }
        string Text { get; set; }
        DateTime DateCreate { get; set; }
        DateTime DateEnd { get; set; }
    }
}
