using DMTools.libs;
using DMTools.Static;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace DMTools
{
    internal static class Program
    {

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            //LoadResourceDll.RegistDLL();
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
          //var pathDMDLL=  Application.StartupPath + "Lib\\dm.dll";
           // RegisterDmSoft.AutoRegCom($"regsvr32 -s {pathDMDLL}");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var path = Application.StartupPath + $@"NLogs\Info\{DateTime.Now.ToString("yyyy-MM-dd")}.txt";

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            var registerDmSoftDllResult = RegisterDmSoft.RegisterDmSoftDll();
            ApplicationConfiguration.Initialize();
            Application.Run(new FormMain(args));
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

    }
}