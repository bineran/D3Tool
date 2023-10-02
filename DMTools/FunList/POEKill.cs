using DMTools.libs;
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

        public void POEJN_StartEvent()
        {

            var powerShellScript = new StringBuilder();
            powerShellScript.AppendLine("Set-ExecutionPolicy RemoteSigned");

            List<string> alprocss = new List<string>();
            alprocss.AddRange(new string[] { "CrossProxy.exe", "CrossInstallerExternal.exe", "CrossInstallerExternal64.exe" });
            foreach (var pName in alprocss)
            {
                Process[] processes = Process.GetProcessesByName(pName);
                foreach (var process in processes)
                {
                    if (process.MainModule != null)
                    {
                        powerShellScript.AppendLine($"icocls {process.MainModule.FileName} /deny Everyone:F");
                        process.Kill();
                    }
                }
            }
            powerShellScript.AppendLine("exit");
            if (powerShellScript.Length > 50)
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




    }


}
