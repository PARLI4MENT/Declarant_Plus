using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_Task.Classes
{
    static class TypeSwitch
    {
        protected class CaseInfo
        {  
            public bool IsDefault { get; set; }
            public Type Target {  get; set; }
            public Action<object> Action { get; set; }
        }

        private static void Do(object sender, params CaseInfo[] cases)
        {
            var type = sender.GetType();
            foreach (var entry in cases)
            {
                if (entry.IsDefault || entry.Target.IsAssignableFrom(type))
                {
                    entry.Action(sender);
                    break;
                }
            }
        }

        public static void TypeConvert(object sender)
        {
        }
    }
}
