using NLog.Extensions.Hosting;
using NLog.Extensions.Logging;
using POEService.common;
using System.Runtime.InteropServices;

namespace POEService
{
    public class Program
    {

        public static void Main(string[] args)
        {

            var hostBuilder = Host.CreateDefaultBuilder(args);
            #region NLOG
            hostBuilder.ConfigureLogging((hostContext, logging) =>
            {
#if DEBUG
#else
                logging.ClearProviders();
#endif
                logging.AddNLog(new NLogProviderOptions
                {
                    CaptureMessageTemplates = true,
                    CaptureMessageProperties = true
                });
            });
            hostBuilder.UseNLog();
            #endregion
            hostBuilder.ConfigureAppConfiguration((config) =>
            {

                config.SetBasePath(AppContext.BaseDirectory);
                config.AddJsonFile("appsettings.json", true, true);
                //var aesPassword = Environment.GetEnvironmentVariable("aesPasswordConfig");
                //config.AddJsonFileAes256("appsettings.json", true, true,aesPassword);
            });
            hostBuilder.ConfigureServices((hostBuilderContext, services) =>
            {
                services.Configure<ServiceConfig>(hostBuilderContext.Configuration.GetSection("ServiceConfig"));
                services.AddHostedService<Worker>();
            });
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                hostBuilder.UseWindowsService();
            }
            hostBuilder.Build().Run();
        }


    }
}