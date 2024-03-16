using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Windows;
using TaskOperation;

namespace ToDo_Task
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private void CloseApplicationCommand()
        //{
        //    Application.Current.Shutdown();
        //}

        private Tasker task;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindows_Loaded(object sender, RoutedEventArgs e)
        {
            task = new Tasker(dataGrid_Main);
            //Tasker.OpenDB();

            //if (File.Exists("ToDoBase.db"))
            //    //MessageBox.Show($"{new FileInfo("ToDoBase.db").Directory.FullName}");
            //    // Get path app
            //    //MessageBox.Show($"{System.Reflection.Assembly.GetExecutingAssembly().Location}");
            //    MessageBox.Show($"{System.Reflection.Assembly.GetEntryAssembly().Location}");
            //else
            //    MessageBox.Show("Nope");
        }

        private void btn_CreateTask_Click(object sender, RoutedEventArgs e)
        {
            //CreateNewTask taskWindow = new CreateNewTask();
            //taskWindow.ShowDialog();

        }

        private void btn_UpdateTable_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    string? path = AppDomain.CurrentDomain.BaseDirectory;
            //    string? path2 = Environment.CurrentDirectory;
            //    string? path3 = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().
            //        GetName().CodeBase);
            //    MessageBox.Show(path);
            //}
            //catch (Exception ex){ MessageBox.Show(ex.Message); }

            Tasker.CreateNewDb();
        }
    }
}