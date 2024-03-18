using Hardcodet.Wpf.TaskbarNotification;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestNotify
{
    public partial class MainWindow : Window
    {
        WindowState winState;
        public MainWindow()
        {
            InitializeComponent();

            ToolTip toolTip = new ToolTip();
            //toolTip.
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if(WindowState == WindowState.Minimized)
                Hide();
            //else
            //    winState = WindowState;
        }

        private void TaskbarIcon_TrayLeftMouseDown(object sender, RoutedEventArgs e)
        {
            Show();
            //WindowState = winState;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}