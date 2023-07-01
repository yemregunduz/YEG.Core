using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEG.Core.Pipelines.Performance
{
    public interface IDurationTrackedRequest
    {
        public int Interval { get; }
    }
}
