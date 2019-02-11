using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace D3Tool
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            try
            {
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //处理UI线程异常
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                //处理非UI线程异常
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                Process[] tProcess = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
                foreach (var p in tProcess)
                {
                    if (p.Id != Process.GetCurrentProcess().Id)
                    {
                        p.Kill();
                    }

                }
                RegisterDM();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Frm_Main());
            }
            catch (Exception ex)
            {
                string str = GetExceptionMsg(ex,string.Empty);
                System.IO.File.AppendAllText("Exception.txt",str,Encoding.GetEncoding("gb2312"));
                //MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception, e.ToString());
            str = "Application:" + str;
            System.IO.File.AppendAllText("Exception.txt", str, Encoding.GetEncoding("gb2312"));
            //LogManager.WriteLog(str);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
            str = "CurrentDomain:" + str;
            System.IO.File.AppendAllText("Exception.txt", str, Encoding.GetEncoding("gb2312"));
            //LogManager.WriteLog(str);
        }

        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }
        static void RegisterDM()
        {
            string p32 = @"C:\Windows\System32";
            string p64 = @"C:\Windows\SysWOW64";
            string filename = "dm.dll";
            string f32 = @"C:\Windows\System32\dm.dll";
            string f64 = @"C:\Windows\SysWOW64\dm.dll";

            if (!File.Exists(filename))
                return;
            if (System.IO.Directory.Exists(p64))
            {
                if (!File.Exists(f64))
                {
                    File.Copy(filename, f64);
                    Process.Start("regsvr32", "/s dm.dll");
                }
            }
            else
            {
                if (!File.Exists(f32))
                {
                    File.Copy(filename, f32);
                    Process.Start("regsvr32", "/s dm.dll");
                }
            } 
        }
    }
}
