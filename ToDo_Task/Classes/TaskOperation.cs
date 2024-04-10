using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.IO;
using System.Windows.Controls;
using ToDo_Task.Classes;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Data.Common;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace TaskOperation
{
    partial class Tasker: ObservableObject
    {
        public static SqliteConnection SqlConn { get; private set; }
        private static string fileDB = "ToDoBase.db";

        [ObservableProperty]
        public static List<TableTaskCurrent>? _CurrentTaskList;

        public Tasker()
        {
            if(_CurrentTaskList == null)
                _CurrentTaskList = new List<TableTaskCurrent>();
            FillList();
        }

        private void FillList()
        {
            using (SqlConn = new SqliteConnection($"Data Source={fileDB};"))
            {
                SqlConn.Open();
                var comm = new SqliteCommand("SELECT * FROM TaskCurrent", SqlConn);
                comm.ExecuteNonQuery();

                using (SqliteDataReader dataReader = comm.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            CurrentTaskList.Add(new TableTaskCurrent
                            {
                                ID = dataReader[0].ToString(),
                                TitleTask = dataReader[1].ToString(),
                                TextTask = dataReader[2].ToString(),
                                DateCreate = Convert.ToDateTime(dataReader[3]),
                                DateEnd = Convert.ToDateTime(dataReader[4]),
                                Status = UInt16.Parse(dataReader[5].ToString())
                            });
                        }
                    }
                }
            }
        }

        private ICommand _AddRow;
        public ICommand AddRow
        {
            get
            {
                return _AddRow ?? (_AddRow = new RelayCommand(() =>
                {

                }));
            }
        }


        //private ICommand _AddTask;
        //public ICommand AddTask
        //{ 
        //    get
        //    {
        //        var newTask = new TableTaskCurrent();

        //        if(ToDo_Task.CreateNewTask)

        //        return _AddTask ?? (_AddTask = new RelayCommand(() =>
        //        {
        //            CurrentTaskList.Add(new TableTaskCurrent
        //            {

        //            });
        //        }));
        //    }
        //}
    }
}