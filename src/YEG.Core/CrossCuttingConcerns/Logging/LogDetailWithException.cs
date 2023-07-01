
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEG.Core.CrossCuttingConcerns.Logging
{
    public class LogDetailWithException : LogDetail
    {
        public string ExceptionMessage { get; set; }
        public LogDetailWithException(string exceptionMessage, string fullName, string methodName, string user, List<LogParameter> logParameters) :
            base(fullName, methodName, user, logParameters)
        {
            ExceptionMessage = exceptionMessage;
        }
        public LogDetailWithException()
        {
            
        }
    }
}
