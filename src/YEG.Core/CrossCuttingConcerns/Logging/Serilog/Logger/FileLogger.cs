using Microsoft.Extensions.Configuration;
using Serilog;
using YEG.Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;

namespace YEG.Core.CrossCuttingConcerns.Logging.Serilog.Logger
{
    public class FileLogger : LoggerServiceBase
    {
        private readonly IConfiguration _configuration;
        public FileLogger(IConfiguration configuration)
        {
            _configuration  = configuration;

            FileLogConfiguration fileLogConfig = _configuration.GetSection("SeriLogConfigurations:FileLogConfiguration")
                .Get<FileLogConfiguration>() ?? throw new Exception(SerilogMessages.NullConfigurationMessage);

            string logFilePath = string.Format("{0},{1}",Directory.GetCurrentDirectory() + fileLogConfig.FolderPath, ".txt");

            Logger = new LoggerConfiguration()
                .WriteTo.File(
                    path: logFilePath,
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: null,
                    fileSizeLimitBytes: 5000000,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}").CreateLogger();
        }
    }
}
