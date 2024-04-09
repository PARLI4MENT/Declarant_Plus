using System.Windows;

namespace ToDo_Task
{
    public partial class MainWindow : Window
    {
        //private void CloseApplicationCommand()
        //{
        //    Application.Current.Shutdown();
        //}


        public MainWindow()
        {
            InitializeComponent();
            //var tasker = new TaskOperation.Tasker();
        }

        private void MainWindows_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btn_CreateTask_Click(object sender, RoutedEventArgs e)
        {
            CreateNewTask taskWindow = new CreateNewTask();
            taskWindow.ShowDialog();
        }

        private void btn_UpdateTable_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}