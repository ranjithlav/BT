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
                throw new ArgumentNullException($"{paramName} is NullOrWhiteSpace");
            }
            return true;
        }
    }
}
