﻿using System;

namespace Microsoft.Dism
{
    public static partial class DismApi
    {
        /// <summary>
        /// Allows tests to override the functionality of the <see cref="GetLastErrorMessage"/> method.
        /// </summary>
        internal static Func<string> GetLastErrorMessageTestHook = null;
    }
}
