using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools
{
    public static class Ex_Cloass
    {
        public static string ToNowString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static Task TaskRun(this Action action)
        {
          return  Task.Run(() =>
            {
                try
                {
                    action();
                }
                catch
                { }
            });
        }
    }
}
