using System;

namespace VRD.Common.Utilities
{
    public static class GenericUtils
    {
        /// <summary>
        /// Creates default instance of the given type.
        /// </summary>
        /// <typeparam name="T">Type to perform method on.</typeparam>
        /// <returns> Returns default instance of <typeparamref name="T" /> . </returns>
        public static T CreateDefaultInstance<T>()
        {
            if (typeof(T).IsValueType || typeof(T) == typeof(String))
            {
                return default(T);
            }

            return Activator.CreateInstance<T>();
        }

        /// <summary>
        /// Checks whether the generic type is reference type and <c>null</c>.
        /// </summary>
        /// <typeparam name="T">Type to perform method on.</typeparam>
        /// <param name="mango">The object to check.</param>
        /// <returns>Returns <c>true</c> if <paramref name="mango"/> is reference type and <c>null</c>, <c>false</c> otherwise.</returns>
        /// <remarks>Returns <c>true</c> if <paramref name="mango"/> is <see cref="string"/> and <c>null</c>.</remarks>
        public static bool IsNull<T>(T mango)
        {
            object obj = mango;

            var type = typeof(T);

            if (type.IsValueType && type != typeof(String))
            {
                return false;
            }

            return obj == null;
        }
    }
}
