using System;
using System.Runtime.InteropServices;

namespace Microsoft.Dism
{
    public static partial class DismApi
    {
        /// <summary>
        /// Describes the language of the image.
        /// </summary>
        /// <remarks>
        /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824760.aspx" />
        /// typedef struct _DismString
        /// {
        ///     PCWSTR Value;
        /// } DismString;
        /// </remarks>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        internal class DismLanguage
        {
            /// <summary>
            /// A null-terminated Unicode string.
            /// </summary>
            public string Value;

            /// <summary>
            /// Converts a DismLanguage class to a String object
            /// </summary>
            /// <param name="language">A DismLanguage object to convert.</param>
            /// <returns>The string associated with the DismLanguage</returns>
            public static implicit operator String(DismLanguage language)
            {
                return language.Value;
            }

            /// <summary>
            /// Converts a String object to a DismLanguage object.
            /// </summary>
            /// <param name="str">A string to convert.</param>
            /// <returns>A DismLanguage containing the specified string.</returns>
            public static implicit operator DismLanguage(String str)
            {
                return new DismLanguage
                {
                    Value = str
                };
            }
        }
    }
}