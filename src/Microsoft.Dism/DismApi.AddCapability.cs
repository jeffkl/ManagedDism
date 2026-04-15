// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Dism
{
    /// <summary>
    /// Represents the main entry point into the Deployment Image Servicing and Management (DISM) API.
    /// </summary>
    public static partial class DismApi
    {
        /// <summary>
        /// Add a capability to an image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
        /// <param name="capabilityName">The name of the capability that is being added.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void AddCapability(DismSession session, string capabilityName)
        {
            AddCapability(session, capabilityName, limitAccess: false, sourcePaths: null, progressCallback: null, userData: null);
        }

        /// <summary>
        /// Add a capability to an image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
        /// <param name="capabilityName">The name of the capability that is being added.</param>
        /// <param name="limitAccess">The flag indicates whether WU/WSUS should be contacted as a source location for downloading the payload of a capability. If payload of the capability to be added exists, the flag is ignored.</param>
        /// <param name="sourcePaths">A list of source locations. The function shall look up removed payload files from the locations specified in SourcePaths, and if not found, continue the search by contacting WU/WSUS depending on parameter LimitAccess.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="DismRebootRequiredException">When the operation requires a reboot to complete.</exception>
        public static void AddCapability(DismSession session, string capabilityName, bool limitAccess, List<string>? sourcePaths)
        {
            AddCapability(session, capabilityName, limitAccess, sourcePaths, progressCallback: null, userData: null);
        }

        /// <summary>
        /// Add a capability to an image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
        /// <param name="capabilityName">The name of the capability that is being added.</param>
        /// <param name="limitAccess">The flag indicates whether WU/WSUS should be contacted as a source location for downloading the payload of a capability. If payload of the capability to be added exists, the flag is ignored.</param>
        /// <param name="sourcePaths">A list of source locations. The function shall look up removed payload files from the locations specified in SourcePaths, and if not found, continue the search by contacting WU/WSUS depending on parameter LimitAccess.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <param name="userData">Optional user data to pass to the DismProgressCallback method.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="DismRebootRequiredException">When the operation requires a reboot to complete.</exception>
        public static void AddCapability(DismSession session, string capabilityName, bool limitAccess, List<string>? sourcePaths, DismProgressCallback? progressCallback, object? userData)
        {
            // Create a DismProgress object to wrap the callback and allow cancellation
            using DismProgress progress = new(progressCallback, userData);

            AddCapability(session, capabilityName, limitAccess, sourcePaths, progress);
        }

        /// <summary>
        /// Asynchronously adds a capability to an image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
        /// <param name="capabilityName">The name of the capability that is being added.</param>
        /// <param name="limitAccess">The flag indicates whether WU/WSUS should be contacted as a source location for downloading the payload of a capability. If payload of the capability to be added exists, the flag is ignored.</param>
        /// <param name="sourcePaths">A list of source locations. The function shall look up removed payload files from the locations specified in SourcePaths, and if not found, continue the search by contacting WU/WSUS depending on parameter LimitAccess.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None" />.</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the operation is canceled.</exception>
        /// <exception cref="DismRebootRequiredException">When the operation requires a reboot to complete.</exception>
        public static Task AddCapabilityAsync(DismSession session, string capabilityName, bool limitAccess, List<string>? sourcePaths, CancellationToken cancellationToken = default)
        {
            return AddCapabilityAsync(session, capabilityName, limitAccess, sourcePaths, progress: null, cancellationToken);
        }

        /// <summary>
        /// Asynchronously adds a capability to an image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
        /// <param name="capabilityName">The name of the capability that is being added.</param>
        /// <param name="limitAccess">The flag indicates whether WU/WSUS should be contacted as a source location for downloading the payload of a capability. If payload of the capability to be added exists, the flag is ignored.</param>
        /// <param name="sourcePaths">A list of source locations. The function shall look up removed payload files from the locations specified in SourcePaths, and if not found, continue the search by contacting WU/WSUS depending on parameter LimitAccess.</param>
        /// <param name="progress">An optional <see cref="IProgress{T}" /> provider to receive progress updates.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None" />.</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the operation is canceled.</exception>
        /// <exception cref="DismRebootRequiredException">When the operation requires a reboot to complete.</exception>
        public static Task AddCapabilityAsync(DismSession session, string capabilityName, bool limitAccess, List<string>? sourcePaths, IProgress<DismProgress>? progress, CancellationToken cancellationToken = default)
        {
            return AddCapabilityAsync(session, capabilityName, limitAccess, sourcePaths, progress, userData: null, cancellationToken);
        }

        /// <summary>
        /// Asynchronously adds a capability to an image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
        /// <param name="capabilityName">The name of the capability that is being added.</param>
        /// <param name="limitAccess">The flag indicates whether WU/WSUS should be contacted as a source location for downloading the payload of a capability. If payload of the capability to be added exists, the flag is ignored.</param>
        /// <param name="sourcePaths">A list of source locations. The function shall look up removed payload files from the locations specified in SourcePaths, and if not found, continue the search by contacting WU/WSUS depending on parameter LimitAccess.</param>
        /// <param name="progress">An optional <see cref="IProgress{T}" /> provider to receive progress updates.</param>
        /// <param name="userData">Optional user data to pass to the specified <see cref="IProgress{T}" />.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None" />.</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the operation is canceled.</exception>
        /// <exception cref="DismRebootRequiredException">When the operation requires a reboot to complete.</exception>
        public static Task AddCapabilityAsync(DismSession session, string capabilityName, bool limitAccess, List<string>? sourcePaths, IProgress<DismProgress>? progress, object? userData, CancellationToken cancellationToken = default)
        {
            return DismUtilities.RunAsync(
                static (state, progress) =>
                {
                    AddCapability(state.session, state.capabilityName, state.limitAccess, state.sourcePaths, progress);

                    return true;
                },
                (session, capabilityName, limitAccess, sourcePaths),
                progress, userData, cancellationToken);
        }

        private static void AddCapability(DismSession session, string capabilityName, bool limitAccess, List<string>? sourcePaths, DismProgress progress)
        {
            // Get the list of source paths as an array
            string[] sourcePathsArray = sourcePaths?.ToArray() ?? [];

            int hresult = NativeMethods.DismAddCapability(session, capabilityName, limitAccess, sourcePathsArray, (uint)sourcePathsArray.Length, progress.EventHandle, progress.DismProgressCallbackNative, IntPtr.Zero);

            DismUtilities.ThrowIfFail(hresult, session);
        }

        internal static partial class NativeMethods
        {
            /// <summary>
            /// Add a capability to an image.
            /// </summary>
            /// <param name="session">A valid DismSession. The DismSession must be associated with an image. You can associate a session with an image by using the DismOpenSession.</param>
            /// <param name="name">The name of the capability that is being added.</param>
            /// <param name="limitAccess">The flag indicates whether WU/WSUS should be contacted as a source location for downloading the payload of a capability. If payload of the capability to be added exists, the flag is ignored.</param>
            /// <param name="sourcePaths">A list of source locations. The function shall look up removed payload files from the locations specified in SourcePaths, and if not found, continue the search by contacting WU/WSUS depending on parameter LimitAccess.</param>
            /// <param name="sourcePathCount">The count of entries in SourcePaths.</param>
            /// <param name="cancelEvent">This is a handle to an event for cancellation.</param>
            /// <param name="progress">Pointer to a client defined callback function to report progress.</param>
            /// <param name="userData">User defined custom data. This will be passed back to the user through the callback.</param>
            /// <returns>Returns S_OK on success.</returns>
            /// <remarks><a href="https://msdn.microsoft.com/en-us/library/windows/desktop/mt684919.aspx" /> HRESULT WINAPI DismAddCapability(_In_ DismSession Session, _In_ PCWSTR Name, _In_ BOOL LimitAccess, _In_ PCWSTR* SourcePaths, _In_opt_ UINT SourcePathCount, _In_opt_ HANDLE CancelEvent, _In_opt_ DISM_PROGRESS_CALLBACK Progress, _In_opt_ PVOID UserData);</remarks>
#if NET7_0_OR_GREATER
            [LibraryImport(DismDllName, StringMarshalling = DismStringMarshalling)]
            public static partial
#else
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            public static extern
#endif
            int DismAddCapability(
                DismSession session,
                string name,
                [MarshalAs(UnmanagedType.Bool)] bool limitAccess,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 6)] string[] sourcePaths,
                UInt32 sourcePathCount,
                SafeWaitHandle cancelEvent,
                DismProgressCallbackNative progress,
                IntPtr userData);
        }
    }
}
