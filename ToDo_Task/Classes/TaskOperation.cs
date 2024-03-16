using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Windows.Controls;
using ToDo_Task.Classes;

namespace TaskOperation
{
    internal class Tasker
    {
        public static SQLiteConnection SqlConn;
        private static SQLiteDataAdapter sqlAdapter;
        private SQLiteCommand? sqlComm;
        private static object tableViewer;
        private static string defSelectFrom = "SELECT * FROM TaskCurrent";
        private static string fileDB = "ToDoBase.db";

        public Tasker(ref SQLiteConnection sqlConn, object sender)
        {
            SqlConn = sqlConn;

            TypeSw
        }

        private bool CheckDBFile(ref SqlConnection sqlConn)
        {
            if (!File.Exists(fileDB))
                return false;
            return true;
        }



        public static void OpenDB()
        {
            using (SqlConn = new SQLiteConnection($"Data Source = {fileDB}; Version = 3"))
            {
                SqlConn.Open();
                SQLiteCommand sqlComm = new SQLiteCommand(defSelectFrom, SqlConn);
                sqlComm.ExecuteNonQuery();

                var dataTable = new DataTable("TaskCurrent");
                sqlAdapter = new SQLiteDataAdapter(sqlComm);
                sqlAdapter.Fill(dataTable);
                .ItemsSource = dataTable.DefaultView;
                sqlAdapter.Update(dataTable);
            }
        }

        private void UpdateTable(object sender)
        {
            if (sender is DataGrid)
            {
                var dataGrid = sender as DataGrid;
                sqlAdapter.Update(((DataView)dataGrid.ItemsSource).Table);
            }
        }

        public void CreateTask(SQLiteConnection sqlConnection, SQLiteCommand sqlCommand)
        {

        }

    }
}
