//using System.Diagnostics;
//using TaskOperation;

//namespace ToDo_Task_WinForm.Classes
//{
//    public class NotifyReminder
//    {
//        //public int Id { get; private set; }
//        //public DateTime EndDate { get; private set; }
//        private static System.Timers.Timer timer;
//        private TableTaskCurrent taskCurrent;

//        public NotifyReminder(int ID, DateTime EndDate)
//        {
//            taskCurrent = new TableTaskCurrent();
//            taskCurrent.ID = ID.ToString();
//            taskCurrent.DateEnd = EndDate;

//            timer = new System.Timers.Timer();
//            timer.Interval = 3000;

//            timer.Elapsed += OnTimedEvent;
//            timer.AutoReset = true;
//            timer.Enabled = true;
//        }

//        public void StopTimer()
//        {
//            timer.Elapsed -= OnTimedEvent;
//            timer.Stop();
//            timer.Dispose();
//        }

//        private async void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
//        {
//            if (CheckDoneTask())
//            {
//                timer.Dispose();
//                Debug.WriteLine($"\nЗадача {taskCurrent.ID}\tОкончание {DateTime.Now}");
//                //ToDoTasker.UpdateTable();
//            }
//        }

//        private bool CheckDoneTask()
//        { 
//            if (DateTime.Now >= taskCurrent.DateEnd)
//            {

//                Debug.WriteLine($"\nЗадача ID {taskCurrent.ID}\t Таймер сработал");
//                Debug.WriteLine($"Текущее время {DateTime.Now:G}");
//                ToDoTasker.ShowNotify(taskCurrent.TitleTask, taskCurrent.TextTask, taskCurrent.DateEnd.Value);
//                return true;
//            }
//            Debug.WriteLine($"Задача ID {taskCurrent.ID}\tОсталось: {taskCurrent.DateEnd - DateTime.Now}");
//            return false;
//        }
//    }
//}
