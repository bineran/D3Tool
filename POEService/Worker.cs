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
                    if(!stoppingToken.IsCancellationRequested)
                    {
                        KillCrossProxy();
                        KillProcess();
                    }
                }
                else
                {
                    await Task.Delay(1000, stoppingToken);
                    if (!stoppingToken.IsCancellationRequested)
                    {
                        EnableCrossProxy();
                    }
                }
                await Task.Delay(1000, stoppingToken);
            }
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

                // ������д��PowerShell���̵ı�׼����
                powerShellProcess.StandardInput.WriteLine(powerShellScript.ToString());

                // ��ȡPowerShell���̵����
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

                // ������д��PowerShell���̵ı�׼����
                powerShellProcess.StandardInput.WriteLine(powerShellScript.ToString());

                // ��ȡPowerShell���̵����
                string output = powerShellProcess.StandardOutput.ReadToEnd();


                powerShellProcess.WaitForExit();
                _logger.LogInformation("EnableCrossProxy: {time}", DateTimeOffset.Now);
            }
        }

    }
}