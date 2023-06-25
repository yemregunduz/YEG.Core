using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEG.RestSharp.Models
{
    public class RestRequestParameter
    {
        public string Controller { get; set; }
        public string? Action { get; set; }
        public Dictionary<string, string>? Headers { get; set; }
        public string? QueryString { get; set; }
        public string? PathVariable { get; set; }
    }
}
