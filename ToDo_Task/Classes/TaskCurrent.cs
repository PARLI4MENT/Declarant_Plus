using CommunityToolkit.Mvvm.ComponentModel;

namespace ToDo_Task.Classes
{
    public partial class TableTaskCurrent: ObservableObject
    {
        [ObservableProperty]
        private string _ID;

        [ObservableProperty]
        private string _TitleTask;

        [ObservableProperty]
        private string _TextTask;

        [ObservableProperty]
        private DateTime _DateCreate;

        [ObservableProperty]
        private DateTime? _DateEnd;

        [ObservableProperty]
        private uint _Status;
    }
}
