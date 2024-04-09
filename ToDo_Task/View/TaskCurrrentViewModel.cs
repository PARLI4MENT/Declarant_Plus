using CommunityToolkit.Mvvm.ComponentModel;
using ToDo_Task.Classes;

namespace ToDo_Task.ViewModel
{
    public partial class TaskCurrrentViewModel : ObservableObject
    {
        [ObservableProperty]
        private static List<TableTaskCurrent> _TaskList;
    }
}
