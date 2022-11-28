using System.Reflection;

namespace Engine.Core
{
    public abstract class Enumeration<T> : IComparable
    {
        private readonly int _Value;
        private readonly string _DisplayName;

        protected Enumeration()
        {
        }
        protected Enumeration(int iValue, string sDisplayName)
        {
            _Value = iValue;
            _DisplayName = sDisplayName;
        }
        public int Value
        {
            get { return _Value; }
        }
        public string DisplayName
        {
            get { return _DisplayName; }
        }
        public override string ToString()
        {
            return DisplayName;
        }
        public static IEnumerable<T> GetAll<T>() where T : Enumeration<T>, new()
        {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var info in fields)
            {
                var instance = new T();
                var locatedValue = info.GetValue(instance) as T;

                if (locatedValue != null)
                {
                    yield return locatedValue;
                }
            }
        }
        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration<T>;

            if (otherValue == null)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = _Value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }
        public override int GetHashCode()
        {
            return _Value.GetHashCode();
        }
        public static int AbsoluteDifference(Enumeration<T> firstValue, Enumeration<T> secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.Value - secondValue.Value);
            return absoluteDifference;
        }
        public static T FromValue<T>(int value) where T : Enumeration<T>, new()
        {
            var matchingItem = Parse<T, int>(value, "value", item => item.Value == value);
            return matchingItem;
        }
        public static T FromDisplayName<T>(string displayName) where T : Enumeration<T>, new()
        {
            var matchingItem = Parse<T, string>(displayName, "display name", item => item.DisplayName == displayName);
            return matchingItem;
        }
        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration<T>, new()
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
                throw new InvalidCastException(message);
            }

            return matchingItem;
        }
        public int CompareTo(object obj)
        {
            return Value.CompareTo(((Enumeration<T>)obj).Value);
        }
        public static bool operator ==(Enumeration<T> left, Enumeration<T> right)
        {
            if (ReferenceEquals(left, null))
            {
                return ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }
        public static bool operator !=(Enumeration<T> left, Enumeration<T> right)
        {
            return !(left == right);
        }
        public static bool operator <(Enumeration<T> left, Enumeration<T> right)
        {
            return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0;
        }
        public static bool operator <=(Enumeration<T> left, Enumeration<T> right)
        {
            return ReferenceEquals(left, null) || left.CompareTo(right) <= 0;
        }
        public static bool operator >(Enumeration<T> left, Enumeration<T> right)
        {
            return !ReferenceEquals(left, null) && left.CompareTo(right) > 0;
        }
        public static bool operator >=(Enumeration<T> left, Enumeration<T> right)
        {
            return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0;
        }
        public static T[] GetDeviceTypes()
        {
            List<T> oList = new();
            Type oType = typeof(T);
            FieldInfo[] aInfo = oType.GetFields();
            for (int i = 0; i < aInfo.Length; i++)
            {
                oList.Add((T)aInfo[i].GetValue(aInfo[i]));
            }
            return oList.ToArray();
        }
        public static FieldInfo[] GetDescribingTypes()
        {
            Type oType = typeof(T);
            return oType.GetFields();
        }
    }
}
