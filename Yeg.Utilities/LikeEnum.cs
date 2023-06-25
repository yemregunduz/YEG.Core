using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yeg.Utilities
{
    public abstract class LikeEnum : IComparable // NOSONAR
    {
        public string Key { get; private set; }
        public string Description { get; private set; }

        public int Value { get; private set; }

        protected LikeEnum(int value, string key, string description = null) => (Value, Key, Description) = (value, key, description);

        public override string ToString() => Key;

        public string GetDescription() => Description;

        public static IEnumerable<T> GetAll<T>() where T : LikeEnum =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null))
                .Cast<T>();

        public override bool Equals(object obj)
        {
            if (obj is not LikeEnum otherValue)
            {
                return false;
            }

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        public int CompareTo(object other) => Value.CompareTo(((LikeEnum)other).Value);

        // Other utility methods ...
    }
}
