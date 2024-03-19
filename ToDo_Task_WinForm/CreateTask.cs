using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToDo_Task_WinForm.Classes;

namespace ToDo_Task_WinForm
{
    public partial class CreateTaskWindow : Form
    {
        public CreateTaskWindow()
        {
            InitializeComponent();
        }

        private void button_CreateTask_Click(object sender, EventArgs e)
        {
            TableTaskCurrent tasker = new TableTaskCurrent();

            var tmp = Convert.ToDateTime(dateTimePicker_DateEnd.Text);

            if (!CheckEmptyField())
                MessageBox.Show("FALSE");
            else
            {
                BindData(tasker);
                TaskOperation.ToDoTasker.CreateTask(tasker);
            }
        }

        /// <summary>
        /// Проверка на пустоту полей
        /// </summary>
        /// <returns></returns>
        private bool CheckEmptyField()
        {
            if (textBox_TileTask.Text == string.Empty ||
                richTextBox_TextTask.Text == string.Empty ||
                dateTimePicker_DateEnd.Value < DateTime.Now)
                return false;
            return true;
        }

        /// <summary>
        /// Присваивание данные из полей формы к экземпляру класса
        /// </summary>
        /// <param name="tasker"></param>
        private void BindData(TableTaskCurrent tasker)
        {
            tasker.TitleTask = textBox_TileTask.Text;
            tasker.TextTask = richTextBox_TextTask.Text;
            tasker.DateCreate = SetZeroSecond(DateTime.Now);
            tasker.DateEnd = SetZeroSecond(dateTimePicker_DateEnd.Value);
            tasker.Status = 1;
        }

        /// <summary>
        /// Установка секунд в 0
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime SetZeroSecond(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);
        }
    }
}
