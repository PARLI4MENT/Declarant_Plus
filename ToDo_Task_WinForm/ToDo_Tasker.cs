using System.Data;
using System.Data.SQLite;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Windows.Forms;
using TaskOperation;

namespace ToDo_Task_WinForm
{
    public partial class ToDo_Tasker : Form
    {
        private Tasker tasker;

        public ToDo_Tasker()
        {
            InitializeComponent();
        }

        #region Локальные перменные (не нужны)
        private static string fileDB = "ToDoBase.db";
        private static string standartQuery = "SELECT * FROM TaskCurrent";
        private string appPath = AppDomain.CurrentDomain.BaseDirectory + fileDB;
        private SQLiteConnection sqlConn;
        private SQLiteCommand sqlComm;
        private SQLiteDataAdapter sqlDataAdapter;
        private DataSet dataSet = new DataSet();
        private DataTable dataTable = new DataTable();
        #endregion

        private void ExecuteQuery(string query)
        {
            using (sqlConn = new SQLiteConnection($"Data Source = {fileDB}; Version = 3;"))
            {
                this.sqlConn.Open();
                this.sqlComm = sqlConn.CreateCommand();
                this.sqlComm.CommandText = standartQuery;
                this.sqlComm.ExecuteNonQuery();
            }
        }

        private void LoadDB()
        {
            using (sqlConn = new SQLiteConnection($"Data Source = {fileDB}; Version = 3;"))
            {
                this.sqlConn.Open();
                this.sqlComm = sqlConn.CreateCommand();
                this.sqlComm.CommandText = standartQuery;

                sqlDataAdapter = new SQLiteDataAdapter(standartQuery, this.sqlConn);
                dataSet.Reset();
                sqlDataAdapter.Fill(dataSet);
                dataTable = dataSet.Tables[0];
                dataGridView_Main.DataSource = dataTable;

            }

        }

        private void ToDo_Tasker_Load(object sender, EventArgs e)
        {
            tasker = new Tasker(dataGridView_Main);
            Tasker.OpenDB();
        }

        private void button_CreateTask_Click(object sender, EventArgs e)
        {

        }

        private void button_EditTask_Click(object sender, EventArgs e)
        {

        }

        private void button_DeleteTask_Click(object sender, EventArgs e)
        {

        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            
        }
    }
}
