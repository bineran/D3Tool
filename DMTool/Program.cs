using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace DMTool
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>

        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.


            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

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
    



            Application.Run(new FormMain());
        }
 }
}