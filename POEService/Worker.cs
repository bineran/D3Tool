using Microsoft.Extensions.Options;
using POEService.common;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace POEService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ServiceConfig serviceConfig;
        public Worker(ILogger<Worker> logger, IOptions<ServiceConfig> _config)
        {
            _logger = logger;
            serviceConfig = _config.Value;
            _logger.LogInformation("serviceConfig:" + serviceConfig.ToJson());
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int pid = 0;
            bool canRest = true;
            while (!stoppingToken.IsCancellationRequested)
            {
                Process[] processes = Process.GetProcessesByName(serviceConfig.POEExeName);
                if (processes.Length > 0 && pid!= processes[0].Id)
                {
                    pid = processes[0].Id;
                    await Task.Delay(30 * 1000, stoppingToken);
               
                    if (!stoppingToken.IsCancellationRequested)
                    {
                        KillCrossProxy();
                        KillProcess();
                        canRest = true;
                    }

                }
                else if (processes.Length==0 && canRest) 
                {
                    EnableCrossProxy();
                    canRest = false;
                }
                await Task.Delay(3 * 1000, stoppingToken);

            }
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            EnableCrossProxy();
            return base.StopAsync(cancellationToken);
        }
        public void KillProcess()
        {
            foreach (var pName in this.serviceConfig.ProcessList)
            {
                Process[] processes = Process.GetProcessesByName(pName);
                foreach (Process process in processes)
                {
                    process.Kill();
                }
              
            }
        }

        public void KillCrossProxy()
        {
            var powerShellScript = new StringBuilder();
            powerShellScript.AppendLine("Set-ExecutionPolicy RemoteSigned -Force");
            foreach (var item in this.serviceConfig.PathList)
            {
                powerShellScript.AppendLine($"icacls {item} /Deny Everyone:F");
            }
            powerShellScript.AppendLine("exit");
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
        public void EnableCrossProxy()
        {
            var powerShellScript = new StringBuilder();
            powerShellScript.AppendLine("Set-ExecutionPolicy RemoteSigned -Force");
            foreach (var item in this.serviceConfig.PathList)
            {
                powerShellScript.AppendLine($"icacls {item} /grant Everyone:F");
            }
            powerShellScript.AppendLine("exit");
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