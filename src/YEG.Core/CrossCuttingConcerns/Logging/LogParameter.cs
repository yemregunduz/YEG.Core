﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEG.Core.CrossCuttingConcerns.Logging
{
    public class LogParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public string Type { get; set; }
        public LogParameter(string name, object value, string type)
        {
            Name = name;
            Value = value;
            Type = type;
        }
        public LogParameter()
        {
            
        }
    }
}
