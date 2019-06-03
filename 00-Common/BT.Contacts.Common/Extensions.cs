using System;

namespace BT.Contacts.Common
{
    public static class Extensions
    {
        public static bool CheckNull<T>(this T obj)
        {
            if (ReferenceEquals(obj, null))
            {
                throw new ArgumentNullException($"{nameof(obj)} is NULL");
            }
            return true;
        }

        public static bool CheckNullOrEmpty(this string obj, string paramName)
        {
            if (ReferenceEquals(obj, null))
            {
                throw new ArgumentNullException($"{paramName} is NULL");
            }
            if (string.IsNullOrWhiteSpace(obj))
            {
                throw new ArgumentException($"{paramName} is NullOrWhiteSpace");
            }
            return true;
        }

        public static bool CheckLessThan(this int obj, int givenInt, string paramName)
        {
            if (obj < givenInt)
            {
                throw new ArgumentOutOfRangeException($"{paramName} is lessthan '{givenInt}'");
            }
            return true;
        }

        public static bool CheckLessThanOrEqual(this int obj, int givenInt, string paramName)
        {
            if (obj <= givenInt)
            {
                throw new ArgumentOutOfRangeException($"{paramName} is lessthan or equal '{givenInt}'");
            }
            return true;
        }
    }
}
