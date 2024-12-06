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
        /// Gets the registry mount point for the given <see cref="DismRegistryHive"/>.
        /// </summary>
        /// <param name="session">A valid offline DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
        /// <param name="registryHive">The <see cref="DismRegistryHive"/> to get the mount point for.</param>
        /// <returns>The registry mount point for the given <see cref="DismRegistryHive"/>.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static string GetRegistryMountPoint(DismSession session, DismRegistryHive registryHive)
        {
            int hresult = NativeMethods.DismGetRegistryMountPoint(session, registryHive, out IntPtr registryMountPointPtr);

            try
            {
                DismUtilities.ThrowIfFail(hresult);

                string? mountPoint = registryMountPointPtr.ToStructure<DismString>();

                return mountPoint!;
            }
            finally
            {
                // Clean up
                Delete(registryMountPointPtr);
            }
        }

        internal static partial class NativeMethods
        {
            /// <summary>
            /// Returns an array of DismMountedImageInfo Structure elements that describe the images that are mounted currently.
            /// </summary>
            /// <param name="session">A valid offline DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
            /// <param name="registryHive">The <see cref="DismRegistryHive"/> to get the mount point for.</param>
            /// <param name="registryMountPointPtr">A pointer which will receive the <see cref="DismString"/> wrapped mount point.</param>
            /// <returns>Returns <c>S_OK</c> on success.</returns>
            [DllImport(DismDllName, EntryPoint = "_DismGetRegistryMountPoint")]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismGetRegistryMountPoint(DismSession session, DismRegistryHive registryHive, out IntPtr registryMountPointPtr);
        }
    }
}
