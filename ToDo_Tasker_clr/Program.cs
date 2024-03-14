using System;
using System.Data.SQLite;

namespace ToDo_Tasker_clr
{
    internal class Program
    {
        private const string? connString = "Data Source=ToDoBase.db";

        static void Main(string[] args)
        {


            var sqlConn = new SQLiteConnection();

            try
            {
                using (sqlConn = new SQLiteConnection(connString))
                {
                    sqlConn.Open();
                    Console.WriteLine("Connection is open!");

                    SQLiteCommand sqlComm = sqlConn.CreateCommand();
                    DateTime dateEnd = DateTime.Now;
                    sqlComm.CommandText = "INSERT INTO TaskCurrent (TitleTask, TextTask, DateCreated, DateEnd)" +
                        $" VALUES ('Title 2', 'Text ToDo 2', '{DateTime.Now}', '{dateEnd.AddMinutes(1.0)}')";
                    sqlComm.ExecuteNonQuery();

                }
                Console.WriteLine("Connection is close.");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }


            Console.ReadKey();
        }
    }
}
