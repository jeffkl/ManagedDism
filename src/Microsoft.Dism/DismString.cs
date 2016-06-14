using System;
using System.Runtime.InteropServices;

namespace Microsoft.Dism
{
    public static partial class DismApi
    {
        /// <summary>
        /// DISM API functions that return strings wrap the heap allocated PCWSTR in a DismString structure.
        /// </summary>
        /// <remarks>
        /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824782.aspx"/>
        /// typedef struct _DismString
        /// {
        ///     PCWSTR Value;
        /// } DismString;
        /// </remarks>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        internal class DismString
        {
            /// <summary>
            /// A null-terminated Unicode string.
            /// </summary>
            private string Value;

            /// <summary>
            /// Converts a DismString class to a String object
            /// </summary>
            /// <param name="dismString"></param>
            /// <returns></returns>
            public static implicit operator String(DismString dismString)
            {
                return dismString.Value;
            }

            /// <summary>
            /// Converts a String object to a DismString object.
            /// </summary>
            /// <param name="str">The string to convert.</param>
            /// <returns>A DismString object containing the string.</returns>
            public static implicit operator DismString(String str)
            {
                return new DismString
                {
                    Value = str
                };
            }
        }
    }
}