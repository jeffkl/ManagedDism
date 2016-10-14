using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable ArrangeStaticMemberQualifier
// ReSharper disable RedundantNameQualifier
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
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)"/> method.</param>
        /// <param name="capabilityName">The name of the capability that is being added.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void AddCapability(DismSession session, string capabilityName)
        {
            DismApi.AddCapability(session, capabilityName, false, null, null, null);
        }

        /// <summary>
        /// Add a capability to an image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)"/> method.</param>
        /// <param name="capabilityName">The name of the capability that is being added.</param>
        /// <param name="limitAccess">The flag indicates whether WU/WSUS should be contacted as a source location for downloading the payload of a capability. If payload of the capability to be added exists, the flag is ignored.</param>
        /// <param name="sourcePaths">A list of source locations. The function shall look up removed payload files from the locations specified in SourcePaths, and if not found, continue the search by contacting WU/WSUS depending on parameter LimitAccess.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void AddCapability(DismSession session, string capabilityName, bool limitAccess, List<string> sourcePaths)
        {
            DismApi.AddCapability(session, capabilityName, limitAccess, sourcePaths, null, null);
        }

        /// <summary>
        /// Add a capability to an image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)"/> method.</param>
        /// <param name="capabilityName">The name of the capability that is being added.</param>
        /// <param name="limitAccess">The flag indicates whether WU/WSUS should be contacted as a source location for downloading the payload of a capability. If payload of the capability to be added exists, the flag is ignored.</param>
        /// <param name="sourcePaths">A list of source locations. The function shall look up removed payload files from the locations specified in SourcePaths, and if not found, continue the search by contacting WU/WSUS depending on parameter LimitAccess.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <param name="userData">Optional user data to pass to the DismProgressCallback method.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void AddCapability(DismSession session, string capabilityName, bool limitAccess, List<string> sourcePaths, Microsoft.Dism.DismProgressCallback progressCallback, object userData)
        {
            // Get the list of source paths as an array
            string[] sourcePathsArray = sourcePaths?.ToArray() ?? new string[0];

            // Create a DismProgress object to wrap the callback and allow cancellation
            var progress = new DismProgress(progressCallback, userData);

            ThrowIfFail(() => NativeMethods.DismAddCapability(session, capabilityName, limitAccess, sourcePathsArray, (uint)sourcePathsArray.Length, progress.EventHandle, progress.DismProgressCallbackNative, IntPtr.Zero));
        }

        /// <summary>
        /// Adds a third party driver (.inf) to an offline Windows® image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)"/> method.</param>
        /// <param name="driverPath">A relative or absolute path to the driver .inf file.</param>
        /// <param name="forceUnsigned">Indicates whether to accept unsigned drivers to an x64-based image. Unsigned drivers will automatically be added to an x86-based image.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void AddDriver(DismSession session, string driverPath, bool forceUnsigned)
        {
            ThrowIfFail(() => NativeMethods.DismAddDriver(session, driverPath, forceUnsigned));
        }

        /// <summary>
        /// Adds a single .cab or .msu file to a Windows® image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the OpenSession method.</param>
        /// <param name="packagePath">A relative or absolute path to the .cab or .msu file being added, a folder with one or more packages, or a folder containing the expanded files of a single .cab file.</param>
        /// <param name="ignoreCheck">Specifies whether to ignore the internal applicability checks that are done when a package is added.</param>
        /// <param name="preventPending">Specifies whether to add a package if it has pending online actions.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void AddPackage(DismSession session, string packagePath, bool ignoreCheck, bool preventPending)
        {
            DismApi.AddPackage(session, packagePath, ignoreCheck, preventPending, null);
        }

        /// <summary>
        /// Adds a single .cab or .msu file to a Windows® image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the OpenSession method.</param>
        /// <param name="packagePath">A relative or absolute path to the .cab or .msu file being added, a folder with one or more packages, or a folder containing the expanded files of a single .cab file.</param>
        /// <param name="ignoreCheck">Specifies whether to ignore the internal applicability checks that are done when a package is added.</param>
        /// <param name="preventPending">Specifies whether to add a package if it has pending online actions.</param>
        /// <param name="progressCallback">A DismProgressCallback method to call when progress is made.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        public static void AddPackage(DismSession session, string packagePath, bool ignoreCheck, bool preventPending, Microsoft.Dism.DismProgressCallback progressCallback)
        {
            DismApi.AddPackage(session, packagePath, ignoreCheck, preventPending, progressCallback, null);
        }

        /// <summary>
        /// Adds a single .cab or .msu file to a Windows® image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the OpenSession method.</param>
        /// <param name="packagePath">A relative or absolute path to the .cab or .msu file being added, a folder with one or more packages, or a folder containing the expanded files of a single .cab file.</param>
        /// <param name="ignoreCheck">Specifies whether to ignore the internal applicability checks that are done when a package is added.</param>
        /// <param name="preventPending">Specifies whether to add a package if it has pending online actions.</param>
        /// <param name="progressCallback">A DismProgressCallback method to call when progress is made.</param>
        /// <param name="userData">Optional user data to pass to the DismProgressCallback method.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        public static void AddPackage(DismSession session, string packagePath, bool ignoreCheck, bool preventPending, Microsoft.Dism.DismProgressCallback progressCallback, object userData)
        {
            // Create a DismProgress object to wrap the callback and allow cancellation
            var progress = new DismProgress(progressCallback, userData);

            ThrowIfFail(() => NativeMethods.DismAddPackage(session, packagePath, ignoreCheck, preventPending, progress.EventHandle, progress.DismProgressCallbackNative, IntPtr.Zero));
        }

        /// <summary>
        /// Applies an unattended answer file to a Windows® image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="unattendFile">A relative or absolute path to the answer file that will be applied to the image.</param>
        /// <param name="singleSession">Specifies whether the packages that are listed in an answer file will be processed in a single session or in multiple sessions.</param>
        public static void ApplyUnattend(DismSession session, string unattendFile, bool singleSession)
        {
            ThrowIfFail(() => NativeMethods.DismApplyUnattend(session, unattendFile, singleSession));
        }

        /// <summary>
        /// Checks whether the image can be serviced or whether it is corrupted.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="scanImage">Specifies whether to scan the image or just check for flags from a previous scan.</param>
        /// <returns>A <see cref="DismImageHealthState"/> indicating the health state of the image.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismImageHealthState CheckImageHealth(DismSession session, bool scanImage)
        {
            return DismApi.CheckImageHealth(session, scanImage, null);
        }

        /// <summary>
        /// Checks whether the image can be serviced or whether it is corrupted.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="scanImage">Specifies whether to scan the image or just check for flags from a previous scan.</param>
        /// <param name="progressCallback">A DismProgressCallback method to call when progress is made.</param>
        /// <returns>A <see cref="DismImageHealthState"/> indicating the health state of the image.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        public static DismImageHealthState CheckImageHealth(DismSession session, bool scanImage, Microsoft.Dism.DismProgressCallback progressCallback)
        {
            return DismApi.CheckImageHealth(session, scanImage, progressCallback, null);
        }

        /// <summary>
        /// Checks whether the image can be serviced or whether it is corrupted.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="scanImage">Specifies whether to scan the image or just check for flags from a previous scan.</param>
        /// <param name="progressCallback">A DismProgressCallback method to call when progress is made.</param>
        /// <param name="userData">Optional user data to pass to the DismProgressCallback method.</param>
        /// <returns>A <see cref="DismImageHealthState"/> indicating the health state of the image.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        public static DismImageHealthState CheckImageHealth(DismSession session, bool scanImage, Microsoft.Dism.DismProgressCallback progressCallback, object userData)
        {
            // Stores the output of the native function
            DismImageHealthState imageHealthState = DismImageHealthState.Healthy;

            // Create a DismProgress object to wrap the callback and allow cancellation
            var progress = new DismProgress(progressCallback, userData);

            ThrowIfFail(() => NativeMethods.DismCheckImageHealth(session, scanImage, progress.EventHandle, progress.DismProgressCallbackNative, IntPtr.Zero, out imageHealthState));

            return imageHealthState;
        }

        /// <summary>
        /// Removes files and releases resources associated with corrupted or invalid mount paths.
        /// </summary>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void CleanupMountpoints()
        {
            ThrowIfFail(NativeMethods.DismCleanupMountpoints);
        }

        /// <summary>
        /// Closes a DISMSession created by <see cref="OpenOfflineSession(string)"/> method. This function does not unmount the image. To unmount the image, use the <see cref="UnmountImage(string, bool)"/> method once all sessions are closed.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)"/> or <see cref="OpenOnlineSession" />method.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void CloseSession(DismSession session)
        {
            ThrowIfFail(() => NativeMethods.DismCloseSession(session.DangerousGetHandle()));
        }

        /// <summary>
        /// Commits the changes made to a Windows® image in a mounted .wim or .vhd file.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)"/> method.</param>
        /// <param name="discardChanges">true or false to discard changes made to the image.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void CommitImage(DismSession session, bool discardChanges)
        {
            DismApi.CommitImage(session, discardChanges, null);
        }

        /// <summary>
        /// Commits the changes made to a Windows® image in a mounted .wim or .vhd file.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)"/> method.</param>
        /// <param name="discardChanges">true or false to discard changes made to the image.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        public static void CommitImage(DismSession session, bool discardChanges, Microsoft.Dism.DismProgressCallback progressCallback)
        {
            DismApi.CommitImage(session, discardChanges, progressCallback, null);
        }

        /// <summary>
        /// Commits the changes made to a Windows® image in a mounted .wim or .vhd file.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)"/> method.</param>
        /// <param name="discardChanges">true or false to discard changes made to the image.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <param name="userData">Optional user data to pass to the DismProgressCallback method.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        public static void CommitImage(DismSession session, bool discardChanges, Microsoft.Dism.DismProgressCallback progressCallback, object userData)
        {
            // Create the flags
            UInt32 flags = discardChanges ? DismApi.DISM_DISCARD_IMAGE : DismApi.DISM_COMMIT_MASK;

            // Create a DismProgress object to wrap the callback and allow cancellation
            var progress = new DismProgress(progressCallback, userData);

            ThrowIfFail(() => NativeMethods.DismCommitImage(session, flags, progress.EventHandle, progress.DismProgressCallbackNative, IntPtr.Zero));
        }

        /// <summary>
        /// Disables a feature in the current image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="featureName">The name of the feature that you want to disable. To disable more than one feature, separate each feature name with a semicolon.</param>
        /// <param name="packageName">Optional. The name of the parent package that the feature is a part of.
        ///
        /// This is an optional parameter. If no package is specified, then the default Windows® Foundation package is used.</param>
        /// <param name="removePayload">Specifies whether to remove the files required to enable the feature.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="DismRebootRequiredException">When the operation requires a reboot to complete.</exception>
        public static void DisableFeature(DismSession session, string featureName, string packageName, bool removePayload)
        {
            DismApi.DisableFeature(session, featureName, packageName, removePayload, null);
        }

        /// <summary>
        /// Disables a feature in the current image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="featureName">The name of the feature that you want to disable. To disable more than one feature, separate each feature name with a semicolon.</param>
        /// <param name="packageName">Optional. The name of the parent package that the feature is a part of.
        ///
        /// This is an optional parameter. If no package is specified, then the default Windows® Foundation package is used.</param>
        /// <param name="removePayload">Specifies whether to remove the files required to enable the feature.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        /// <exception cref="DismRebootRequiredException">When the operation requires a reboot to complete.</exception>
        public static void DisableFeature(DismSession session, string featureName, string packageName, bool removePayload, Microsoft.Dism.DismProgressCallback progressCallback)
        {
            DismApi.DisableFeature(session, featureName, packageName, removePayload, progressCallback, null);
        }

        /// <summary>
        /// Disables a feature in the current image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="featureName">The name of the feature that you want to disable. To disable more than one feature, separate each feature name with a semicolon.</param>
        /// <param name="packageName">Optional. The name of the parent package that the feature is a part of.
        ///
        /// This is an optional parameter. If no package is specified, then the default Windows® Foundation package is used.</param>
        /// <param name="removePayload">Specifies whether to remove the files required to enable the feature.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <param name="userData">Optional user data to pass to the DismProgressCallback method.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        /// <exception cref="DismRebootRequiredException">When the operation requires a reboot to complete.</exception>
        public static void DisableFeature(DismSession session, string featureName, string packageName, bool removePayload, Microsoft.Dism.DismProgressCallback progressCallback, object userData)
        {
            // Create a DismProgress object to wrap the callback and allow cancellation
            var progress = new DismProgress(progressCallback, userData);

            ThrowIfFail(() => NativeMethods.DismDisableFeature(session, featureName, packageName, removePayload, progress.EventHandle, progress.DismProgressCallbackNative, IntPtr.Zero), Win32Error.ERROR_SUCCESS, Win32Error.ERROR_SUCCESS_REBOOT_REQUIRED, Win32Error.ERROR_SUCCESS_RESTART_REQUIRED);
        }

        /// <summary>
        /// Enables a feature from the specified package name.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="featureName">The name of the feature that is being enabled. To enable more than one feature, separate each feature name with a semicolon.</param>
        /// <param name="packageName">The name of the package that contains the feature.</param>
        /// <param name="limitAccess">Specifies whether Windows Update (WU) should be contacted as a source location for downloading files if none are found in other specified locations. Before checking WU, DISM will check for the files in the SourcePaths provided and in any locations specified in the registry by group policy. If the files required to enable the feature are still present on the computer, this flag is ignored.</param>
        /// <param name="enableAll">Specifies whether to enable all dependencies of the feature. If the specified feature or any one of its dependencies cannot be enabled, none of them will be changed from their existing state.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="DismRebootRequiredException">When the operation requires a reboot to complete.</exception>
        public static void EnableFeatureByPackageName(DismSession session, string featureName, string packageName, bool limitAccess, bool enableAll)
        {
            DismApi.EnableFeatureByPackageName(session, featureName, packageName, limitAccess, enableAll, null, null);
        }

        /// <summary>
        /// Enables a feature from the specified package name.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="featureName">The name of the feature that is being enabled. To enable more than one feature, separate each feature name with a semicolon.</param>
        /// <param name="packageName">The name of the package that contains the feature.</param>
        /// <param name="limitAccess">Specifies whether Windows Update (WU) should be contacted as a source location for downloading files if none are found in other specified locations. Before checking WU, DISM will check for the files in the SourcePaths provided and in any locations specified in the registry by group policy. If the files required to enable the feature are still present on the computer, this flag is ignored.</param>
        /// <param name="enableAll">Specifies whether to enable all dependencies of the feature. If the specified feature or any one of its dependencies cannot be enabled, none of them will be changed from their existing state.</param>
        /// <param name="sourcePaths">A list of source locations to check for files needed to enable the feature.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="DismRebootRequiredException">When the operation requires a reboot to complete.</exception>
        public static void EnableFeatureByPackageName(DismSession session, string featureName, string packageName, bool limitAccess, bool enableAll, List<string> sourcePaths)
        {
            DismApi.EnableFeatureByPackageName(session, featureName, packageName, limitAccess, enableAll, sourcePaths, null);
        }

        /// <summary>
        /// Enables a feature from the specified package name.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="featureName">The name of the feature that is being enabled. To enable more than one feature, separate each feature name with a semicolon.</param>
        /// <param name="packageName">The name of the package that contains the feature.</param>
        /// <param name="limitAccess">Specifies whether Windows Update (WU) should be contacted as a source location for downloading files if none are found in other specified locations. Before checking WU, DISM will check for the files in the SourcePaths provided and in any locations specified in the registry by group policy. If the files required to enable the feature are still present on the computer, this flag is ignored.</param>
        /// <param name="enableAll">Specifies whether to enable all dependencies of the feature. If the specified feature or any one of its dependencies cannot be enabled, none of them will be changed from their existing state.</param>
        /// <param name="sourcePaths">A list of source locations to check for files needed to enable the feature.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        /// <exception cref="DismRebootRequiredException">When the operation requires a reboot to complete.</exception>
        public static void EnableFeatureByPackageName(DismSession session, string featureName, string packageName, bool limitAccess, bool enableAll, List<string> sourcePaths, Microsoft.Dism.DismProgressCallback progressCallback)
        {
            DismApi.EnableFeatureByPackageName(session, featureName, packageName, limitAccess, enableAll, sourcePaths, progressCallback, null);
        }

        /// <summary>
        /// Enables a feature from the specified package name.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="featureName">The name of the feature that is being enabled. To enable more than one feature, separate each feature name with a semicolon.</param>
        /// <param name="packageName">The name of the package that contains the feature.</param>
        /// <param name="limitAccess">Specifies whether Windows Update (WU) should be contacted as a source location for downloading files if none are found in other specified locations. Before checking WU, DISM will check for the files in the SourcePaths provided and in any locations specified in the registry by group policy. If the files required to enable the feature are still present on the computer, this flag is ignored.</param>
        /// <param name="enableAll">Specifies whether to enable all dependencies of the feature. If the specified feature or any one of its dependencies cannot be enabled, none of them will be changed from their existing state.</param>
        /// <param name="sourcePaths">A list of source locations to check for files needed to enable the feature.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <param name="userData">Optional user data to pass to the DismProgressCallback method.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        /// <exception cref="DismRebootRequiredException">When the operation requires a reboot to complete.</exception>
        public static void EnableFeatureByPackageName(DismSession session, string featureName, string packageName, bool limitAccess, bool enableAll, List<string> sourcePaths, Microsoft.Dism.DismProgressCallback progressCallback, object userData)
        {
            DismApi.EnableFeature(session, featureName, packageName, DismPackageIdentifier.Name, limitAccess, enableAll, sourcePaths, progressCallback, userData);
        }

        /// <summary>
        /// Enables a feature from the specified package path.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="featureName">The name of the feature that is being enabled. To enable more than one feature, separate each feature name with a semicolon.</param>
        /// <param name="packagePath">The path of the package that contains the feature.</param>
        /// <param name="limitAccess">Specifies whether Windows Update (WU) should be contacted as a source location for downloading files if none are found in other specified locations. Before checking WU, DISM will check for the files in the SourcePaths provided and in any locations specified in the registry by group policy. If the files required to enable the feature are still present on the computer, this flag is ignored.</param>
        /// <param name="enableAll">Specifies whether to enable all dependencies of the feature. If the specified feature or any one of its dependencies cannot be enabled, none of them will be changed from their existing state.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="DismRebootRequiredException">When the operation requires a reboot to complete.</exception>
        public static void EnableFeatureByPackagePath(DismSession session, string featureName, string packagePath, bool limitAccess, bool enableAll)
        {
            DismApi.EnableFeatureByPackagePath(session, featureName, packagePath, limitAccess, enableAll, null, null);
        }

        /// <summary>
        /// Enables a feature from the specified package path.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="featureName">The name of the feature that is being enabled. To enable more than one feature, separate each feature name with a semicolon.</param>
        /// <param name="packagePath">The path of the package that contains the feature.</param>
        /// <param name="limitAccess">Specifies whether Windows Update (WU) should be contacted as a source location for downloading files if none are found in other specified locations. Before checking WU, DISM will check for the files in the SourcePaths provided and in any locations specified in the registry by group policy. If the files required to enable the feature are still present on the computer, this flag is ignored.</param>
        /// <param name="enableAll">Specifies whether to enable all dependencies of the feature. If the specified feature or any one of its dependencies cannot be enabled, none of them will be changed from their existing state.</param>
        /// <param name="sourcePaths">A list of source locations to check for files needed to enable the feature.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void EnableFeatureByPackagePath(DismSession session, string featureName, string packagePath, bool limitAccess, bool enableAll, List<string> sourcePaths)
        {
            DismApi.EnableFeatureByPackagePath(session, featureName, packagePath, limitAccess, enableAll, sourcePaths, null);
        }

        /// <summary>
        /// Enables a feature from the specified package path.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="featureName">The name of the feature that is being enabled. To enable more than one feature, separate each feature name with a semicolon.</param>
        /// <param name="packagePath">The path of the package that contains the feature.</param>
        /// <param name="limitAccess">Specifies whether Windows Update (WU) should be contacted as a source location for downloading files if none are found in other specified locations. Before checking WU, DISM will check for the files in the SourcePaths provided and in any locations specified in the registry by group policy. If the files required to enable the feature are still present on the computer, this flag is ignored.</param>
        /// <param name="enableAll">Specifies whether to enable all dependencies of the feature. If the specified feature or any one of its dependencies cannot be enabled, none of them will be changed from their existing state.</param>
        /// <param name="sourcePaths">A list of source locations to check for files needed to enable the feature.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        /// <exception cref="DismRebootRequiredException">When the operation requires a reboot to complete.</exception>
        public static void EnableFeatureByPackagePath(DismSession session, string featureName, string packagePath, bool limitAccess, bool enableAll, List<string> sourcePaths, Microsoft.Dism.DismProgressCallback progressCallback)
        {
            DismApi.EnableFeatureByPackagePath(session, featureName, packagePath, limitAccess, enableAll, sourcePaths, progressCallback, null);
        }

        /// <summary>
        /// Enables a feature from the specified package path.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="featureName">The name of the feature that is being enabled. To enable more than one feature, separate each feature name with a semicolon.</param>
        /// <param name="packagePath">The path of the package that contains the feature.</param>
        /// <param name="limitAccess">Specifies whether Windows Update (WU) should be contacted as a source location for downloading files if none are found in other specified locations. Before checking WU, DISM will check for the files in the SourcePaths provided and in any locations specified in the registry by group policy. If the files required to enable the feature are still present on the computer, this flag is ignored.</param>
        /// <param name="enableAll">Specifies whether to enable all dependencies of the feature. If the specified feature or any one of its dependencies cannot be enabled, none of them will be changed from their existing state.</param>
        /// <param name="sourcePaths">A list of source locations to check for files needed to enable the feature.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <param name="userData">Optional user data to pass to the DismProgressCallback method.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        /// <exception cref="DismRebootRequiredException">When the operation requires a reboot to complete.</exception>
        public static void EnableFeatureByPackagePath(DismSession session, string featureName, string packagePath, bool limitAccess, bool enableAll, List<string> sourcePaths, Microsoft.Dism.DismProgressCallback progressCallback, object userData)
        {
            DismApi.EnableFeature(session, featureName, packagePath, DismPackageIdentifier.Path, limitAccess, enableAll, sourcePaths, progressCallback, userData);
        }

        /// <summary>
        /// Gets DISM capabilities.
        /// </summary>
        /// <param name="session">A valid DismSession. The DismSession must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)"/> method.</param>
        /// <returns>A <see cref="DismCapabilityCollection"/> object containing a collection of <see cref="DismCapability"/> objects.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismCapabilityCollection GetCapabilities(DismSession session)
        {
            DismCapabilityCollection capabilities = new DismCapabilityCollection();

            // Used for the native call
            IntPtr capabilityPtr = IntPtr.Zero;
            UInt32 capabilityCount = 0;

            try
            {
                ThrowIfFail(() => NativeMethods.DismGetCapabilities(session, out capabilityPtr, out capabilityCount));

                // Add the items
                capabilities.AddRange<DismCapability_>(capabilityPtr, (int)capabilityCount, c => new DismCapability(c));
            }
            finally
            {
                DismApi.Delete(capabilityPtr);
            }

            return capabilities;
        }

        /// <summary>
        /// Gets DISM capability info.
        /// </summary>
        /// <param name="session">A valid DismSession. The DismSession must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)"/> method.</param>
        /// <param name="capabilityName">The name of the specified capability.</param>
        /// <returns>A <see cref="DismCapabilityInfo"/> object.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismCapabilityInfo GetCapabilityInfo(DismSession session, string capabilityName)
        {
            // Stores the output from DismGetCapabilityInfo
            IntPtr capabilityInfoPtr = IntPtr.Zero;

            try
            {
                ThrowIfFail(() => NativeMethods.DismGetCapabilityInfo(session, capabilityName, out capabilityInfoPtr));

                // Return a new DismCapabilityInfo from the native pointer
                return new DismCapabilityInfo(capabilityInfoPtr);
            }
            finally
            {
                // Clean up the native pointer
                DismApi.Delete(capabilityInfoPtr);
            }
        }

        /// <summary>
        /// Gets information about an .inf file in a specified image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)"/> method.</param>
        /// <param name="driverPath">A relative or absolute path to the driver .inf file.</param>
        /// <returns>A <see cref="DismDriverCollection"/> object containing a collection of <see cref="DismDriver"/> objects.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismDriverCollection GetDriverInfo(DismSession session, string driverPath)
        {
            var driverInfos = new DismDriverCollection();

            // Used for the native call
            IntPtr driverInfoPtr = IntPtr.Zero;
            UInt32 driverInfoCount = 0;
            IntPtr driverPackagePtr = IntPtr.Zero;

            try
            {
                ThrowIfFail(() => NativeMethods.DismGetDriverInfo(session, driverPath, out driverInfoPtr, out driverInfoCount, out driverPackagePtr));

                // Add the items
                driverInfos.AddRange<DismApi.DismDriver_>(driverInfoPtr, (int)driverInfoCount, i => new DismDriver(i));
            }
            finally
            {
                // Clean up
                DismApi.Delete(driverInfoPtr);
                DismApi.Delete(driverPackagePtr);
            }

            return driverInfos;
        }

        /// <summary>
        /// Gets the drivers in an image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)"/> method.</param>
        /// <param name="allDrivers">true or false to specify to retrieve all drivers or just out-of-box drivers.</param>
        /// <returns>A <see cref="DismDriverPackageCollection"/> object containing a collection of <see cref="DismDriverPackage"/> objects.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismDriverPackageCollection GetDrivers(DismSession session, bool allDrivers)
        {
            var driverPackages = new DismDriverPackageCollection();

            // Used for the native call
            IntPtr driverPackagePtr = IntPtr.Zero;
            UInt32 driverPackageCount = 0;

            try
            {
                ThrowIfFail(() => NativeMethods.DismGetDrivers(session, allDrivers, out driverPackagePtr, out driverPackageCount));

                // Add the items
                driverPackages.AddRange<DismApi.DismDriverPackage_>(driverPackagePtr, (int)driverPackageCount, i => new DismDriverPackage(i));
            }
            finally
            {
                // Clean up
                DismApi.Delete(driverPackagePtr);
            }

            return driverPackages;
        }

        /// <summary>
        /// Gets detailed information for the specified feature.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="featureName">The name of the feature that you want to get more information about.</param>
        /// <returns>A <see cref="DismFeatureInfo"/> object.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismFeatureInfo GetFeatureInfo(DismSession session, string featureName)
        {
            return DismApi.GetFeatureInfo(session, featureName, "", DismPackageIdentifier.None);
        }

        /// <summary>
        /// Gets detailed information for the specified feature.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="featureName">The name of the feature that you want to get more information about.</param>
        /// <param name="packageName">The package name.</param>
        /// <returns>A <see cref="DismFeatureInfo"/> object.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismFeatureInfo GetFeatureInfoByPackageName(DismSession session, string featureName, string packageName)
        {
            return DismApi.GetFeatureInfo(session, featureName, packageName, DismPackageIdentifier.Name);
        }

        /// <summary>
        /// Gets detailed information for the specified feature.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="featureName">The name of the feature that you want to get more information about.</param>
        /// <param name="packagePath">An absolute path to a package.</param>
        /// <returns>A <see cref="DismFeatureInfo"/> object.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismFeatureInfo GetFeatureInfoByPackagePath(DismSession session, string featureName, string packagePath)
        {
            return DismApi.GetFeatureInfo(session, featureName, packagePath, DismPackageIdentifier.Path);
        }

        /// <summary>
        /// Gets the parent features of a specified feature.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the OpenSession Function.</param>
        /// <param name="featureName">The name of the feature that you want to find the parent of.</param>
        /// <param name="packageName">The name of the package that contains the feature.</param>
        /// <returns>A <see cref="DismFeatureCollection"/> object containing a collection of <see cref="DismFeature"/> objects.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismFeatureCollection GetFeatureParentByName(DismSession session, string featureName, string packageName)
        {
            return DismApi.GetFeatureParent(session, featureName, packageName, DismPackageIdentifier.Name);
        }

        /// <summary>
        /// Gets the parent features of a specified feature.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the OpenSession Function.</param>
        /// <param name="featureName">The name of the feature that you want to find the parent of.</param>
        /// <param name="packagePath">An absolute path to a .cab file.</param>
        /// <returns>A <see cref="DismFeatureCollection"/> object containing a collection of <see cref="DismFeature"/> objects.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismFeatureCollection GetFeatureParentByPath(DismSession session, string featureName, string packagePath)
        {
            return DismApi.GetFeatureParent(session, featureName, packagePath, DismPackageIdentifier.Path);
        }

        /// <summary>
        /// Gets all the features in an image, regardless of whether the features are enabled or disabled.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <returns>A <see cref="DismFeatureCollection"/> object containing a collection of <see cref="DismFeature"/> objects.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismFeatureCollection GetFeatures(DismSession session)
        {
            return DismApi.GetFeatures(session, String.Empty, DismPackageIdentifier.None);
        }

        /// <summary>
        /// Gets all the features in an image, regardless of whether the features are enabled or disabled.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="packageName">The name of the package to get features of.</param>
        /// <returns>A <see cref="DismFeatureCollection"/> object containing a collection of <see cref="DismFeature"/> objects.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismFeatureCollection GetFeaturesByPackageName(DismSession session, string packageName)
        {
            return DismApi.GetFeatures(session, packageName, DismPackageIdentifier.Name);
        }

        /// <summary>
        /// Gets all the features in an image, regardless of whether the features are enabled or disabled.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// /// <param name="packagePath">The path of the package to get features of.</param>
        /// <returns>A <see cref="DismFeatureCollection"/> object containing a collection of <see cref="DismFeature"/> objects.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismFeatureCollection GetFeaturesByPackagePath(DismSession session, string packagePath)
        {
            return DismApi.GetFeatures(session, packagePath, DismPackageIdentifier.Path);
        }

        /// <summary>
        /// Gets a collection of images contained in the specified .wim or .vhd file.
        /// </summary>
        /// <param name="imageFilePath">// Clean up</param>
        /// <returns>A <see cref="DismImageInfoCollection"/> object containing a collection of <see cref="DismImageInfo"/> objects.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismImageInfoCollection GetImageInfo(string imageFilePath)
        {
            var imageInfos = new DismImageInfoCollection();

            // Used for the native call
            IntPtr imageInfoPtr = IntPtr.Zero;
            UInt32 imageInfoCount = 0;

            try
            {
                ThrowIfFail(() => NativeMethods.DismGetImageInfo(imageFilePath, out imageInfoPtr, out imageInfoCount));

                // Add the items
                imageInfos.AddRange<DismApi.DismImageInfo_>(imageInfoPtr, (int)imageInfoCount, i => new DismImageInfo(i));
            }
            finally
            {
                // Clean up
                DismApi.Delete(imageInfoPtr);
            }

            return imageInfos;
        }

        /// <summary>
        /// Gets the error message in the current thread immediately after a failure.
        /// </summary>
        /// <returns>An error message if one is found, otherwise null.</returns>
        public static string GetLastErrorMessage()
        {
            // Allow this method to be overridden by an internal test hook
            //
            if (GetLastErrorMessageTestHook != null)
            {
                return GetLastErrorMessageTestHook();
            }

            // Used for the native call
            IntPtr errorMessagePtr = IntPtr.Zero;

            try
            {
                if (NativeMethods.DismGetLastErrorMessage(out errorMessagePtr) != Win32Error.ERROR_SUCCESS)
                {
                    return null;
                }

                // Get a string from the pointer
                string dismString = errorMessagePtr.ToStructure<DismApi.DismString>();

                // See if the string has a value
                if (String.IsNullOrEmpty(dismString) == false)
                {
                    // Return the trimmed value
                    return dismString.Trim();
                }
            }
            finally
            {
                // Clean up
                DismApi.Delete(errorMessagePtr);
            }

            // No error message was found
            return null;
        }

        /// <summary>
        /// Gets a list of images that are currently mounted.
        /// </summary>
        /// <returns>A <see cref="DismMountedImageInfoCollection"/> object containing a collection of <see cref="DismMountedImageInfo"/> objects.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismMountedImageInfoCollection GetMountedImages()
        {
            var mountedImageInfos = new DismMountedImageInfoCollection();

            // Used for the native call
            IntPtr mountedImageInfoPtr = IntPtr.Zero;
            UInt32 mountedImageInfoCount = 0;

            try
            {
                ThrowIfFail(() => NativeMethods.DismGetMountedImageInfo(out mountedImageInfoPtr, out mountedImageInfoCount));

                // Add the items
                mountedImageInfos.AddRange<DismApi.DismMountedImageInfo_>(mountedImageInfoPtr, (int)mountedImageInfoCount, i => new DismMountedImageInfo(i));
            }
            finally
            {
                // Clean up
                DismApi.Delete(mountedImageInfoPtr);
            }

            return mountedImageInfos;
        }

        /// <summary>
        /// Gets extended information about a package.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the OpenImageSession Function.</param>
        /// <param name="packageName">The name of the package to get information about.</param>
        /// <returns>A <see cref="DismPackageInfo"/> object.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismPackageInfo GetPackageInfoByName(DismSession session, string packageName)
        {
            return DismApi.GetPackageInfo(session, packageName, DismPackageIdentifier.Name);
        }

        /// <summary>
        /// Gets extended information about a package.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the OpenImageSession Function.</param>
        /// <param name="packagePath">An absolute path to a .cab file or to a folder containing an expanded package.</param>
        /// <returns>A <see cref="DismPackageInfo"/> object.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismPackageInfo GetPackageInfoByPath(DismSession session, string packagePath)
        {
            return DismApi.GetPackageInfo(session, packagePath, DismPackageIdentifier.Path);
        }

        /// <summary>
        /// Gets a collection of each package in an image and provides basic information about each package, such as the package name and type of package.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DismSession must be associated with an image.</param>
        /// <returns>A <see cref="DismPackageCollection"/> object containing a collection of <see cref="DismPackage"/>objects.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismPackageCollection GetPackages(DismSession session)
        {
            var packages = new DismPackageCollection();

            // Used for the native call
            IntPtr packagePtr = IntPtr.Zero;
            UInt32 packageCount = 0;

            try
            {
                ThrowIfFail(() => NativeMethods.DismGetPackages(session, out packagePtr, out packageCount));

                // Add the items
                packages.AddRange<DismApi.DismPackage_>(packagePtr, (int)packageCount, i => new DismPackage(i));
            }
            finally
            {
                // Clean up
                DismApi.Delete(packagePtr);
            }

            return packages;
        }

        /// <summary>
        /// Initializes DISM API. Initialize must be called once per process before calling any other DISM API functions.
        /// </summary>
        /// <param name="logLevel">Indicates the level of logging.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void Initialize(DismLogLevel logLevel)
        {
            DismApi.Initialize(logLevel, null);
        }

        /// <summary>
        /// Initializes DISM API. Initialize must be called once per process before calling any other DISM API functions.
        /// </summary>
        /// <param name="logLevel">Indicates the level of logging.</param>
        /// <param name="logFilePath">A relative or absolute path to a log file. All messages generated will be logged to this path. If NULL, the default log path, %windir%\Logs\DISM\dism.log, will be used.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void Initialize(DismLogLevel logLevel, string logFilePath)
        {
            DismApi.Initialize(logLevel, logFilePath, null);
        }

        /// <summary>
        /// Initializes DISM API. Initialize must be called once per process before calling any other DISM API functions.
        /// </summary>
        /// <param name="logLevel">Indicates the level of logging.</param>
        /// <param name="logFilePath">A relative or absolute path to a log file. All messages generated will be logged to this path. If NULL, the default log path, %windir%\Logs\DISM\dism.log, will be used.</param>
        /// <param name="scratchDirectory">A relative or absolute path to a scratch directory. DISM API will use this directory for internal operations. If null, the default temp directory, \Windows\%Temp%, will be used.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void Initialize(DismLogLevel logLevel, string logFilePath, string scratchDirectory)
        {
            ThrowIfFail(() => NativeMethods.DismInitialize(logLevel, logFilePath, scratchDirectory));
        }

        /// <summary>
        /// Mounts a WIM or VHD image file to a specified location.
        /// </summary>
        /// <param name="imageFilePath">The path to the WIM or VHD file on the local computer. A .wim, .vhd, or .vhdx file name extension is required.</param>
        /// <param name="mountPath">The path of the location where the image should be mounted. This mount path must already exist on the computer. The Windows image in a .wim, .vhd, or .vhdx file can be mounted to an empty folder on an NTFS formatted drive. A Windows image in a .vhd or .vhdx file can also be mounted to an unassigned drive letter. You cannot mount an image to the root of the existing drive.</param>
        /// <param name="imageIndex">The index of the image in the WIM file that you want to mount. For a VHD file, you must specify an index of 1.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void MountImage(string imageFilePath, string mountPath, int imageIndex)
        {
            DismApi.MountImage(imageFilePath, mountPath, imageIndex, false);
        }

        /// <summary>
        /// Mounts a WIM or VHD image file to a specified location.
        /// </summary>
        /// <param name="imageFilePath">The path to the WIM or VHD file on the local computer. A .wim, .vhd, or .vhdx file name extension is required.</param>
        /// <param name="mountPath">The path of the location where the image should be mounted. This mount path must already exist on the computer. The Windows image in a .wim, .vhd, or .vhdx file can be mounted to an empty folder on an NTFS formatted drive. A Windows image in a .vhd or .vhdx file can also be mounted to an unassigned drive letter. You cannot mount an image to the root of the existing drive.</param>
        /// <param name="imageIndex">The index of the image in the WIM file that you want to mount. For a VHD file, you must specify an index of 1.</param>
        /// <param name="readOnly">Specifies if the image should be mounted in read-only mode.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void MountImage(string imageFilePath, string mountPath, int imageIndex, bool readOnly)
        {
            DismApi.MountImage(imageFilePath, mountPath, imageIndex, readOnly, DismMountImageOptions.None);
        }

        /// <summary>
        /// Mounts a WIM or VHD image file to a specified location.
        /// </summary>
        /// <param name="imageFilePath">The path to the WIM or VHD file on the local computer. A .wim, .vhd, or .vhdx file name extension is required.</param>
        /// <param name="mountPath">The path of the location where the image should be mounted. This mount path must already exist on the computer. The Windows image in a .wim, .vhd, or .vhdx file can be mounted to an empty folder on an NTFS formatted drive. A Windows image in a .vhd or .vhdx file can also be mounted to an unassigned drive letter. You cannot mount an image to the root of the existing drive.</param>
        /// <param name="imageIndex">The index of the image in the WIM file that you want to mount. For a VHD file, you must specify an index of 1.</param>
        /// <param name="readOnly">Specifies if the image should be mounted in read-only mode.</param>
        /// <param name="options">Specifies options to use when mounting an image.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void MountImage(string imageFilePath, string mountPath, int imageIndex, bool readOnly, DismMountImageOptions options)
        {
            DismApi.MountImage(imageFilePath, mountPath, imageIndex, readOnly, options, null);
        }

        /// <summary>
        /// Mounts a WIM or VHD image file to a specified location.
        /// </summary>
        /// <param name="imageFilePath">The path to the WIM or VHD file on the local computer. A .wim, .vhd, or .vhdx file name extension is required.</param>
        /// <param name="mountPath">The path of the location where the image should be mounted. This mount path must already exist on the computer. The Windows image in a .wim, .vhd, or .vhdx file can be mounted to an empty folder on an NTFS formatted drive. A Windows image in a .vhd or .vhdx file can also be mounted to an unassigned drive letter. You cannot mount an image to the root of the existing drive.</param>
        /// <param name="imageIndex">The index of the image in the WIM file that you want to mount. For a VHD file, you must specify an index of 1.</param>
        /// <param name="readOnly">Specifies if the image should be mounted in read-only mode.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        public static void MountImage(string imageFilePath, string mountPath, int imageIndex, bool readOnly, Dism.DismProgressCallback progressCallback)
        {
            DismApi.MountImage(imageFilePath, mountPath, imageIndex, readOnly, DismMountImageOptions.None, progressCallback);
        }

        /// <summary>
        /// Mounts a WIM or VHD image file to a specified location.
        /// </summary>
        /// <param name="imageFilePath">The path to the WIM or VHD file on the local computer. A .wim, .vhd, or .vhdx file name extension is required.</param>
        /// <param name="mountPath">The path of the location where the image should be mounted. This mount path must already exist on the computer. The Windows image in a .wim, .vhd, or .vhdx file can be mounted to an empty folder on an NTFS formatted drive. A Windows image in a .vhd or .vhdx file can also be mounted to an unassigned drive letter. You cannot mount an image to the root of the existing drive.</param>
        /// <param name="imageIndex">The index of the image in the WIM file that you want to mount. For a VHD file, you must specify an index of 1.</param>
        /// <param name="readOnly">Specifies if the image should be mounted in read-only mode.</param>
        /// <param name="options">Specifies options to use when mounting an image.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        public static void MountImage(string imageFilePath, string mountPath, int imageIndex, bool readOnly, DismMountImageOptions options, Dism.DismProgressCallback progressCallback)
        {
            DismApi.MountImage(imageFilePath, mountPath, imageIndex, readOnly, options, progressCallback, null);
        }

        /// <summary>
        /// Mounts a WIM or VHD image file to a specified location.
        /// </summary>
        /// <param name="imageFilePath">The path to the WIM or VHD file on the local computer. A .wim, .vhd, or .vhdx file name extension is required.</param>
        /// <param name="mountPath">The path of the location where the image should be mounted. This mount path must already exist on the computer. The Windows image in a .wim, .vhd, or .vhdx file can be mounted to an empty folder on an NTFS formatted drive. A Windows image in a .vhd or .vhdx file can also be mounted to an unassigned drive letter. You cannot mount an image to the root of the existing drive.</param>
        /// <param name="imageIndex">The index of the image in the WIM file that you want to mount. For a VHD file, you must specify an index of 1.</param>
        /// <param name="readOnly">Specifies if the image should be mounted in read-only mode.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <param name="userData">Optional user data to pass to the DismProgressCallback method.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        public static void MountImage(string imageFilePath, string mountPath, int imageIndex, bool readOnly, Dism.DismProgressCallback progressCallback, object userData)
        {
            DismApi.MountImage(imageFilePath, mountPath, imageIndex, readOnly, DismMountImageOptions.None, progressCallback, userData);
        }

        /// <summary>
        /// Mounts a WIM or VHD image file to a specified location.
        /// </summary>
        /// <param name="imageFilePath">The path to the WIM or VHD file on the local computer. A .wim, .vhd, or .vhdx file name extension is required.</param>
        /// <param name="mountPath">The path of the location where the image should be mounted. This mount path must already exist on the computer. The Windows image in a .wim, .vhd, or .vhdx file can be mounted to an empty folder on an NTFS formatted drive. A Windows image in a .vhd or .vhdx file can also be mounted to an unassigned drive letter. You cannot mount an image to the root of the existing drive.</param>
        /// <param name="imageIndex">The index of the image in the WIM file that you want to mount. For a VHD file, you must specify an index of 1.</param>
        /// <param name="readOnly">Specifies if the image should be mounted in read-only mode.</param>
        /// <param name="options">Specifies options to use when mounting an image.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <param name="userData">Optional user data to pass to the DismProgressCallback method.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        public static void MountImage(string imageFilePath, string mountPath, int imageIndex, bool readOnly, DismMountImageOptions options, Dism.DismProgressCallback progressCallback, object userData)
        {
            DismApi.MountImage(imageFilePath, mountPath, imageIndex, null, DismImageIdentifier.ImageIndex, readOnly, options, progressCallback, userData);
        }

        /// <summary>
        /// Mounts a WIM or VHD image file to a specified location.
        /// </summary>
        /// <param name="imageFilePath">The path to the WIM or VHD file on the local computer. A .wim, .vhd, or .vhdx file name extension is required.</param>
        /// <param name="mountPath">The path of the location where the image should be mounted. This mount path must already exist on the computer. The Windows image in a .wim, .vhd, or .vhdx file can be mounted to an empty folder on an NTFS formatted drive. A Windows image in a .vhd or .vhdx file can also be mounted to an unassigned drive letter. You cannot mount an image to the root of the existing drive.</param>
        /// <param name="imageName">The name of the image that you want to mount.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void MountImage(string imageFilePath, string mountPath, string imageName)
        {
            DismApi.MountImage(imageFilePath, mountPath, imageName, false);
        }

        /// <summary>
        /// Mounts a WIM or VHD image file to a specified location.
        /// </summary>
        /// <param name="imageFilePath">The path to the WIM or VHD file on the local computer. A .wim, .vhd, or .vhdx file name extension is required.</param>
        /// <param name="mountPath">The path of the location where the image should be mounted. This mount path must already exist on the computer. The Windows image in a .wim, .vhd, or .vhdx file can be mounted to an empty folder on an NTFS formatted drive. A Windows image in a .vhd or .vhdx file can also be mounted to an unassigned drive letter. You cannot mount an image to the root of the existing drive.</param>
        /// <param name="imageName">The name of the image that you want to mount.</param>
        /// <param name="readOnly">Specifies if the image should be mounted in read-only mode.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void MountImage(string imageFilePath, string mountPath, string imageName, bool readOnly)
        {
            DismApi.MountImage(imageFilePath, mountPath, imageName, readOnly, DismMountImageOptions.None);
        }

        /// <summary>
        /// Mounts a WIM or VHD image file to a specified location.
        /// </summary>
        /// <param name="imageFilePath">The path to the WIM or VHD file on the local computer. A .wim, .vhd, or .vhdx file name extension is required.</param>
        /// <param name="mountPath">The path of the location where the image should be mounted. This mount path must already exist on the computer. The Windows image in a .wim, .vhd, or .vhdx file can be mounted to an empty folder on an NTFS formatted drive. A Windows image in a .vhd or .vhdx file can also be mounted to an unassigned drive letter. You cannot mount an image to the root of the existing drive.</param>
        /// <param name="imageName">The name of the image that you want to mount.</param>
        /// <param name="readOnly">Specifies if the image should be mounted in read-only mode.</param>
        /// <param name="options">Specifies options to use when mounting an image.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void MountImage(string imageFilePath, string mountPath, string imageName, bool readOnly, DismMountImageOptions options)
        {
            DismApi.MountImage(imageFilePath, mountPath, imageName, readOnly, options, null);
        }

        /// <summary>
        /// Mounts a WIM or VHD image file to a specified location.
        /// </summary>
        /// <param name="imageFilePath">The path to the WIM or VHD file on the local computer. A .wim, .vhd, or .vhdx file name extension is required.</param>
        /// <param name="mountPath">The path of the location where the image should be mounted. This mount path must already exist on the computer. The Windows image in a .wim, .vhd, or .vhdx file can be mounted to an empty folder on an NTFS formatted drive. A Windows image in a .vhd or .vhdx file can also be mounted to an unassigned drive letter. You cannot mount an image to the root of the existing drive.</param>
        /// <param name="imageName">The name of the image that you want to mount.</param>
        /// <param name="readOnly">Specifies if the image should be mounted in read-only mode.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        public static void MountImage(string imageFilePath, string mountPath, string imageName, bool readOnly, Dism.DismProgressCallback progressCallback)
        {
            DismApi.MountImage(imageFilePath, mountPath, imageName, readOnly, DismMountImageOptions.None, progressCallback);
        }

        /// <summary>
        /// Mounts a WIM or VHD image file to a specified location.
        /// </summary>
        /// <param name="imageFilePath">The path to the WIM or VHD file on the local computer. A .wim, .vhd, or .vhdx file name extension is required.</param>
        /// <param name="mountPath">The path of the location where the image should be mounted. This mount path must already exist on the computer. The Windows image in a .wim, .vhd, or .vhdx file can be mounted to an empty folder on an NTFS formatted drive. A Windows image in a .vhd or .vhdx file can also be mounted to an unassigned drive letter. You cannot mount an image to the root of the existing drive.</param>
        /// <param name="imageName">The name of the image that you want to mount.</param>
        /// <param name="readOnly">Specifies if the image should be mounted in read-only mode.</param>
        /// <param name="options">Specifies options to use when mounting an image.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        public static void MountImage(string imageFilePath, string mountPath, string imageName, bool readOnly, DismMountImageOptions options, Dism.DismProgressCallback progressCallback)
        {
            DismApi.MountImage(imageFilePath, mountPath, imageName, readOnly, options, progressCallback, null);
        }

        /// <summary>
        /// Mounts a WIM or VHD image file to a specified location.
        /// </summary>
        /// <param name="imageFilePath">The path to the WIM or VHD file on the local computer. A .wim, .vhd, or .vhdx file name extension is required.</param>
        /// <param name="mountPath">The path of the location where the image should be mounted. This mount path must already exist on the computer. The Windows image in a .wim, .vhd, or .vhdx file can be mounted to an empty folder on an NTFS formatted drive. A Windows image in a .vhd or .vhdx file can also be mounted to an unassigned drive letter. You cannot mount an image to the root of the existing drive.</param>
        /// <param name="imageName">The name of the image that you want to mount.</param>
        /// <param name="readOnly">Specifies if the image should be mounted in read-only mode.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <param name="userData">Optional user data to pass to the DismProgressCallback method.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        public static void MountImage(string imageFilePath, string mountPath, string imageName, bool readOnly, Dism.DismProgressCallback progressCallback, object userData)
        {
            DismApi.MountImage(imageFilePath, mountPath, imageName, readOnly, DismMountImageOptions.None, progressCallback, userData);
        }

        /// <summary>
        /// Mounts a WIM or VHD image file to a specified location.
        /// </summary>
        /// <param name="imageFilePath">The path to the WIM or VHD file on the local computer. A .wim, .vhd, or .vhdx file name extension is required.</param>
        /// <param name="mountPath">The path of the location where the image should be mounted. This mount path must already exist on the computer. The Windows image in a .wim, .vhd, or .vhdx file can be mounted to an empty folder on an NTFS formatted drive. A Windows image in a .vhd or .vhdx file can also be mounted to an unassigned drive letter. You cannot mount an image to the root of the existing drive.</param>
        /// <param name="imageName">The name of the image that you want to mount.</param>
        /// <param name="readOnly">Specifies if the image should be mounted in read-only mode.</param>
        /// <param name="options">Specifies options to use when mounting an image.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <param name="userData">Optional user data to pass to the DismProgressCallback method.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        public static void MountImage(string imageFilePath, string mountPath, string imageName, bool readOnly, DismMountImageOptions options, Dism.DismProgressCallback progressCallback, object userData)
        {
            DismApi.MountImage(imageFilePath, mountPath, 0, imageName, DismImageIdentifier.ImageName, readOnly, options, progressCallback, userData);
        }

        /// <summary>
        /// Associates an offline Windows image with a DISMSession.
        /// </summary>
        /// <param name="imagePath">An absolute or relative path to the root directory of an offline Windows image or an absolute or relative path to the root directory of a mounted Windows image.</param>
        /// <returns>A <see cref="DismSession"/> object.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismSession OpenOfflineSession(string imagePath)
        {
            return DismApi.OpenOfflineSession(imagePath, null, null);
        }

        /// <summary>
        /// Associates an offline Windows image with a DISMSession.
        /// </summary>
        /// <param name="imagePath">An absolute or relative path to the root directory of an offline Windows image or an absolute or relative path to the root directory of a mounted Windows image.</param>
        /// <param name="windowsDirectory">A relative or absolute path to the Windows directory. The path is relative to the mount point.</param>
        /// <param name="systemDrive">The letter of the system drive that contains the boot manager. If SystemDrive is NULL, the default value of the drive containing the mount point is used.</param>
        /// <returns>A <see cref="DismSession"/> object.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismSession OpenOfflineSession(string imagePath, string windowsDirectory, string systemDrive)
        {
            return DismApi.OpenSession(imagePath, windowsDirectory, systemDrive);
        }

        /// <summary>
        /// Associates an online Windows image with a DISMSession.
        /// </summary>
        /// <returns>A <see cref="DismSession"/> object.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismSession OpenOnlineSession()
        {
            return OpenSession(DismApi.DISM_ONLINE_IMAGE, null, null);
        }

        /// <summary>
        /// Remounts a Windows image from the .wim or .vhd file that was previously mounted at the path specified by MountPath.  Use the DismOpenSession Function to associate the image with a DISMSession after it is remounted.
        ///
        /// You can use the DismRemountImage function when the image is in the DismMountStatusNeedsRemount state, as described by the DismMountStatus Enumeration. The image may enter this state if it is mounted and then a reboot occurs.
        /// </summary>
        /// <param name="mountPath">A relative or absolute path to the mount directory of the image.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void RemountImage(string mountPath)
        {
            ThrowIfFail(() => NativeMethods.DismRemountImage(mountPath));
        }

        /// <summary>
        /// Removes the capability from an image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="capabilityName">The name of the capability that is being removed</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void RemoveCapability(DismSession session, string capabilityName)
        {
            DismApi.RemoveCapability(session, capabilityName, null, null);
        }

        /// <summary>
        /// Removes the capability from an image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="capabilityName">The name of the capability that is being removed</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <param name="userData">Optional user data to pass to the DismProgressCallback method.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        public static void RemoveCapability(DismSession session, string capabilityName, Dism.DismProgressCallback progressCallback, object userData)
        {
            // Create a DismProgress object to wrap the callback and allow cancellation
            var progress = new DismProgress(progressCallback, userData);

            ThrowIfFail(() => NativeMethods.DismRemoveCapability(session, capabilityName, progress.EventHandle, progress.DismProgressCallbackNative, IntPtr.Zero));
        }

        /// <summary>
        /// Removes an out-of-box driver from an offline image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="driverPath">The published file name of the driver that has been added to the image, for example OEM1.inf. You can use the GetDrivers method to get the published name of the driver.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void RemoveDriver(DismSession session, string driverPath)
        {
            ThrowIfFail(() => NativeMethods.DismRemoveDriver(session, driverPath));
        }

        /// <summary>
        /// Removes a package from an image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="packageName">The package name.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void RemovePackageByName(DismSession session, string packageName)
        {
            DismApi.RemovePackageByName(session, packageName, null);
        }

        /// <summary>
        /// Removes a package from an image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="packageName">The package name.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        public static void RemovePackageByName(DismSession session, string packageName, Dism.DismProgressCallback progressCallback)
        {
            DismApi.RemovePackageByName(session, packageName, progressCallback, null);
        }

        /// <summary>
        /// Removes a package from an image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="packageName">The package name.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <param name="userData">Optional user data to pass to the DismProgressCallback method.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        public static void RemovePackageByName(DismSession session, string packageName, Dism.DismProgressCallback progressCallback, object userData)
        {
            DismApi.RemovePackage(session, packageName, DismPackageIdentifier.Name, progressCallback, userData);
        }

        /// <summary>
        /// Removes a package from an image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="packagePath">The package path.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void RemovePackageByPath(DismSession session, string packagePath)
        {
            DismApi.RemovePackageByPath(session, packagePath, null);
        }

        /// <summary>
        /// Removes a package from an image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="packagePath">The package path.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        public static void RemovePackageByPath(DismSession session, string packagePath, Dism.DismProgressCallback progressCallback)
        {
            DismApi.RemovePackageByPath(session, packagePath, progressCallback, null);
        }

        /// <summary>
        /// Removes a package from an image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="packagePath">The package path.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <param name="userData">Optional user data to pass to the DismProgressCallback method.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        public static void RemovePackageByPath(DismSession session, string packagePath, Dism.DismProgressCallback progressCallback, object userData)
        {
            DismApi.RemovePackage(session, packagePath, DismPackageIdentifier.Path, progressCallback, userData);
        }

        /// <summary>
        /// Repairs a corrupted image that has been identified as repairable by the CheckImageHealth Function.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="limitAccess">Specifies whether the RestoreImageHealth method should contact Windows Update (WU) as a source location for downloading repair files. Before checking WU, DISM will check for the files in the sourcePaths provided and in any locations specified in the registry by Group Policy. If the files that are required to enable the feature are found in these other specified locations, this flag is ignored.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void RestoreImageHealth(DismSession session, bool limitAccess)
        {
            DismApi.RestoreImageHealth(session, limitAccess, null);
        }

        /// <summary>
        /// Repairs a corrupted image that has been identified as repairable by the CheckImageHealth Function.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="limitAccess">Specifies whether the RestoreImageHealth method should contact Windows Update (WU) as a source location for downloading repair files. Before checking WU, DISM will check for the files in the sourcePaths provided and in any locations specified in the registry by Group Policy. If the files that are required to enable the feature are found in these other specified locations, this flag is ignored.</param>
        /// <param name="sourcePaths">List of source locations to check for repair files.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void RestoreImageHealth(DismSession session, bool limitAccess, List<string> sourcePaths)
        {
            DismApi.RestoreImageHealth(session, limitAccess, sourcePaths, null);
        }

        /// <summary>
        /// Repairs a corrupted image that has been identified as repairable by the CheckImageHealth Function.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="limitAccess">Specifies whether the RestoreImageHealth method should contact Windows Update (WU) as a source location for downloading repair files. Before checking WU, DISM will check for the files in the sourcePaths provided and in any locations specified in the registry by Group Policy. If the files that are required to enable the feature are found in these other specified locations, this flag is ignored.</param>
        /// <param name="sourcePaths">List of source locations to check for repair files.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        public static void RestoreImageHealth(DismSession session, bool limitAccess, List<string> sourcePaths, Dism.DismProgressCallback progressCallback)
        {
            DismApi.RestoreImageHealth(session, limitAccess, sourcePaths, progressCallback, null);
        }

        /// <summary>
        /// Repairs a corrupted image that has been identified as repairable by the CheckImageHealth Function.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="sourcePaths">List of source locations to check for repair files.</param>
        /// <param name="limitAccess">Specifies whether the RestoreImageHealth method should contact Windows Update (WU) as a source location for downloading repair files. Before checking WU, DISM will check for the files in the sourcePaths provided and in any locations specified in the registry by Group Policy. If the files that are required to enable the feature are found in these other specified locations, this flag is ignored.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <param name="userData">Optional user data to pass to the DismProgressCallback method.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        public static void RestoreImageHealth(DismSession session, bool limitAccess, List<string> sourcePaths, Dism.DismProgressCallback progressCallback, object userData)
        {
            // Get the list of source paths as an array
            var sourcePathsArray = sourcePaths?.ToArray() ?? new string[0];

            // Create a DismProgress object to wrap the callback and allow cancellation
            var progress = new DismProgress(progressCallback, userData);

            ThrowIfFail(() => NativeMethods.DismRestoreImageHealth(session, sourcePathsArray, (uint)sourcePathsArray.Length, limitAccess, progress.EventHandle, progress.DismProgressCallbackNative, IntPtr.Zero));
        }

        /// <summary>
        /// Shuts down DISM API. Shutdown must be called once per process. Other DISM API function calls will fail after Shutdown has been called.
        /// </summary>
        /// <returns></returns>
        public static void Shutdown()
        {
            ThrowIfFail(NativeMethods.DismShutdown);
        }

        /// <summary>
        /// Unmounts a Windows image from a specified location.
        /// </summary>
        /// <param name="mountPath">A relative or absolute path to the mount directory of the image.</param>
        /// <param name="commitChanges">Specifies whether or not the changes to the image should be saved.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void UnmountImage(string mountPath, bool commitChanges)
        {
            DismApi.UnmountImage(mountPath, commitChanges, null);
        }

        /// <summary>
        /// Unmounts a Windows image from a specified location.
        /// </summary>
        /// <param name="mountPath">A relative or absolute path to the mount directory of the image.</param>
        /// <param name="commitChanges">Specifies whether or not the changes to the image should be saved.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        public static void UnmountImage(string mountPath, bool commitChanges, Dism.DismProgressCallback progressCallback)
        {
            DismApi.UnmountImage(mountPath, commitChanges, progressCallback, null);
        }

        /// <summary>
        /// Unmounts a Windows image from a specified location.
        /// </summary>
        /// <param name="mountPath">A relative or absolute path to the mount directory of the image.</param>
        /// <param name="commitChanges">Specifies whether or not the changes to the image should be saved.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <param name="userData">Optional user data to pass to the DismProgressCallback method.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        public static void UnmountImage(string mountPath, bool commitChanges, Dism.DismProgressCallback progressCallback, object userData)
        {
            // Determine flags
            var flags = commitChanges ? DismApi.DISM_COMMIT_MASK : DismApi.DISM_DISCARD_IMAGE;

            // Create a DismProgress object to wrap the callback and allow cancellation
            var progress = new DismProgress(progressCallback, userData);

            ThrowIfFail(() => NativeMethods.DismUnmountImage(mountPath, flags, progress.EventHandle, progress.DismProgressCallbackNative, IntPtr.Zero));
        }

        #region Undocumented functions found in the DISM PowerShell module

        /// <summary>
        /// Adds an app package (.appx) that will install for each new user to a Windows image.
        /// </summary>
        /// <param name="session">A valid DISM Session.</param>
        /// <param name="appPath">Specifies the location of the app package (.appx) to add to the Windows image.</param>
        /// <param name="dependencyPackages">Specifies the location of dependency packages.</param>
        /// <param name="licensePath">Specifies the location of the .xml file containing your application license.</param>
        /// <param name="skipLicense">Specifies the location of the .xml file containing your application license.</param>
        /// <param name="customDataPath">Specifies the location of a custom data file. The custom data file will be renamed custom.data and saved in the app data store.</param>
        public static void _AddProvisionedAppxPackage(DismSession session, string appPath, List<string> dependencyPackages, string licensePath, bool skipLicense, string customDataPath)
        {
            string[] dependencyPackagesArray = dependencyPackages?.ToArray() ?? new string[0];

            ThrowIfFail(() => NativeMethods._DismAddProvisionedAppxPackage(session, appPath, dependencyPackagesArray, (uint)dependencyPackagesArray.Length, licensePath, skipLicense, customDataPath));
        }

        /// <summary>
        /// Gets information about app packages (.appx) in an image that will be installed for each new user.
        /// </summary>
        /// <param name="session">A valid DISM Session.</param>
        /// <returns>A <see cref="DismAppxPackageCollection"/> object containing a collection of <see cref="DismAppxPackage"/> objects.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static DismAppxPackageCollection _GetProvisionedAppxPackages(DismSession session)
        {
            var appxPackages = new DismAppxPackageCollection();

            IntPtr appxPackagesPtr = IntPtr.Zero;
            UInt32 appxPackagesCount = 0;
            try
            {
                ThrowIfFail(() => NativeMethods._DismGetProvisionedAppxPackages(session, out appxPackagesPtr, out appxPackagesCount));

                appxPackages.AddRange<DismApi.DismAppxPackage_>(appxPackagesPtr, (int)appxPackagesCount, i => new DismAppxPackage(i));
            }
            finally
            {
                DismApi.Delete(appxPackagesPtr);
            }

            return appxPackages;
        }

        /// <summary>
        /// Removes an app package (.appx) from a Windows image.
        /// </summary>
        /// <param name="session">A valid DISM Session.</param>
        /// <param name="packageName">Specifies the name of the app package (.appx) to remove from the Windows image.</param>
        public static void _RemoveProvisionedAppxPackage(DismSession session, string packageName)
        {
            ThrowIfFail(() => NativeMethods._DismRemoveProvisionedAppxPackage(session, packageName));
        }

        #endregion Undocumented functions found in the DISM PowerShell module

        /// <summary>
        /// Releases resources held by a structure or an array of structures returned by other DISM API functions.
        /// </summary>
        /// <param name="handle">A pointer to the structure, or array of structures, to be deleted. The structure must have been returned by an earlier call to a DISM API function.</param>
        /// <returns>A standard HRESULT indication success or failure.</returns>
        private static void Delete(IntPtr handle)
        {
            // Call the native function
            NativeMethods.DismDelete(handle);
        }

        /// <summary>
        /// Throws an exception if the specified function fails.
        /// </summary>
        /// <param name="func">A <see cref="Func{Int32}"/> to execute and evaluate the return code of.</param>
        private static void ThrowIfFail(Func<int> func)
        {
            ThrowIfFail(func, Win32Error.ERROR_SUCCESS);
        }

        /// <summary>
        /// Throws an exception if the specified function fails.
        /// </summary>
        /// <param name="func">A <see cref="Func{Int32}"/> to execute and evaluate the return code of.</param>
        /// <param name="successCodes">A list of one or more codes to consider as success for the function.</param>
        private static void ThrowIfFail(Func<int> func, params int[] successCodes)
        {
            if (successCodes == null)
            {
                throw new ArgumentNullException(nameof(successCodes));
            }

            int hresult = func();

            if (!successCodes.Any(code => code.Equals(hresult)))
            {
                throw DismException.GetDismExceptionForHResult(hresult);
            }
        }

        /// <summary>
        /// Enables a feature from the specified package path.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="featureName">The name of the feature that is being enabled. To enable more than one feature, separate each feature name with a semicolon.</param>
        /// <param name="identifier">A package name or absolute path.</param>
        /// <param name="packageIdentifier">A DismPackageIdentifier value.</param>
        /// <param name="limitAccess">Specifies whether Windows Update (WU) should be contacted as a source location for downloading files if none are found in other specified locations. Before checking WU, DISM will check for the files in the SourcePaths provided and in any locations specified in the registry by group policy. If the files required to enable the feature are still present on the computer, this flag is ignored.</param>
        /// <param name="enableAll">Specifies whether to enable all dependencies of the feature. If the specified feature or any one of its dependencies cannot be enabled, none of them will be changed from their existing state.</param>
        /// <param name="sourcePaths">A list of source locations to check for files needed to enable the feature.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <param name="userData">Optional user data to pass to the DismProgressCallback method.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        /// <exception cref="DismRebootRequiredException">When the operation requires a reboot to complete.</exception>
        private static void EnableFeature(DismSession session, string featureName, string identifier, DismPackageIdentifier packageIdentifier, bool limitAccess, bool enableAll, List<string> sourcePaths, Microsoft.Dism.DismProgressCallback progressCallback, object userData)
        {
            // Get the list of source paths as an array
            string[] sourcePathsArray = sourcePaths?.ToArray() ?? new string[0];

            // Create a DismProgress object to wrap the callback and allow cancellation
            var progress = new DismProgress(progressCallback, userData);

            ThrowIfFail(() => NativeMethods.DismEnableFeature(session, featureName, identifier, identifier == null ? DismPackageIdentifier.None : packageIdentifier, limitAccess, sourcePathsArray, (uint)sourcePathsArray.Length, enableAll, progress.EventHandle, progress.DismProgressCallbackNative, IntPtr.Zero));
        }

        /// <summary>
        /// Gets detailed information for the specified feature.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="featureName">The name of the feature that you want to get more information about.</param>
        /// <param name="identifier">Either an absolute path to a .cab file or the package name, depending on the packageIdentifier parameter value.</param>
        /// <param name="packageIdentifier">A valid DismPackageIdentifier Enumeration value.</param>
        /// <returns>A <see cref="DismFeatureInfo"/> object.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        private static DismFeatureInfo GetFeatureInfo(DismSession session, string featureName, string identifier, DismPackageIdentifier packageIdentifier)
        {
            // Stores the output from DismGetFeatureInfo
            IntPtr featureInfoPtr = IntPtr.Zero;

            try
            {
                ThrowIfFail(() => NativeMethods.DismGetFeatureInfo(session, featureName, identifier, packageIdentifier, out featureInfoPtr));

                // Return a new DismFeatureInfo from the native pointer
                return new DismFeatureInfo(featureInfoPtr);
            }
            finally
            {
                // Clean up the native pointer
                DismApi.Delete(featureInfoPtr);
            }
        }

        /// <summary>
        /// Gets the parent features of a specified feature.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the OpenSession Function.</param>
        /// <param name="featureName">The name of the feature that you want to find the parent of.</param>
        /// <param name="identifier">Either an absolute path to a .cab file or the package name, depending on the PackageIdentifier parameter value.</param>
        /// <param name="packageIdentifier">Optional. A valid DismPackageIdentifier Enumeration value.</param>
        /// <returns>A <see cref="DismFeatureCollection"/> object containing a collection of <see cref="DismFeature"/> objects.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        private static DismFeatureCollection GetFeatureParent(DismSession session, string featureName, string identifier, DismPackageIdentifier packageIdentifier)
        {
            var features = new DismFeatureCollection();

            // Used for the native call
            IntPtr featurePtr = IntPtr.Zero;
            UInt32 featureCount = 0;

            try
            {
                ThrowIfFail(() => NativeMethods.DismGetFeatureParent(session, featureName, identifier, packageIdentifier, out featurePtr, out featureCount));

                // Add the items
                features.AddRange<DismApi.DismFeature_>(featurePtr, (int)featureCount, i => new DismFeature(i));
            }
            finally
            {
                // Clean up
                DismApi.Delete(featurePtr);
            }

            return features;
        }

        /// <summary>
        /// Gets all the features in an image, regardless of whether the features are enabled or disabled.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="identifier">Optional. Either an absolute path to a .cab file or the package name, depending on the packageIdentifier parameter value.</param>
        /// <param name="packageIdentifier">A valid DismPackageIdentifier Enumeration value.</param>
        /// <returns>A <see cref="DismFeatureCollection"/> object containing a collection of <see cref="DismFeature"/> objects.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        private static DismFeatureCollection GetFeatures(DismSession session, string identifier, DismPackageIdentifier packageIdentifier)
        {
            var features = new DismFeatureCollection();

            IntPtr featurePtr = IntPtr.Zero;
            UInt32 featureCount = 0;

            try
            {
                ThrowIfFail(() => NativeMethods.DismGetFeatures(session, identifier, packageIdentifier, out featurePtr, out featureCount));

                // Add the items
                features.AddRange<DismApi.DismFeature_>(featurePtr, (int)featureCount, i => new DismFeature(i));
            }
            finally
            {
                // Clean up
                DismApi.Delete(featurePtr);
            }

            return features;
        }

        /// <summary>
        /// Gets extended information about a package.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the OpenImageSession Function.</param>
        /// <param name="identifier">Either an absolute path to a .cab file or the package name, depending on the PackageIdentifier parameter value.</param>
        /// <param name="packageIdentifier">A valid DismPackageIdentifier Enumeration value.</param>
        /// <returns>A <see cref="DismPackageInfo"/> object.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        private static DismPackageInfo GetPackageInfo(DismSession session, string identifier, DismPackageIdentifier packageIdentifier)
        {
            // Used for the native call
            IntPtr packageInfoPtr = IntPtr.Zero;

            try
            {
                ThrowIfFail(() => NativeMethods.DismGetPackageInfo(session, identifier, packageIdentifier, out packageInfoPtr));

                // Return a new DismPackageInfo object with a reference to the pointer
                return new DismPackageInfo(packageInfoPtr);
            }
            finally
            {
                // Clean up
                DismApi.Delete(packageInfoPtr);
            }
        }

        /// <summary>
        /// Mounts a WIM or VHD image file to a specified location.
        /// </summary>
        /// <param name="imageFilePath">The path to the WIM or VHD file on the local computer. A .wim, .vhd, or .vhdx file name extension is required.</param>
        /// <param name="mountPath">The path of the location where the image should be mounted. This mount path must already exist on the computer. The Windows image in a .wim, .vhd, or .vhdx file can be mounted to an empty folder on an NTFS formatted drive. A Windows image in a .vhd or .vhdx file can also be mounted to an unassigned drive letter. You cannot mount an image to the root of the existing drive.</param>
        /// <param name="imageIndex">The index of the image in the WIM file that you want to mount. For a VHD file, you must specify an index of 1.</param>
        /// <param name="imageName">The name of the image that you want to mount.</param>
        /// <param name="imageIdentifier">A DismImageIdentifier Enumeration value such as DismImageIndex.</param>
        /// <param name="readOnly">Specifies if the image should be mounted in read-only mode.</param>
        /// <param name="options">Specifies options to use when mounting an image.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <param name="userData">Optional user data to pass to the DismProgressCallback method.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        private static void MountImage(string imageFilePath, string mountPath, int imageIndex, string imageName, DismImageIdentifier imageIdentifier, bool readOnly, DismMountImageOptions options, Microsoft.Dism.DismProgressCallback progressCallback, object userData)
        {
            // Determine the flags to pass to the native call
            var flags = (readOnly ? DismApi.DISM_MOUNT_READONLY : DismApi.DISM_MOUNT_READWRITE) | (uint)options;

            // Create a DismProgress object to wrap the callback and allow cancellation
            var progress = new DismProgress(progressCallback, userData);

            ThrowIfFail(() => NativeMethods.DismMountImage(imageFilePath, mountPath, (uint)imageIndex, imageName, imageIdentifier, flags, progress.EventHandle, progress.DismProgressCallbackNative, IntPtr.Zero));
        }

        /// <summary>
        /// Associates an offline or online Windows image with a DISMSession.
        /// </summary>
        /// <param name="imagePath">An absolute or relative path to the root directory of an offline Windows image, an absolute or relative path to the root directory of a mounted Windows image, or DISM_ONLINE_IMAGE to associate with the online Windows installation.</param>
        /// <param name="windowsDirectory">A relative or absolute path to the Windows directory. The path is relative to the mount point.</param>
        /// <param name="systemDrive">The letter of the system drive that contains the boot manager. If SystemDrive is NULL, the default value of the drive containing the mount point is used.</param>
        /// <returns>A <see cref="DismSession"/> object.</returns>
        /// <exception cref="DismException">When a failure occurs.</exception>
        private static DismSession OpenSession(string imagePath, string windowsDirectory, string systemDrive)
        {
            DismSession session = null;

            ThrowIfFail(() => NativeMethods.DismOpenSession(imagePath, windowsDirectory, systemDrive, out session));

            return session;
        }

        /// <summary>
        /// Removes a package from an image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <param name="identifier">Either an absolute path to a .cab file or the package name, depending on the PackageIdentifier parameter value.</param>
        /// <param name="packageIdentifier">A DismPackageIdentifier Enumeration.</param>
        /// <param name="progressCallback">A progress callback method to invoke when progress is made.</param>
        /// <param name="userData">Optional user data to pass to the DismProgressCallback method.</param>
        /// <exception cref="DismException">When a failure occurs.</exception>
        /// <exception cref="OperationCanceledException">When the user requested the operation be canceled.</exception>
        private static void RemovePackage(DismSession session, string identifier, DismPackageIdentifier packageIdentifier, Dism.DismProgressCallback progressCallback, object userData)
        {
            // Create a DismProgress object to wrap the callback and allow cancellation
            var progress = new DismProgress(progressCallback, userData);

            ThrowIfFail(() => NativeMethods.DismRemovePackage(session, identifier, packageIdentifier, progress.EventHandle, progress.DismProgressCallbackNative, IntPtr.Zero));
        }
    }
}