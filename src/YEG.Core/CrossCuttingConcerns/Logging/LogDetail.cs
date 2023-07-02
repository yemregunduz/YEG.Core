using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEG.Core.CrossCuttingConcerns.Logging
{
    public class LogDetail
    {
        public string FullName { get; set; }
        public string MethodName { get; set; }
        public string UserId { get; set; }
        public List<LogParameter> LogParameters { get; set; }

        public LogDetail(string fullName,string methodName, string userId, List<LogParameter> logParameters)
        {
            FullName = fullName;
            MethodName = methodName;
            UserId = userId;
            LogParameters = logParameters;
        }
        public LogDetail()
        {
            
        }
    }
}
