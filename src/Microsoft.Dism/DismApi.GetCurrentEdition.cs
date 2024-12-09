// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Runtime.InteropServices;

namespace Microsoft.Dism
{
    public partial class DismApi
    {
        /// <summary>
        /// Gets the Windows edition ID of the image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
        /// <returns>The Windows edition ID of the image.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static string GetCurrentEdition(DismSession session)
        {
            int hresult = NativeMethods.DismGetCurrentEdition(session, out IntPtr editionId);

            try
            {
                DismUtilities.ThrowIfFail(hresult, session);

                string? editionString = editionId.ToStructure<DismString>();

                return editionString!;
            }
            finally
            {
                Delete(editionId);
            }
        }

        internal static partial class NativeMethods
        {
            /// <summary>
            /// Gets edition information about a Windows image.
            /// </summary>
            /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
            /// <param name="editionId">Recieves a pointer to a <see cref="DismString"/> structure containing the edition ID of the image.</param>
            /// <returns>Returns <c>S_OK</c> on success.</returns>
            [DllImport(DismDllName, EntryPoint = "_DismGetCurrentEdition")]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismGetCurrentEdition(DismSession session, out IntPtr editionId);
        }
    }
}
