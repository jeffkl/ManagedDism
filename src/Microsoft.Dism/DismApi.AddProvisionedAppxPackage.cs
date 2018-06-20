// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Dism
{
    public static partial class DismApi
    {
        /// <summary>
        /// Adds an app package (.appx) that will install for each new user to a Windows image.
        /// </summary>
        /// <param name="session">A valid DISM Session.</param>
        /// <param name="appPath">Specifies the location of the app package (.appx) to add to the Windows image.</param>
        /// <param name="dependencyPackages">Specifies the location of dependency packages.</param>
        /// <param name="licensePath">Specifies the location of the .xml file containing your application license.</param>
        /// <param name="customDataPath">Specifies the location of a custom data file. The custom data file will be renamed custom.data and saved in the app data store.</param>
        /// <exception cref="DismRebootRequiredException">When the operation requires a reboot to complete.</exception>
        public static void AddProvisionedAppxPackage(DismSession session, string appPath, List<string> dependencyPackages, string licensePath, string customDataPath)
        {
            string[] dependencyPackagesArray = dependencyPackages?.ToArray() ?? new string[0];

            bool skipLicense = String.IsNullOrEmpty(licensePath);

            int hresult = NativeMethods._DismAddProvisionedAppxPackage(session, appPath, dependencyPackagesArray, (uint)dependencyPackagesArray.Length, licensePath, skipLicense, customDataPath);

            DismUtilities.ThrowIfFail(hresult, session);
        }

        internal static partial class NativeMethods
        {
            /// <summary>
            /// Gets a provisioned appx package.
            /// </summary>
            /// <param name="session">A valid DISM Session.</param>
            /// <param name="appPath">The application path.</param>
            /// <param name="dependencyPackages">The dependent packages.</param>
            /// <param name="dependencyPackageCount">The dependent package count.</param>
            /// <param name="licensePath">The license path.</param>
            /// <param name="skipLicense">Specifies whether the license should be skipped.</param>
            /// <param name="customDataPath">A custom path.</param>
            /// <returns>Returns S_OK on success.</returns>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int _DismAddProvisionedAppxPackage(DismSession session, [MarshalAs(UnmanagedType.LPWStr)] string appPath, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 3)] string[] dependencyPackages, UInt32 dependencyPackageCount, [MarshalAs(UnmanagedType.LPWStr)] string licensePath, bool skipLicense, [MarshalAs(UnmanagedType.LPWStr)] string customDataPath);
        }
    }
}