using System.Diagnostics;
using System.Text;

namespace DMTools
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.


            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //����UI�߳��쳣
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //������UI�߳��쳣
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Process[] tProcess = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            foreach (var p in tProcess)
            {
                if (p.Id != Process.GetCurrentProcess().Id)
                {

                    p.Kill();

                }

            }
     
            // RegisterDM();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
    


            ApplicationConfiguration.Initialize();
            Application.Run(new FormMain());
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
        /// �����Զ����쳣��Ϣ
        /// </summary>
        /// <param name="ex">�쳣����</param>
        /// <param name="backStr">�����쳣��Ϣ����exΪnullʱ��Ч</param>
        /// <returns>�쳣�ַ����ı�</returns>
        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************�쳣�ı�****************************");
            sb.AppendLine("������ʱ�䡿��" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("���쳣���͡���" + ex.GetType().Name);
                sb.AppendLine("���쳣��Ϣ����" + ex.Message);
                sb.AppendLine("����ջ���á���" + ex.StackTrace);
            }
            else
            {
                sb.AppendLine("��δ�����쳣����" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }
    }
}