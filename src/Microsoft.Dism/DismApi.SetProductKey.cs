// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System.Runtime.InteropServices;

namespace Microsoft.Dism
{
    public static partial class DismApi
    {
        /// <summary>
        /// Sets the product key of the current image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
        /// <param name="productKey">The product key.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void SetProductKey(DismSession session, string productKey)
        {
            int hresult = NativeMethods.DismSetProductKey(session, productKey);

            DismUtilities.ThrowIfFail(hresult);
        }

        internal static partial class NativeMethods
        {
            /// <summary>
            /// Sets the product key of the current image.
            /// </summary>
            /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
            /// <param name="productKey">The product key.</param>
            /// <returns>Returns <c>S_OK</c> on success.</returns>
            [DllImport(DismDllName, EntryPoint = "_DismSetProductKey", CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismSetProductKey(DismSession session, string productKey);
        }
    }
}
