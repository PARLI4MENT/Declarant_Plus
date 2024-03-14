using System;
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
    internal class Program
    {
        private const string? connString = "Data Source=ToDoBase.db";

        static void Main(string[] args)
        {
            var sqlConn = new SQLiteConnection();

            // Write from sqlite database
            //try
            //{
            //    using (sqlConn = new SQLiteConnection(connString))
            //    {
            //        sqlConn.Open();
            //        Console.WriteLine("Connection is open!");

            //        SQLiteCommand sqlComm = sqlConn.CreateCommand();
            //        DateTime dateEnd = DateTime.Now;
            //        sqlComm.CommandText = "INSERT INTO TaskCurrent (TitleTask, TextTask, DateCreated, DateEnd)" +
            //            $" VALUES ('Title 2', 'Text ToDo 2', '{DateTime.Now}', '{dateEnd.AddMinutes(1.0)}')";
            //        sqlComm.ExecuteNonQuery();

            //    }
            //    Console.WriteLine("Connection is close.");
            //}
            //catch (Exception ex) { Console.WriteLine(ex.Message); }
            //Console.WriteLine();

            // Read rows from sqlite database
            



            Console.ReadKey();
        }
    }
}
