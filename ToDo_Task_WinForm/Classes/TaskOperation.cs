using System.Data.SQLite;
using System.Data;
using ToDo_Task_WinForm.Classes;
using System.Diagnostics;
using Microsoft.Toolkit.Uwp.Notifications;
using Timer = System.Timers.Timer;


namespace TaskOperation
{
    public class ToDoTasker
    {
        public static SQLiteConnection SqlConn { get; private set; }
        private static SQLiteDataAdapter sqlAdapter;
        private SQLiteCommand? sqlComm;
        private static DataGridView gridView { get; set; }
        private static string selectViewData = "SELECT TitleTask, TextTask," +
            "DateCreated, DateEnd FROM TaskCurrent WHERE Status = 1";
        private static string selectAllData = "SELECT * FROM TaskCurrent WHERE Status = 1 ORDER BY DateEnd";
        private static string fileDB = "ToDoBase.db";
        //private static List<NotifyReminder> notifyReminders;
        private static Timer timer;

        public ToDoTasker()
        {
            SqlConn = new SQLiteConnection();
            if (CheckDbFile())
                CheckOverdueTask();
            else
                CreateNewDb();
            OpenDB();
        }

        public ToDoTasker(DataGridView dataGrid)
        {
            SqlConn = new SQLiteConnection();
            gridView = dataGrid;
            if (CheckDbFile())
                CheckOverdueTask();
            else
                CreateNewDb();
            OpenDB();
        }

        private static void SetTimer()
        {
            timer = new Timer();
            timer.Elapsed += TimerOnElipsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private static void TimerOnElipsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Debug.WriteLine($"Timer {e.SignalTime}");
        }

        /// <summary>
        /// Создание нового пустого файла БД SQLite и инициализация таблицы
        /// </summary>
        private void CreateNewDb()
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

        /// <summary>
        /// Открытие файла БД и проверка задач на "просроченность по дате" 
        /// </summary>
        private void OpenDB()
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
                    SetReminds();
                }
            }
        }

        private static void SetReminds()
        {
            /*
            //if (notifyReminders != null)
            //{
            //    if (notifyReminders.Count > 0)
            //    {
            //        Debug.WriteLine("############ STOP ALL TIMERS ############");
            //        foreach (var rem in notifyReminders)
            //        {
            //            rem.StopTimer();
            //        }
            //    }
            //    using (SqlConn = new SQLiteConnection($"Data Source = {fileDB}; Version = 3;"))
            //    {
            //        SqlConn.Open();
            //        string command = "SELECT ID, DateEnd FROM TaskCurrent WHERE Status = 1 ORDER BY DateEnd";
            //        SQLiteCommand sqlComm = new SQLiteCommand(command, SqlConn);
            //        using (SQLiteDataReader reader = sqlComm.ExecuteReader())
            //        {
            //            if (reader.HasRows)
            //            {
            //                notifyReminders = new List<NotifyReminder>();
            //                while (reader.Read())
            //                {
            //                    int _id = Int16.Parse(reader["ID"].ToString());
            //                    DateTime _endDate = Convert.ToDateTime(reader["DateEnd"]);
            //                    notifyReminders.Add(new NotifyReminder(_id, _endDate));
            //                }
            //            }
            //            else return;
            //        }
            //    }
            //}
            */


        }

        /// <summary>
        /// Проверка на существование файла бд SQLite
        /// </summary>
        /// <returns></returns>
        private static bool CheckDbFile()
        {
            if (!File.Exists(fileDB))
                return false;
            return true;
        }

        /// <summary>
        /// Проверка задач из БД на "просроченность по дате"
        /// </summary>
        private void CheckOverdueTask()
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
                                {
                                    ShowNotify($"Задача {reader["TitleTask"].ToString()} была удалена из-за просроченности",
                                        reader["TextTask"].ToString(), Convert.ToDateTime(reader["DateEnd"]));
                                    DeleteRecord(SqlConn, Int16.Parse(reader["ID"].ToString()));
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Вывод всплывающих уведомлений
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Message"></param>
        /// <param name="endDate"></param>
        public static void ShowNotify(string Title, string Message, DateTime endDate)
        {
            var notify = new ToastContentBuilder();
            notify.AddText(Title, AdaptiveTextStyle.Title);
            notify.AddText($"{Message}\nДата: {endDate.ToString()}", AdaptiveTextStyle.Default);
            notify.Show();
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

        /// <summary>
        /// Удаление записи по выделенной области DataGrid
        /// </summary>
        /// <param name="dataGrid"></param>
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
                    UpdateTable();
                }
            }
        }

        /// <summary>
        /// Кастомизация таблицы и переименование колонок таблицы
        /// </summary>
        private static void CustomTableColumn()
        {
            gridView.Columns["ID"].Visible = false;

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

        /// <summary>
        /// Создания задачи и оправка запроса на содание записи в БД SQLite
        /// </summary>
        /// <param name="tasker"></param>
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

        public async static void UpdateTask(TableTaskCurrent tasker)
        {
            using (SqlConn = new SQLiteConnection($"Data Source={fileDB};Version=3;"))
            {
                SqlConn.Open();
                SQLiteCommand sqlComm = new SQLiteCommand();
                sqlComm.Connection = SqlConn;
                sqlComm.CommandText = $"UPDATE TaskCurrent " +
                    $"SET TitleTask = '{tasker.TitleTask}', TextTask = '{tasker.TextTask}', DateCreated = '{DateTime.Now.ToString()}', DateEnd = '{tasker.DateEnd.ToString()}', Status = 1 " +
                    $"WHERE ID = {tasker.ID};";
                sqlComm.ExecuteNonQuery();
                Debug.WriteLine("EXECUTE QUERY =>\t" + $"UPDATE TaskCurrent (TitleTask, TextTask, DateCreated, DateEnd, Status)" +
                    $" VALUES ({tasker.TitleTask}, {tasker.TextTask}, {DateTime.Now.ToString()}, {tasker.DateEnd.ToString()}, 1) WHERE ID = {tasker.ID};");
            }
            UpdateTable();
        }
    }
}