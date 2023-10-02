using DMTools.libs;
using DMTools.Static;
using System;
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
            //����UI�߳��쳣
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //�����UI�߳��쳣
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
           // POEJN_StartEvent();
            var registerDmSoftDllResult = RegisterDmSoft.RegisterDmSoftDll();
            ApplicationConfiguration.Initialize();
            Application.Run(new FormMain(args));
        }
        public static void POEJN_StartEvent()
        {
            var alstr = GetDisabledPrograms();
            var powerShellScript = new StringBuilder();
            powerShellScript.AppendLine("Set-ExecutionPolicy RemoteSigned -Force");

            List<string> alprocss = new List<string>();
            alprocss.Add( @"CrossProxy");
            var op = "grant";//grant,deny
            var croossPath = @"D:\Game\WeGame\apps\Cross\Core\Stable\CrossProxy.exe";
            List<string> exes = new List<string>();
            exes.Add("CrossInstallerExternal64.exe");
            exes.Add("CrossInstallerExternal.exe");
            powerShellScript.AppendLine($"icacls {croossPath} /{op} Everyone:F");
            foreach (var exe in exes)
            {
                var exePath = croossPath.Replace($"CrossProxy.exe", exe);
                powerShellScript.AppendLine($"icacls {exePath} /{op} Everyone:F");
            }
            //foreach (var pName in alprocss)
            //{
            //    Process[] processes = Process.GetProcessesByName(pName);
            //    List<string> exes = new List<string>();
            //    exes.Add("CrossInstallerExternal64.exe");
            //    exes.Add("CrossInstallerExternal.exe");
            //    foreach (var process in processes)
            //    {

            //        try
            //        {
            //            if (process.MainModule != null)
            //            {
            //                powerShellScript.AppendLine($"icacls {process.MainModule.FileName} /{op} Everyone:F");
            //                foreach (var exe in exes)
            //                {
            //                    var exePath = process.MainModule.FileName.Replace($"{pName}.exe", exe);
            //                    powerShellScript.AppendLine($"icacls {exePath} /{op} Everyone:F");
            //                }
            //            }

            //        }
            //        catch
            //        { 

            //        }
            //    }



            //}
            powerShellScript.AppendLine("exit");
            if (powerShellScript.Length > 50)
            {
                using (Process powerShellProcess = new Process())
                {
                    powerShellProcess.StartInfo.FileName = "powershell.exe";
                    powerShellProcess.StartInfo.RedirectStandardInput = true;
                    powerShellProcess.StartInfo.RedirectStandardOutput = true;
                    powerShellProcess.StartInfo.UseShellExecute = false;
                   // powerShellProcess.StartInfo.CreateNoWindow = true;

                    powerShellProcess.Start();

                    // ������д��PowerShell���̵ı�׼����
                    powerShellProcess.StandardInput.WriteLine(powerShellScript.ToString());

                    // ��ȡPowerShell���̵����
                    string output = powerShellProcess.StandardOutput.ReadToEnd();
                  
                  
                    powerShellProcess.WaitForExit();
                }
            }
        }
        private static List<string> GetDisabledPrograms()
        {
            List<string> disabledPrograms = new List<string>();

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";
                startInfo.RedirectStandardOutput = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                startInfo.Arguments = "/c icacls /dumpacl *";

                Process process = new Process();
                process.StartInfo = startInfo;
                process.Start();

                while (!process.StandardOutput.EndOfStream)
                {
                    string line = process.StandardOutput.ReadLine();
                    if (line.Contains("Deny"))
                    {
                        string[] splitLine = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string programName = splitLine[splitLine.Length - 1];
                        disabledPrograms.Add(programName);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ִ������ʱ���ִ���: " + e.Message);
            }

            return disabledPrograms;
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