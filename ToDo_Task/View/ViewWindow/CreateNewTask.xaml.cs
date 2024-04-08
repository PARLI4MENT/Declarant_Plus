using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ToDo_Task
{
    /// <summary>
    /// Interaction logic for CreateNewTask.xaml
    /// </summary>
    /// 

    public partial class CreateNewTask : Window
    {
        private double _WWnd;
        public double WWnd
        {
            get => _WWnd;
            set => _WWnd = value;
        }
        private double _HWnd;
        public double HWnd
        {
            get => _HWnd;
            set => _HWnd = value;
        }

        public CreateNewTask()
        {
            InitializeComponent();
        }

        private void txtBox_Time_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }

    public class MVVM: ObservableRecipient
    {

        private DateTime _Date = DateTime.Now;
        public DateTime Date
        {
            get => _Date;
            set
            {
                _Date = DateTime.Now;
            }
        }
    }
}
