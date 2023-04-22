using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Color = System.Drawing.Color;


    public static class Ex_Class
    {
        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

    public static void DoubleBuffered(this Control c)
        {
            Type dgvType = c.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
               BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(c, true, null);
        }
   

        public static byte[] HexStringToByteArray(this string hex)
        {
            hex = hex.Replace(" ", "");
            // Remove "0x" at the start of the string.
            if (hex.StartsWith("0x"))
            {
                hex = hex.Substring(2);
            }

            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        public static int newWidth(this int x, int newWidth=1920, int orgWidth = 1920)
        {

            var p = Math.Round(Convert.ToDouble(newWidth) / Convert.ToDouble(orgWidth), 2);

            return Convert.ToInt32(x * p);
        }
        public static int newHeight(this int x, int newHeight=1080, int orgHeight = 1080)
        {
            var p = Math.Round(Convert.ToDouble(newHeight) / Convert.ToDouble(orgHeight), 2);

            return Convert.ToInt32(x * p);
        }
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
        public static List<Color> ToColors(this string str)
        {
            var colors = new List<Color>();
            try
            {
                if (str.TrimLength() > 0)
                {
                    var cs = str.Split('|');

                    foreach (var c in cs)
                    {
                        if (c.TrimLength() == 6)
                        {
                            var r = Convert.ToInt32(str.Substring(0, 2), 16);
                            var g = Convert.ToInt32(str.Substring(2, 2), 16);
                            var b = Convert.ToInt32(str.Substring(4, 2), 16);

                            colors.Add(Color.FromArgb(r, g, b));
                        }
                    }
                }

            }
            catch
            {
                colors.Clear();
            }

            return colors;
        }
        public static List<String> ToColorStrList(this string str)
        {
            var colors = new List<string>();

            try
            {
                if (str.TrimLength() > 0)
                {
                    var cs = str.Split('|');

                    foreach (var c in cs)
                    {
                        if (c.Contains(" "))
                        {
                            var cStr=c.Split(" ");
                            if (cStr.Length == 3)
                            {
                                colors.Add(Convert.ToInt32(cStr[0]).ToString("x") + Convert.ToInt32(cStr[1]).ToString("x") + Convert.ToInt32(cStr[2]).ToString("x"));
                            }
                        }
                        else if (c.Contains(","))
                        {
                            var cStr = c.Split(",");
                            if (cStr.Length == 3)
                            {
                                colors.Add(Convert.ToInt32(cStr[0]).ToString("x") + Convert.ToInt32(cStr[1]).ToString("x") + Convert.ToInt32(cStr[2]).ToString("x"));
                            }
                        }
                        else  if (c.TrimLength() == 6)
                        {
                            var r = Convert.ToInt32(str.Substring(0, 2), 16);
                            var g = Convert.ToInt32(str.Substring(2, 2), 16);
                            var b = Convert.ToInt32(str.Substring(4, 2), 16);
                            colors.Add(c.Trim().ToLower());
                        }

                    }
                }

            }
            catch
            {
                colors.Clear();
                
            }

            return colors;
        }


    public static string ToColor(this string c)
    {
        if (c == null)
            return null;
        try
        {
            if (c.Contains(" "))
            {
                var cStr = c.Split(" ");
                if (cStr.Length == 3)
                {
                    return ColorTranslator.ToHtml(Color.FromArgb(Convert.ToInt32(cStr[0]), Convert.ToInt32(cStr[1]), Convert.ToInt32(cStr[2]))).Replace("#", "").ToLower();



                }
            }
            else if (c.Contains(","))
            {
                var cStr = c.Split(",");
                if (cStr.Length == 3)
                {
                    return ColorTranslator.ToHtml(Color.FromArgb(Convert.ToInt32(cStr[0]), Convert.ToInt32(cStr[1]), Convert.ToInt32(cStr[2]))).Replace("#", "").ToLower();

                }
            }
            else if (c.TrimLength() == 6)
            {
                var r = Convert.ToInt32(c.Substring(0, 2), 16);
                var g = Convert.ToInt32(c.Substring(2, 2), 16);
                var b = Convert.ToInt32(c.Substring(4, 2), 16);
                return c.Trim();
            }
            return null ;
        }
        catch
        {
            return null;
        }
    
    }
}

