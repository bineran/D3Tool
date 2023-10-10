/*using DMTools.libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using DMTools.Config;
using DMTools.Control;
//using Dm;
using Idmsoft = DMTools.libs.Idmsoft;
using System.Diagnostics;

namespace DMTools.FunList
{

    [KeyName("杀POE多余进程")]
    public class POEKill : BaseD3
    {
        public const EnumD3 enumD3Name = EnumD3.POE多余进程;


        List<(int, int, int)> bagPoints = new List<(int, int, int)>();
        public POEKill(D3Param d3Param, List<KeyTimeSetting> Times, EnumD3 enumD3) : base(d3Param, Times, enumD3)
        {

            this.StartEvent += POEJN_StartEvent;
        }
        public static void DenyPOE()
        {
            var CrossProxy = "CrossProxy";
           var  CrossProxyProcess =Process.GetProcessesByName(CrossProxy);
            if (CrossProxyProcess.Length==0)
            {
                return;
            }

            var fileinfo = new FileInfo(CrossProxyProcess[0].MainModule.FileName);
            var path = fileinfo.Directory;


            var powerShellScript = new StringBuilder();
            powerShellScript.AppendLine("Set-ExecutionPolicy RemoteSigned -Force");

            List<string> exeList=new List<string>();
            exeList.Add("CrossProxy.exe");
            exeList.Add("CrossInstallerExternal.exe");
            exeList.Add("CrossInstallerExternal64.exe");
            foreach (var exeName in exeList)
            {
                powerShellScript.AppendLine($"icacls { path+"\\"+ exeName} /Deny Everyone:F");
            }

            powerShellScript.AppendLine("exit");
            if (powerShellScript.Length > 40)
            {
                using (Process powerShellProcess = new Process())
                {
                    powerShellProcess.StartInfo.FileName = "powershell.exe";
                    powerShellProcess.StartInfo.RedirectStandardInput = true;
                    powerShellProcess.StartInfo.RedirectStandardOutput = true;
                    powerShellProcess.StartInfo.UseShellExecute = false;
                    powerShellProcess.StartInfo.CreateNoWindow = true;

                    powerShellProcess.Start();

                    // 将命令写入PowerShell进程的标准输入
                    powerShellProcess.StandardInput.WriteLine(powerShellScript.ToString());

                    // 读取PowerShell进程的输出
                    string output = powerShellProcess.StandardOutput.ReadToEnd();


                    powerShellProcess.WaitForExit();
                }
                System.IO.File.WriteAllText("CrossProxy.Config", path.FullName, Encoding.UTF8);
            }
        }
        public static void GrantPOE(string CrossProxyPath)
        {
            var powerShellScript = new StringBuilder();
            powerShellScript.AppendLine("Set-ExecutionPolicy RemoteSigned -Force");

            List<string> exeList = new List<string>();
            exeList.Add("CrossProxy.exe");
            exeList.Add("CrossInstallerExternal.exe");
            exeList.Add("CrossInstallerExternal64.exe");
            foreach (var exeName in exeList)
            {
                powerShellScript.AppendLine($"icacls {CrossProxyPath + "\\" + exeName} /grant Everyone:F");
            }
            powerShellScript.AppendLine("exit");
            if (powerShellScript.Length > 40)
            {
                using (Process powerShellProcess = new Process())
                {
                    powerShellProcess.StartInfo.FileName = "powershell.exe";
                    powerShellProcess.StartInfo.RedirectStandardInput = true;
                    powerShellProcess.StartInfo.RedirectStandardOutput = true;
                    powerShellProcess.StartInfo.UseShellExecute = false;
                    powerShellProcess.StartInfo.CreateNoWindow = true;

                    powerShellProcess.Start();

                    // 将命令写入PowerShell进程的标准输入
                    powerShellProcess.StandardInput.WriteLine(powerShellScript.ToString());

                    // 读取PowerShell进程的输出
                    string output = powerShellProcess.StandardOutput.ReadToEnd();


                    powerShellProcess.WaitForExit();
                }
            }
        }
        public void Kill()
        {
            List<string> processList = new List<string>();
            processList.Add("CrossInstallerExternal64");
            processList.Add("CrossInstallerExternal");
            processList.Add("CrossProxy");
            foreach (var  pName in processList)
            {
                Process[] processes = Process.GetProcessesByName(pName);
                foreach (Process process in processes)
                {
                        process.Kill();
                    
                }

            }
          
        }
        public void POEJN_StartEvent()
        {
            DenyPOE();
            Kill();
            //while (!cs.IsCancellationRequested)
            //{ 
            //    Kill();
            //    Sleep(2000);
            //}
        }




    }


}
*/