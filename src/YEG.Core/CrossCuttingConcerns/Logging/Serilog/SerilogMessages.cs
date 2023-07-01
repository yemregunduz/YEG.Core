using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEG.Core.CrossCuttingConcerns.Logging.Serilog
{
    public static class SerilogMessages
    {
        public static string NullConfigurationMessage =>
            "You have sent a blank value! Something went wrong. Please try again.";
    }
}
