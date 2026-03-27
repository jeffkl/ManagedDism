// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System.Runtime.InteropServices;

namespace Microsoft.Dism
{
    public static partial class DismApi
    {
        /// <summary>
        /// Represents native functions called by DismApi.
        /// </summary>
        internal static partial class NativeMethods
        {
#if !NET7_0_OR_GREATER
            private const CharSet DismCharacterSet = CharSet.Unicode;
#endif
            private const string DismDllName = "DismApi";
#if NET7_0_OR_GREATER
            private const StringMarshalling DismStringMarshalling = StringMarshalling.Utf16;
#endif
        }
    }
}