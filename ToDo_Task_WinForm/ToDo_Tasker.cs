using System.Data;
using System.Data.SQLite;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Windows.Forms;
using TaskOperation;

namespace ToDo_Task_WinForm
{
    public partial class ToDo_Tasker : Form
    {
        private ToDoTasker tasker;

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

        private void ToDo_Tasker_Load(object sender, EventArgs e)
        {
            tasker = new ToDoTasker(dataGridView_Main);
        }

        private void button_CreateTask_Click(object sender, EventArgs e)
        {
            CreateTaskWindow taskWindow = new CreateTaskWindow();
            taskWindow.ShowDialog();
        }

        private void button_EditTask_Click(object sender, EventArgs e)
        {
            if (dataGridView_Main.CurrentRow != null)
            {
                EditTaskWindow editTaskWindow = new EditTaskWindow(dataGridView_Main);
                editTaskWindow.ShowDialog();
            }
            else
                MessageBox.Show("Выберите строку для редактирования");
        }

        private void button_DeleteTask_Click(object sender, EventArgs e)
        {
            if (dataGridView_Main.CurrentRow != null)
            {
                ToDoTasker.DeleteRecord(dataGridView_Main);
            }
            else
                MessageBox.Show("Выберите строку для удаления");
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            ToDoTasker.UpdateTable();
        }
    }
}
