﻿using System.Data;

namespace ToDo_Tasker_clr
{
    //class Tst
    //{
    //    private const string? connString = "Data Source=ToDoBase.db";

    //    public List<TableTaskCurrent> OutOfDb(SQLiteConnection sqlConn)
    //    {
    //        try
    //        {
    //            using (sqlConn = new SQLiteConnection(connString))
    //            {
    //                List<TableTaskCurrent> list = new List<TableTaskCurrent>();

    //                sqlConn.Open();

    //                SQLiteCommand sqlComm = new SQLiteCommand("SELECT * FROM TaskCurrent", sqlConn);

    //                using (SQLiteDataReader reader = sqlComm.ExecuteReader())
    //                {
    //                    if (reader.HasRows)
    //                    {
    //                        while (reader.Read())
    //                        {
    //                            TableTaskCurrent task = new TableTaskCurrent();
    //                            task.ID = reader["ID"].ToString();
    //                            task.TitleTask = reader["TitleTask"].ToString();
    //                            task.TextTask = reader["TextTask"].ToString();
    //                            task.DateCreate = Convert.ToDateTime(reader["DateCreated"].ToString());
    //                            task.DateEnd = Convert.ToDateTime(reader["DateEnd"].ToString());
    //                            task.Status = Convert.ToUInt16(reader["Status"]);

    //                            list.Add(task);
    //                        }
    //                    }
    //                }
    //                return list;

    //                // Output to console
    //                //Console.WriteLine();
    //                //foreach (TableTaskCurrent task in list)
    //                //{
    //                //    Console.WriteLine($"{task.ID}\t{task.TitleTask}\t {task.TextTask}\t {task.DateCreate}\t {task.DateEnd}\t {task.Status}");
    //                //}
    //                //sqlComm.ExecuteReader();
    //            }
    //        }
    //        catch (Exception ex) { Console.WriteLine(ex.Message); return null; }
    //    }
    //}
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

    //public class Reminder
    //{
    //    private DateTime nextDate;
    //    public Reminder(DateTime _nextDate)
    //    {
    //        nextDate = _nextDate;
    //        Console.WriteLine($"Now:\t{DateTime.Now}\nNext:\t{nextDate.ToString()}");
    //    }
    //    public bool CheckDoneTask()
    //    {
    //        if (DateTime.Now >= nextDate)
    //        {
    //            Console.WriteLine("Таймер сработал");
    //            return true;
    //        }
    //        Console.WriteLine($"Осталось: {nextDate - DateTime.Now}");
    //        return false;
    //    }
    //}

    public class TaskClass
    {
        public string NameTask { get; set; }
        public DateTime EndDate { get; set; }

        public TaskClass(string NameTask, DateTime EndDate)
        {
            this.NameTask = NameTask;
            this.EndDate = EndDate;
        }
    }
    public class CreateReminder
    {
        public string NameTask { get; private set; }
        public DateTime EndDate { get; private set; }
        private System.Timers.Timer Timer;

        //private List<TaskClass> Cls;

        //public CreateTask(List<TaskClass> cls)
        //{
        //    var srtList = cls.OrderBy(x => x.EndDate).ToList();

        //    foreach (var item in cls)
        //    {

        //    }
        //}

        public CreateReminder()
        {
            Timer = new System.Timers.Timer();
            Timer.Interval = 1000;

            Timer.Elapsed += OnTimedEvent;
            Timer.AutoReset = true;
            Timer.Enabled = true;
        }
        public CreateReminder(string NameTask, DateTime EndDate)
        {
            this.NameTask= NameTask;
            this.EndDate = EndDate;

            Timer = new System.Timers.Timer();
            Timer.Interval = 1000;

            Timer.Elapsed += OnTimedEvent;
            Timer.AutoReset = true;
            Timer.Enabled = true;
        }

        private async void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (CheckDoneTask())
            {
                this.Timer.Dispose();
                Console.WriteLine($"Задача {this.NameTask}\tОкончание {DateTime.Now}");
                Console.WriteLine();
            }
        }

        public bool CheckDoneTask()
        {
            if (DateTime.Now >= this.EndDate)
            {
                Console.WriteLine($"Задача {this.NameTask}\t Таймер сработал");
                Console.WriteLine($"Текущее время {DateTime.Now:G}");
                //Console.WriteLine();
                return true;
            }
            //Console.WriteLine($"Задача {NameTask}\tОсталось: {EndDate - DateTime.Now}");
            //Console.WriteLine();
            return false;
        }
    }

    internal class Program
    {
        private const string? connString = "Data Source=ToDoBase.db;Version=3;";

        static void Main(string[] args)
        {
            string substruct = "0:05:00";
            var periodTo = new DateTime(2017, 10, 5, 12, 30, 0);
            var periodFrom = new DateTime(2017, 10, 5, 12, 25, 0);

            Console.WriteLine();

            if (periodFrom.Date == periodTo.Date)
            {
                TimeSpan interval = periodTo - periodFrom;
                if (interval.Hours <= 0 && interval.Minutes <= 5)
                {
                    Console.WriteLine("Готово");
                }
                else Console.WriteLine("Разница больше");
            }

            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
