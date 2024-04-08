using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Windows.Controls;

namespace TaskOperation
{
    internal class Tasker
    {
        public static SQLiteConnection SqlConn { get; private set; }
        private static SQLiteDataAdapter sqlAdapter;
        private SQLiteCommand? sqlComm;
        private static DataGrid gridView {get; set;}
        private static string defSelectFrom = "SELECT TitleTask, TextTask, DateEnd FROM TaskCurrent";
        private static string fileDB = "ToDoBase.db";

        public Tasker()
        {
            SqlConn = new SQLiteConnection();
        }

        public Tasker(DataGrid dataGrid)
        {
            SqlConn = new SQLiteConnection();
            gridView = dataGrid;
        }

        public static void CreateNewDb()
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory + fileDB;
            SQLiteConnection.CreateFile(appPath);
            using (SqlConn = new SQLiteConnection($"Data Source={appPath}; Version = 3;"))
            {
                SqlConn.Open();
                string sqlCmd = "CREATE TABLE TaskCurrent (ID INTEGER PRIMARY KEY ON CONFLICT ABORT AUTOINCREMENT NOT NULL," +
                    "TitleTask   TEXT (0, 20)," +
                    "TextTask    TEXT," +
                    "DateCreated TEXT," +
                    "DateEnd     TEXT," +
                    "Status      INTEGER NOT NULL DEFAULT (1));";
                var cmd = new SQLiteCommand(sqlCmd, SqlConn);
                cmd.ExecuteNonQuery();
            }
        }

        public static void OpenDB()
        {
            if(CheckDbFile(SqlConn))
            using (SqlConn = new SQLiteConnection($"Data Source = {fileDB}; Version = 3;"))
            {
                SqlConn.Open();
                var sqlComm = new SQLiteCommand(defSelectFrom, SqlConn);
                sqlComm.ExecuteNonQuery();

                var dataTable = new DataTable("TaskCurrent");
                sqlAdapter = new SQLiteDataAdapter(sqlComm);
                sqlAdapter.Fill(dataTable);
                gridView.ItemsSource = dataTable.DefaultView;
                sqlAdapter.Update(dataTable);
            }
        }

        private static bool CheckDbFile(SQLiteConnection sqlConn)
        {
            if (!File.Exists(fileDB))
                return false;
            return true;
        }

        private void UpdateTable(object sender)
        {
            if (sender is DataGrid)
            {
                var dataGrid = sender as DataGrid;
                sqlAdapter.Update(((DataView)dataGrid.ItemsSource).Table);
            }
        }
        

    }
}