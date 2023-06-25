using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yeg.Utilities.Enums;

namespace Yeg.Utilities.Attributes
{
    [AttributeUsage(AttributeTargets.Enum)]
    public class EnumSortModeAttribute : Attribute
    {
        public EnumSortModeAttribute(SortModeEnum sortMode)
        {
            SortMode = sortMode;
        }
        public SortModeEnum SortMode { get; private set; }
    }
}
