using System;
using System.Timers;
using System.Data.SQLite;

namespace ToDo_Tasker_clr
{

    class Tst
    {
        private const string? connString = "Data Source=ToDoBase.db";

        public List<TableTaskCurrent> OutOfDb(SQLiteConnection sqlConn)
        {
            try
            {
                using (sqlConn = new SQLiteConnection(connString))
                {
                    List<TableTaskCurrent> list = new List<TableTaskCurrent>();

                    sqlConn.Open();

                    SQLiteCommand sqlComm = new SQLiteCommand("SELECT * FROM TaskCurrent", sqlConn);

                    using (SQLiteDataReader reader = sqlComm.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TableTaskCurrent task = new TableTaskCurrent();
                                task.ID = reader["ID"].ToString();
                                task.TitleTask = reader["TitleTask"].ToString();
                                task.TextTask = reader["TextTask"].ToString();
                                task.DateCreate = Convert.ToDateTime(reader["DateCreated"].ToString());
                                task.DateEnd = Convert.ToDateTime(reader["DateEnd"].ToString());
                                task.Status = Convert.ToUInt16(reader["Status"]);

                                list.Add(task);
                            }
                        }
                    }
                    return list;

                    // Output to console
                    //Console.WriteLine();
                    //foreach (TableTaskCurrent task in list)
                    //{
                    //    Console.WriteLine($"{task.ID}\t{task.TitleTask}\t {task.TextTask}\t {task.DateCreate}\t {task.DateEnd}\t {task.Status}");
                    //}
                    //sqlComm.ExecuteReader();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return null; }
        }
    }

    //class EventReminder
    //{
    //    private System.Timers.Timer timer;

    //    public EventReminder()
    //    {
    //        timer = new System.Timers.Timer();
    //        timer.Elapsed += Timer_Elapsed;
    //        timer.Interval = TimeSpan.FromMinutes(1).TotalMilliseconds;
    //        timer.AutoReset = true;
    //        timer.Start();
    //    }

    //    private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
    //    {

    //    }
    //}

    public class Reminder
    {
        private DateTime nextDate;
        public Reminder(DateTime _nextDate)
        {
            nextDate = _nextDate;
            Console.WriteLine($"Now:\t{DateTime.Now}\nNext:\t{nextDate.ToString()}");
        }
        public bool CheckDoneTask()
        {
            if (DateTime.Now >= nextDate)
            {
                Console.WriteLine("Таймер сработал");
                return true;
            }
            Console.WriteLine($"Осталось: {nextDate - DateTime.Now}");
            return false;
        }
    }

    internal class Program
    {
        private const string? connString = "Data Source=ToDoBase.db";
        private static System.Threading.Timer ts;
        private static Reminder rem;

        static void Main(string[] args)
        {
            int period = 60000;
            DateTime date = DateTime.Now;
            date = date.AddMinutes(2);



            Console.ReadKey();
        }
    }
}
