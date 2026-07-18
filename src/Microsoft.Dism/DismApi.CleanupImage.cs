// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Dism
{
	public static partial class DismApi
	{
		/// <summary>
		/// Performs a cleanup operation on a Windows® image in a mounted .wim or .vhd file.
		/// </summary>
		/// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
		/// <param name="type">The <see cref="DismCleanImageType"/> operation to perform.</param>
		/// <exception cref="DismException">When a failure occurs.</exception>
		public static void CleanupImage(DismSession session, DismCleanImageType type)
		{
			CleanupImage(session, type, DismCleanImageFlags.None);
		}

		/// <summary>
		/// Performs a cleanup operation on a Windows® image in a mounted .wim or .vhd file.
		/// </summary>
		/// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
		/// <param name="type">The <see cref="DismCleanImageType"/> operation to perform.</param>
		/// <param name="flags">The <see cref="DismCleanImageFlags"/> options to apply. Only valid for Component cleanup.</param>
		/// <exception cref="DismException">When a failure occurs.</exception>
		public static void CleanupImage(DismSession session, DismCleanImageType type, DismCleanImageFlags flags)
		{
			CleanupImage(session, type, flags, progressCallback: null);
		}

		/// <summary>
		/// Performs a cleanup operation on a Windows® image in a mounted .wim or .vhd file.
		/// </summary>
		/// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
		/// <param name="type">The <see cref="DismCleanImageType"/> operation to perform.</param>
		/// <param name="flags">The <see cref="DismCleanImageFlags"/> options to apply. Only valid for Component cleanup.</param>
		/// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
		/// <exception cref="DismException">When a failure occurs.</exception>
		public static void CleanupImage(DismSession session, DismCleanImageType type, DismCleanImageFlags flags, DismProgressCallback? progressCallback)
		{
			CleanupImage(session, type, flags, progressCallback, userData: null);
		}

		/// <summary>
		/// Performs a cleanup operation on a Windows® image in a mounted .wim or .vhd file.
		/// </summary>
		/// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
		/// <param name="type">The <see cref="DismCleanImageType"/> operation to perform.</param>
		/// <param name="flags">The <see cref="DismCleanImageFlags"/> options to apply. Only valid for Component cleanup.</param>
		/// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
		/// <param name="userData">Optional user data to pass to the DismProgressCallback method.</param>
		/// <exception cref="DismException">When a failure occurs.</exception>
		public static void CleanupImage(DismSession session, DismCleanImageType type, DismCleanImageFlags flags, DismProgressCallback? progressCallback, object? userData)
		{
			using DismProgress progress = new(progressCallback, userData);

			CleanupImage(session, type, flags, progress);
		}

		/// <summary>
		/// Asynchronously performs a cleanup operation on a Windows® image in a mounted .wim or .vhd file.
		/// </summary>
		/// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
		/// <param name="type">The <see cref="DismCleanImageType"/> operation to perform.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None" />.</param>
		/// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
		/// <exception cref="DismException">When a failure occurs.</exception>
		/// <exception cref="OperationCanceledException">When the operation is canceled.</exception>
		public static Task CleanupImageAsync(DismSession session, DismCleanImageType type, CancellationToken cancellationToken = default)
		{
			return CleanupImageAsync(session, type, DismCleanImageFlags.None, cancellationToken);
		}

		/// <summary>
		/// Asynchronously performs a cleanup operation on a Windows® image in a mounted .wim or .vhd file.
		/// </summary>
		/// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
		/// <param name="type">The <see cref="DismCleanImageType"/> operation to perform.</param>
		/// <param name="flags">The <see cref="DismCleanImageFlags"/> options to apply. Only valid for Component cleanup.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None" />.</param>
		/// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
		/// <exception cref="DismException">When a failure occurs.</exception>
		/// <exception cref="OperationCanceledException">When the operation is canceled.</exception>
		public static Task CleanupImageAsync(DismSession session, DismCleanImageType type, DismCleanImageFlags flags, CancellationToken cancellationToken = default)
		{
			return CleanupImageAsync(session, type, flags, progress: null, cancellationToken);
		}

		/// <summary>
		/// Asynchronously performs a cleanup operation on a Windows® image in a mounted .wim or .vhd file.
		/// </summary>
		/// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
		/// <param name="type">The <see cref="DismCleanImageType"/> operation to perform.</param>
		/// <param name="flags">The <see cref="DismCleanImageFlags"/> options to apply. Only valid for Component cleanup.</param>
		/// <param name="progress">An optional <see cref="IProgress{T}" /> provider to receive progress updates.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None" />.</param>
		/// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
		/// <exception cref="DismException">When a failure occurs.</exception>
		/// <exception cref="OperationCanceledException">When the operation is canceled.</exception>
		public static Task CleanupImageAsync(DismSession session, DismCleanImageType type, DismCleanImageFlags flags, IProgress<DismProgress>? progress, CancellationToken cancellationToken = default)
		{
			return CleanupImageAsync(session, type, flags, progress, userData: null, cancellationToken);
		}

		/// <summary>
		/// Asynchronously performs a cleanup operation on a Windows® image in a mounted .wim or .vhd file.
		/// </summary>
		/// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
		/// <param name="type">The <see cref="DismCleanImageType"/> operation to perform.</param>
		/// <param name="flags">The <see cref="DismCleanImageFlags"/> options to apply. Only valid for Component cleanup.</param>
		/// <param name="progress">An optional <see cref="IProgress{T}" /> provider to receive progress updates.</param>
		/// <param name="userData">Optional user data to pass to the specified <see cref="IProgress{T}" />.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None" />.</param>
		/// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
		/// <exception cref="DismException">When a failure occurs.</exception>
		/// <exception cref="OperationCanceledException">When the operation is canceled.</exception>
		public static Task CleanupImageAsync(DismSession session, DismCleanImageType type, DismCleanImageFlags flags, IProgress<DismProgress>? progress, object? userData, CancellationToken cancellationToken = default)
		{
			return DismUtilities.RunAsync(
				static (state, progress) =>
				{
					CleanupImage(state.session, state.type, state.flags, progress);

					return true;
				},
				(session, type, flags),
				progress,
				userData,
				cancellationToken);
		}

		private static void CleanupImage(DismSession session, DismCleanImageType type, DismCleanImageFlags flags, DismProgress progress)
		{
			int hresult = NativeMethods.DismCleanImage(session, type, flags, progress.EventHandle, progress.DismProgressCallbackNative, IntPtr.Zero);

			DismUtilities.ThrowIfFail(hresult, session);
		}

		internal static partial class NativeMethods
		{
#if NET7_0_OR_GREATER
            [LibraryImport(DismDllName, EntryPoint = "_DismCleanImage")]
            public static partial
#else
			[DllImport(DismDllName, EntryPoint = "_DismCleanImage")]
			public static extern
#endif
			int DismCleanImage(
				DismSession session,
				DismCleanImageType type,
				DismCleanImageFlags flags,
				SafeWaitHandle cancelEvent,
				DismProgressCallbackNative progress,
				IntPtr userData);
		}
	}
}