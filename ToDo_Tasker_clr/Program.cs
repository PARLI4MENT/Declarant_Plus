using System;
using System.Data.SQLite;

namespace ToDo_Tasker_clr
{
    //interface ITaskStruct
    //{
    //    Guid ID { get; set; }
    //    string Title { get; set; }
    //    string Text { get; set; }
    //    DateTime DateCreated { get; set; }
    //    DateTime DateEnd { get; set; }
    //}

    //private class TaskStruct : ITaskStruct
    //{
    //    public override bool Equals(object? obj)
    //    {
    //        return base.Equals(obj);
    //    }

    //    public override int GetHashCode()
    //    {
    //        return base.GetHashCode();
    //    }

    //    public override string? ToString()
    //    {
    //        return base.ToString();
    //    }
    //}

    internal class Program
    {
        private const string? connString = "Data Source=ToDoBase.db";


        static void Main(string[] args)
        {
            var sqlConn = new SQLiteConnection();

            try
            {
                using(sqlConn = new SQLiteConnection(connString))
                {
                    sqlConn.Open();
                    Console.WriteLine("Connection is open!");

                }
                Console.WriteLine("Connection is close.");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message);}


            Console.ReadKey();
        }
    }
}
