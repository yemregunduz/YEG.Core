using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEG.Core.CrossCuttingConcerns.Logging.Serilog.Logger
{
    public class ConsoleLogger : LoggerServiceBase
    {
        public ConsoleLogger()
        {
            Logger = new LoggerConfiguration().WriteTo.Console(
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}").CreateLogger();
        }
    }
}
