// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Runtime.InteropServices;

namespace Microsoft.Dism
{
    public static partial class DismApi
    {
        /// <summary>
        /// Gets information related to a Windows product key.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
        /// <param name="productKey">The product key to get information for.</param>
        /// <returns>A <see cref="DismProductKeyInfo"/> object.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismProductKeyInfo GetProductKeyInfo(DismSession session, string productKey)
        {
            int hresult = NativeMethods.DismGetProductKeyInfo(session, productKey, out IntPtr editionIdPtr, out IntPtr channelPtr);

            try
            {
                DismUtilities.ThrowIfFail(hresult, session);

                string? editionId = editionIdPtr.ToStructure<DismString>();
                string? channel = channelPtr.ToStructure<DismString>();

                return new DismProductKeyInfo(editionId!, channel!);
            }
            finally
            {
                Delete(editionIdPtr);
                Delete(channelPtr);
            }
        }

        internal static partial class NativeMethods
        {
            /// <summary>
            /// Gets information related to a Windows product key.
            /// </summary>
            /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
            /// <param name="productKey">The product key to get information about.</param>
            /// <param name="editionIdPtr">Pointer that will receive a <see cref="DismString"/> with the edition ID.</param>
            /// <param name="channelPtr">Pointer that will receive a <see cref="DismString"/> with the channel name.</param>
            /// <returns>Returns <c>S_OK</c> on success.</returns>
            [DllImport(DismDllName, EntryPoint = "_DismGetProductKeyInfo", CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismGetProductKeyInfo(DismSession session, string productKey, out IntPtr editionIdPtr, out IntPtr channelPtr);
        }
    }
}
