// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System.Runtime.InteropServices;

namespace Microsoft.Dism
{
    public static partial class DismApi
    {
        /// <summary>
        /// Validates the given Windows product key.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
        /// <param name="productKey">The product key to validate.</param>
        /// <returns><see langword="true"/> if the product key is valid, <see langword="false"/> otherwise.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static bool ValidateProductKey(DismSession session, string productKey)
        {
            int hresult = NativeMethods.DismValidateProductKey(session, productKey);

            if ((uint)hresult != DismApi.DISMAPI_E_UNKNOWN_PRODUCT)
            {
                DismUtilities.ThrowIfFail(hresult);
            }

            return hresult == 0;
        }

        internal static partial class NativeMethods
        {
            /// <summary>
            /// Validates the given Windows product key.
            /// </summary>
            /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
            /// <param name="productKey">The product key to validate.</param>
            /// <returns>Returns <c>S_OK</c> on success.</returns>
            [DllImport(DismDllName, EntryPoint = "_DismValidateProductKey", CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismValidateProductKey(DismSession session, string productKey);
        }
    }
}
