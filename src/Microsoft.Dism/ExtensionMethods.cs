using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Dism
{
    internal static class ExtensionMethods
    {
        public static IEnumerable<T> AsEnumerable<T>(this IntPtr ptr, int count)
        {
            if (count == 0)
            {
                yield break;
            }

            // Calculate the size of the struct
            //
            var structSize = Marshal.SizeOf(typeof(T));

            // Get the starting point as a long
            //
            var startPtr = ptr.ToInt64();

            // Loop through each pointer
            //
            for (int i = 0; i < count; i++)
            {
                // Get the address of the current structure
                //
                var currentPtr = new IntPtr(startPtr + (i * structSize));

                // Return the structure at the current pointer
                //
                yield return currentPtr.ToStructure<T>();
            }
        }

        /// <summary>
        /// Marshals data from an unmanaged block of memory to a newly allocated managed object of the specified type.
        /// </summary>
        /// <typeparam name="T">The System.Type of object to be created. This type object must represent a formatted class or a structure.</typeparam>
        /// <param name="ptr">A pointer to an unmanaged block of memory.</param>
        /// <returns>A managed object containing the data pointed to by the ptr parameter.</returns>
        /// <exception cref="ArgumentException">The T parameter layout is not sequential or explicit.
        /// -or-
        /// The T parameter is a generic type.</exception>
        public static T ToStructure<T>(this IntPtr ptr)
        {
            return (T)Marshal.PtrToStructure(ptr, typeof(T));
        }
    }
}