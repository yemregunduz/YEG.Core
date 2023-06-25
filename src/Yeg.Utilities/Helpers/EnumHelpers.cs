using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yeg.Utilities.Attributes;
using Yeg.Utilities.Enums;

namespace Yeg.Utilities.Helpers
{
    public class EnumHelpers
    {
        /// <summary>
        /// Gets the sorting mode for an enum type.
        /// </summary>
        /// <param name="enumType">The enum type.</param>
        /// <returns>The sorting mode of the enum type.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="enumType"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="enumType"/> is not an enum type.</exception>
        public static SortModeEnum GetSortMode(Type enumType)
        {
            if (enumType == null)
                throw new ArgumentNullException(nameof(enumType));

            if (!enumType.IsEnum)
                throw new ArgumentException("Target type must be an enum.", nameof(enumType));

            var attrs = enumType.GetCustomAttributes(typeof(EnumSortModeAttribute), false);

            if (attrs.Length > 0 && attrs[0] is EnumSortModeAttribute sortModeAttribute)
                return sortModeAttribute.SortMode;

            return SortModeEnum.ByName;
        }
    }
}
