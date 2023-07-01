using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEG.Core.Security
{
    public static class SecurityMessages
    {
        public static string NullConfigurationMessage => 
            "TokenOptions cannot be null. Please check your appsettings.json file and ensure that the \"TokenOptions\" section is properly defined.";
    }
}
