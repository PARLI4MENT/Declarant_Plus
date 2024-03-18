using System.Data.SQLite;
using System.Data;
using ToDo_Task_WinForm.Classes;
using System.Diagnostics;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace TaskOperation
{
    internal class ToDoTasker
    {
        public static SQLiteConnection SqlConn { get; private set; }
        private static SQLiteDataAdapter sqlAdapter;
        private SQLiteCommand? sqlComm;
        private static DataGridView gridView { get; set; }
        private static string selectViewData = "SELECT TitleTask, TextTask," +
            "DateCreated, DateEnd FROM TaskCurrent WHERE Status = 1";
        private static string selectAllData = "SELECT * FROM TaskCurrent WHERE Status = 1";
        private static string fileDB = "ToDoBase.db";

        public ToDoTasker()
        {
            SqlConn = new SQLiteConnection();
            CheckOverdueTask();
        }

        public ToDoTasker(DataGridView dataGrid)
        {
            SqlConn = new SQLiteConnection();
            gridView = dataGrid;
            CheckOverdueTask();
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
                    "DateEnd     TEXT NOT NULL," +
                    "Status      INTEGER NOT NULL DEFAULT (1));";
                SQLiteCommand cmd = new SQLiteCommand(sqlCmd, SqlConn);
                cmd.ExecuteNonQuery();
            }
        }

        public static void OpenDB()
        {
            if (CheckDbFile())
            {
                using (SqlConn = new SQLiteConnection($"Data Source = {fileDB}; Version = 3;"))
                {
                    SqlConn.Open();
                    SQLiteCommand sqlComm = new SQLiteCommand(selectAllData, SqlConn);
                    sqlComm.ExecuteNonQuery();

                    var dataTable = new DataTable("TaskCurrent");
                    sqlAdapter = new SQLiteDataAdapter(sqlComm);
                    sqlAdapter.Fill(dataTable);
                    gridView.DataSource = dataTable.DefaultView;
                    sqlAdapter.Update(dataTable);
                    CustomTableColumn();
                }
            }
        }

        private static bool CheckDbFile()
        {
            if (!File.Exists(fileDB))
                return false;
            return true;
        }

        /// <summary>
        /// Проверка задач из БД на "просроченность по дате"
        /// </summary>
        private static void CheckOverdueTask()
        {
            using (SqlConn = new SQLiteConnection($"Data Source = {fileDB}; Version = 3;"))
            {
                SqlConn.Open();
                SQLiteCommand sqlCommand = new SQLiteCommand(selectAllData, SqlConn);
                using (SQLiteDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader["DateEnd"].ToString() != string.Empty)
                            {
                                DateTime tmp = Convert.ToDateTime(reader["DateEnd"]);
                                Debug.WriteLine(tmp.ToString());
                                if (tmp < DateTime.Now)
                                    DeleteRecord(SqlConn, Int16.Parse(reader["ID"].ToString()));
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Удаление записей из таблицы по "ID"
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <param name="ID"></param>
        public static void DeleteRecord(SQLiteConnection sqlConn, int ID)
        {
            string delQueryByID = $"DELETE FROM TaskCurrent WHERE ID={ID};";
            SQLiteCommand sqlComm = new SQLiteCommand(delQueryByID, sqlConn);
            sqlComm.ExecuteNonQuery();
            Debug.WriteLine($"DELETED ROW  WHERE ID => {ID}");
        }
        
        public static void DeleteRecord(DataGridView dataGrid)
        {
            if (dataGrid.SelectedCells.Count != 0)
            {
                var ID = dataGrid.CurrentRow.Cells[0].Value.ToString();
                string delQueryByID = $"DELETE FROM TaskCurrent WHERE ID={ID};";
                using (SqlConn = new SQLiteConnection($"Data Source = {fileDB}; Version = 3;"))
                {
                    SqlConn.Open();
                    SQLiteCommand sqlComm = new SQLiteCommand(delQueryByID, SqlConn);
                    sqlComm.ExecuteNonQuery();
                    Debug.WriteLine($"DELETED ROW  WHERE ID => {ID}");
                }
            }
        }

        /// <summary>
        /// Смена статуса
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <param name="ID"></param>
        public static void RevertStatusRecord(SQLiteConnection sqlConn, int ID)
        {

        }

        /// <summary>
        /// Кастомизация таблицы и переименование колонок таблицы
        /// </summary>
        private static void CustomTableColumn()
        {
            //gridView.Columns["ID"].Visible = false;

            gridView.Columns["TitleTask"].HeaderText = "Задача";
            gridView.Columns["TitleTask"].Width = 70;

            gridView.Columns["TextTask"].HeaderText = "Описание";
            gridView.Columns["TextTask"].Width = 320;

            gridView.Columns["DateCreated"].HeaderText = "Дата создания";
            gridView.Columns["DateCreated"].Width = 120;

            gridView.Columns["DateEnd"].HeaderText = "Дата окончания";
            gridView.Columns["DateEnd"].Width = 120;

            gridView.Columns["Status"].Visible = false;
        }

        public static void CreateTask(TableTaskCurrent tasker)
        {
            using (SqlConn = new SQLiteConnection($"Data Source={fileDB};Version=3;"))
            {
                SqlConn.Open();
                SQLiteCommand sqlComm = new SQLiteCommand();
                sqlComm.Connection = SqlConn;
                sqlComm.CommandText = $"INSERT INTO TaskCurrent (TitleTask, TextTask, DateCreated, DateEnd, Status) " +
                    $"VALUES ('{tasker.TitleTask}', '{tasker.TextTask}', '{DateTime.Now.ToString()}', '{tasker.DateEnd.ToString()}', 1);";
                sqlComm.ExecuteNonQuery();
                Debug.WriteLine("EXECUTE QUERY =>\t" + $"INSERT INTO TaskCurrent (TitleTask, TextTask, DateCreated, DateEnd, Status)" +
                    $" VALUES ({tasker.TitleTask}, {tasker.TextTask}, {DateTime.Now.ToString()}, {tasker.DateEnd.ToString()}, 1);");
            }
            UpdateTable();
        }

        /// <summary>
        /// Обновление данных таблицы
        /// </summary>
        public async static void UpdateTable()
        {
            if (CheckDbFile())
            {
                gridView.DataSource = null;
                await using (SqlConn = new SQLiteConnection($"Data Source = {fileDB}; Version = 3;"))
                {
                    SqlConn.Open();
                    SQLiteCommand sqlComm = new SQLiteCommand(selectAllData, SqlConn);
                    sqlComm.ExecuteNonQuery();

                    var dataTable = new DataTable("TaskCurrent");
                    sqlAdapter = new SQLiteDataAdapter(sqlComm);
                    sqlAdapter.Fill(dataTable);
                    gridView.DataSource = dataTable.DefaultView;
                    sqlAdapter.Update(dataTable);
                }
            }
            CustomTableColumn();
        }
    }
}