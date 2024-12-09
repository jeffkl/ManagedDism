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
        /// Gets the list of Windows edition IDs the provided image can be set to.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
        /// <returns>The list of editions the provided image can be set to.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismEditionCollection GetTargetEditions(DismSession session)
        {
            int hresult = NativeMethods.DismGetTargetEditions(session, out IntPtr editionIdPtr, out uint count);

            try
            {
                DismUtilities.ThrowIfFail(hresult, session);

                return new DismEditionCollection(editionIdPtr, count);
            }
            finally
            {
                Delete(editionIdPtr);
            }
        }

        internal static partial class NativeMethods
        {
            /// <summary>
            /// Gets the list of Windows edition IDs the provided image can be set to.
            /// </summary>
            /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
            /// <param name="editionIds">Pointer to an array of <see cref="DismString"/> structures containing the target edition IDs of the image.</param>
            /// <param name="count">The number of edition IDs that are returned.</param>
            /// <returns>Returns <c>S_OK</c> on success.</returns>
            [DllImport(DismDllName, EntryPoint = "_DismGetTargetEditions")]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismGetTargetEditions(DismSession session, out IntPtr editionIds, out uint count);
        }
    }
}
