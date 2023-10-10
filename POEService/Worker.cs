using Microsoft.Extensions.Options;
using POEService.common;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml.Linq;

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
            while (!stoppingToken.IsCancellationRequested)
            {
                Process[] processes = Process.GetProcessesByName(serviceConfig.POEExeName);
                if (processes.Length > 0)
                {
                    await Task.Delay(30 * 1000, stoppingToken);
                    KillCrossProxy();
                }
                else
                {
                    await Task.Delay(30 * 1000, stoppingToken);
                    EnableCrossProxy();
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
        public void KillProcess()
        {
            List<string> processList = new List<string>();
            processList.Add("CrossInstallerExternal64");
            processList.Add("CrossInstallerExternal");
            processList.Add("CrossProxy");
            foreach (var pName in processList)
            {
                Process[] processes = Process.GetProcessesByName(pName);
                foreach (Process process in processes)
                {
                    process.Kill();

                }
                _logger.LogInformation("KillProcess: {time}", DateTimeOffset.Now);
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
                _logger.LogInformation("KillCrossProxy: {time}", DateTimeOffset.Now);
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
                _logger.LogInformation("EnableCrossProxy: {time}", DateTimeOffset.Now);
            }
        }

    }
}