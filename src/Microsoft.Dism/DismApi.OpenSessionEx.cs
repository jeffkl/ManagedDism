// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace Microsoft.Dism
{
    public static partial class DismApi
    {
#pragma warning disable RS0026 // Do not add multiple public overloads with optional parameters
        /// <summary>
        /// Associates an offline Windows image with a DISMSession.
        /// </summary>
        /// <param name="imagePath">An absolute or relative path to the root directory of an offline Windows image or an absolute or relative path to the root directory of a mounted Windows image.</param>
        /// <param name="options">A <see cref="DismSessionOptions"/> object that contains the options for the session.</param>
        /// <returns>A <see cref="DismSession" /> object.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismSession OpenOfflineSessionEx(string imagePath, DismSessionOptions options = null)
        {
            return OpenOfflineSessionEx(imagePath, null, null, options);
        }

        /// <summary>
        /// Associates an offline Windows image with a DISMSession.
        /// </summary>
        /// <param name="imagePath">An absolute or relative path to the root directory of an offline Windows image or an absolute or relative path to the root directory of a mounted Windows image.</param>
        /// <param name="windowsDirectory">A relative or absolute path to the Windows directory. The path is relative to the mount point.</param>
        /// <param name="systemDrive">The letter of the system drive that contains the boot manager. If SystemDrive is NULL, the default value of the drive containing the mount point is used.</param>
        /// <param name="options">A <see cref="DismSessionOptions"/> object that contains the options for the session.</param>
        /// <returns>A <see cref="DismSession" /> object.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismSession OpenOfflineSessionEx(string imagePath, string windowsDirectory, string systemDrive, DismSessionOptions options = null)
        {
            return OpenSession(imagePath, windowsDirectory, systemDrive, options);
        }
#pragma warning restore RS0026 // Do not add multiple public overloads with optional parameters

        /// <summary>
        /// Associates an online Windows image with a DISMSession.
        /// </summary>
        /// <param name="options">A <see cref="DismSessionOptions"/> object that contains the options for the session.</param>
        /// <returns>A <see cref="DismSession" /> object.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismSession OpenOnlineSessionEx(DismSessionOptions options = null)
        {
            return OpenSession(DISM_ONLINE_IMAGE, null, null, options);
        }
    }
}