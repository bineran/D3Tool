using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTool
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
        public static int TrimLength(this string str)
        {
            if (str == null) { return 0; }
            return str.Trim().Length;
        }
        public static Color? ToColor(this string str)
        {
            if (str == null) { return null; }
            try
            {
                if (str.TrimLength() == 6)
                { 
                    str=str.Trim();
                    var r = Convert.ToInt32(str.Substring(0, 2), 16);
                    var g = Convert.ToInt32(str.Substring(2, 2), 16);
                    var b = Convert.ToInt32(str.Substring(4, 2), 16);
                    return Color.FromArgb(r, g, b);
                }
            }
            catch {
              
            }
            return null;
        }
    }
}
