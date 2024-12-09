// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;

namespace Microsoft.Dism
{
    public static partial class DismApi
    {
        /// <summary>
        /// Changes an offline Windows image to a higher edition.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
        /// <param name="editionId">The edition to set the image to.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="DismRebootRequiredException">When the operation requires a reboot to complete.</exception>
        public static void SetEdition(DismSession session, string editionId)
        {
            SetEdition(session, editionId, progressCallback: null, userData: null);
        }

        /// <summary>
        /// Changes an offline Windows image to a higher edition.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
        /// <param name="editionId">The edition to set the image to.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="DismRebootRequiredException">When the operation requires a reboot to complete.</exception>
        public static void SetEdition(DismSession session, string editionId, Dism.DismProgressCallback? progressCallback)
        {
            SetEdition(session, editionId, progressCallback, userData: null);
        }

        /// <summary>
        /// Changes an offline Windows image to a higher edition.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
        /// <param name="editionId">The edition to set the image to.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <param name="userData">Optional user data to pass to the DismProgressCallback method.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="DismRebootRequiredException">When the operation requires a reboot to complete.</exception>
        public static void SetEdition(DismSession session, string editionId, Dism.DismProgressCallback? progressCallback, object? userData)
        {
            SetEdition(session, editionId, productKey: null, progressCallback, userData);
        }

        /// <summary>
        /// Changes an offline Windows image to a higher edition and sets the product key.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
        /// <param name="editionId">The edition to set the image to.</param>
        /// <param name="productKey">A product key for the specified edition.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="DismRebootRequiredException">When the operation requires a reboot to complete.</exception>
        public static void SetEditionAndProductKey(DismSession session, string editionId, string productKey)
        {
            SetEditionAndProductKey(session, editionId, productKey, progressCallback: null, userData: null);
        }

        /// <summary>
        /// Changes an offline Windows image to a higher edition and sets the product key.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
        /// <param name="editionId">The edition to set the image to.</param>
        /// <param name="productKey">A product key for the specified edition.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="DismRebootRequiredException">When the operation requires a reboot to complete.</exception>
        public static void SetEditionAndProductKey(DismSession session, string editionId, string productKey, Dism.DismProgressCallback? progressCallback)
        {
            SetEditionAndProductKey(session, editionId, productKey, progressCallback, userData: null);
        }

        /// <summary>
        /// Changes an offline Windows image to a higher edition and sets the product key.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
        /// <param name="editionId">The edition to set the image to.</param>
        /// <param name="productKey">A product key for the specified edition.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <param name="userData">Optional user data to pass to the DismProgressCallback method.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="DismRebootRequiredException">When the operation requires a reboot to complete.</exception>
        public static void SetEditionAndProductKey(DismSession session, string editionId, string productKey, Dism.DismProgressCallback? progressCallback, object? userData)
        {
            SetEdition(session, editionId, productKey, progressCallback, userData);
        }

        /// <summary>
        /// Changes an offline Windows image to a higher edition and optionaly sets the product key.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
        /// <param name="editionId">The edition to set the image to.</param>
        /// <param name="productKey">Optional. A product key for the specified edition.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <param name="userData">Optional user data to pass to the DismProgressCallback method.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="DismRebootRequiredException">When the operation requires a reboot to complete.</exception>
        private static void SetEdition(DismSession session, string editionId, string? productKey, Dism.DismProgressCallback? progressCallback, object? userData)
        {
            // Create a DismProgress object to wrap the callback and allow cancellation
            DismProgress progress = new DismProgress(progressCallback, userData);

            int hresult = NativeMethods.DismSetEdition(session, editionId, productKey, progress.EventHandle, progress.DismProgressCallbackNative, IntPtr.Zero);

            DismUtilities.ThrowIfFail(hresult, session);
        }

        internal static partial class NativeMethods
        {
            /// <summary>
            /// Changes an offline Windows image to a higher edition.
            /// </summary>
            /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
            /// <param name="editionID">The edition to set the image to.</param>
            /// <param name="productKey">Optional. A product key for the specified edition.</param>
            /// <param name="cancelEvent">Optional. You can set a CancelEvent for this function in order to cancel the operation in progress when signaled by the client. If the CancelEvent is received at a stage when the operation cannot be canceled, the operation will continue and return a success code. If the CancelEvent is received and the operation is canceled, the image state is unknown. You should verify the image state before continuing or discard the changes and start again.</param>
            /// <param name="progress">Optional. A pointer to a client-defined DismProgressCallback Function.</param>
            /// <param name="userData">Optional. User defined custom data.</param>
            /// <returns>Returns <c>S_OK</c> on success.</returns>
            [DllImport(DismDllName, EntryPoint = "_DismSetEdition", CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismSetEdition(
                DismSession session,
                [MarshalAs(UnmanagedType.LPWStr)] string editionID,
                [MarshalAs(UnmanagedType.LPWStr)] string? productKey,
                SafeWaitHandle cancelEvent,
                DismProgressCallback progress,
                IntPtr userData);
        }
    }
}