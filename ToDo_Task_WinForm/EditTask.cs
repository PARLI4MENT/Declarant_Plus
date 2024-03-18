using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToDo_Task_WinForm.Classes;

namespace ToDo_Task_WinForm
{
    public partial class EditTaskWindow : Form
    {
        private DataGridView dataGrid;
        private static TableTaskCurrent taskCurrent;

        public EditTaskWindow(DataGridView dataGridView)
        {
            InitializeComponent();
            dataGrid = dataGridView;
            taskCurrent = new TableTaskCurrent();
        }

        private void EditTaskWindow_Load(object sender, EventArgs e)
        {
            BindData(this.dataGrid);
        }

        private void button_EditTask_Click(object sender, EventArgs e)
        {
            FillData(ref taskCurrent);
            TaskOperation.ToDoTasker.UpdateTask(taskCurrent);
        }

        private void FillData(ref TableTaskCurrent task)
        {
            taskCurrent.TitleTask = textBox_TileTask.Text;
            taskCurrent.TextTask = richTextBox_TextTask.Text;
            taskCurrent.DateEnd = Convert.ToDateTime(dateTimePicker_DateEnd.Value);
        }

        private void BindData(DataGridView dataGridView)
        {
            if (dataGridView.SelectedCells.Count != 0)
            {
                taskCurrent.ID = dataGridView.CurrentRow.Cells[0].Value.ToString();
                
                // Заголовок задачи
                if(dataGridView.CurrentRow.Cells[1].Value.ToString() != string.Empty)
                    textBox_TileTask.Text = dataGridView.CurrentRow.Cells[1].Value.ToString();
                // Описание задачи
                if(dataGridView.CurrentRow.Cells[2].Value.ToString() != string.Empty)
                    richTextBox_TextTask.Text = dataGridView.CurrentRow.Cells[2].Value.ToString();
                // End date
                dateTimePicker_DateEnd.Value = Convert.ToDateTime(dataGridView.CurrentRow.Cells[4].Value);
            }
        }

    }
}
