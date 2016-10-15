using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming
namespace Microsoft.Dism
{
    public static partial class DismApi
    {
        /// <summary>
        /// Represents native functions called by DismApi.
        /// </summary>
        internal static class NativeMethods
        {
            private const string DismDllName = "DismApi";
            private const CharSet DismCharacterSet = CharSet.Unicode;

            /// <summary>
            /// Add a capability to an image.
            /// </summary>
            /// <param name="Session">A valid DismSession. The DismSession must be associated with an image. You can associate a session with an image by using the DismOpenSession.</param>
            /// <param name="Name">The name of the capability that is being added.</param>
            /// <param name="LimitAccess">The flag indicates whether WU/WSUS should be contacted as a source location for downloading the payload of a capability. If payload of the capability to be added exists, the flag is ignored.</param>
            /// <param name="SourcePaths">A list of source locations. The function shall look up removed payload files from the locations specified in SourcePaths, and if not found, continue the search by contacting WU/WSUS depending on parameter LimitAccess.</param>
            /// <param name="SourcePathCount">The count of entries in SourcePaths.</param>
            /// <param name="CancelEvent">This is a handle to an event for cancellation.</param>
            /// <param name="Progress">Pointer to a client defined callback function to report progress.</param>
            /// <param name="UserData">User defined custom data. This will be passed back to the user through the callback.</param>
            /// <returns>Returns S_OK on success.</returns>
            /// <remarks>
            /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/mt684919.aspx"/>
            /// HRESULT WINAPI DismAddCapability(_In_ DismSession Session, _In_ PCWSTR Name, _In_ BOOL LimitAccess, _In_ PCWSTR* SourcePaths, _In_opt_ UINT SourcePathCount, _In_opt_ HANDLE CancelEvent, _In_opt_ DISM_PROGRESS_CALLBACK  Progress, _In_opt_ PVOID UserData);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismAddCapability(
                DismSession Session,
                string Name,
                [MarshalAs(UnmanagedType.Bool)] bool LimitAccess,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 6)] string[] SourcePaths,
                UInt32 SourcePathCount,
                SafeWaitHandle CancelEvent,
                DismProgressCallback Progress,
                IntPtr UserData);

            /// <summary>
            /// Adds a third party driver (.inf) to an offline Windows® image.
            /// </summary>
            /// <param name="Session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
            /// <param name="DriverPath">A relative or absolute path to the driver .inf file.</param>
            /// <param name="ForceUnsigned">A Boolean value that specifies whether to accept unsigned drivers to an x64-based image. Unsigned drivers will automatically be added to an x86-based image.</param>
            /// <returns>Returns S_OK on success.</returns>
            /// <remarks>
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824764.aspx"/>
            /// CDATA[HRESULT WINAPI DismAddDriver (_In_ DismSession Session, _In_ PCWSTR DriverPath, _In_ BOOL ForceUnsigned);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismAddDriver(DismSession Session, string DriverPath, [MarshalAs(UnmanagedType.Bool)] bool ForceUnsigned);

            /// <summary>
            /// Adds a single .cab or .msu file to a Windows® image.
            /// </summary>
            /// <param name="Session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
            /// <param name="PackagePath">A relative or absolute path to the .cab or .msu file being added, a folder with one or more packages, or a folder containing the expanded files of a single .cab file.</param>
            /// <param name="IgnoreCheck">A Boolean value to specify whether to ignore the internal applicability checks that are done when a package is added.</param>
            /// <param name="PreventPending">A Boolean value to specify whether to add a package if it has pending online actions.</param>
            /// <param name="CancelEvent">Optional. You can set a CancelEvent for this function in order to cancel the operation in progress when signaled by the client. If the CancelEvent is received at a stage when the operation cannot be canceled, the operation will continue and return a success code. If the CancelEvent is received and the operation is canceled, the image state is unknown. You should verify the image state before continuing or discard the changes and start again.</param>
            /// <param name="Progress">Optional. A pointer to a client-defined DismProgressCallback Function.</param>
            /// <param name="UserData">Optional. User defined custom data.</param>
            /// <returns>Returns S_OK on success.</returns>
            /// <remarks>Only .cab files can be added to an online image. Either .cab or .msu files can be added to an offline image.
            ///
            /// This function will return a special error code if the package is not applicable. You can use the DismGetPackageInfo Function to determine if a package is applicable to the target image.
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824788.aspx"/>
            /// HRESULT WINAPI DismAddPackage (_In_ DismSession Session, _In_ PCWSTR PackagePath, _In_ BOOL IgnoreCheck, _In_ BOOL PreventPending _In_opt_ HANDLE CancelEvent, _In_opt_ DISM_PROGRESS_CALLBACK Progress, _In_opt_ PVOID UserData)
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismAddPackage(DismSession Session, string PackagePath, [MarshalAs(UnmanagedType.Bool)] bool IgnoreCheck, [MarshalAs(UnmanagedType.Bool)] bool PreventPending, SafeWaitHandle CancelEvent, DismProgressCallback Progress, IntPtr UserData);

            /// <summary>
            /// Applies an unattended answer file to a Windows® image.
            /// </summary>
            /// <param name="Session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
            /// <param name="UnattendFile">A relative or absolute path to the answer file that will be applied to the image.</param>
            /// <param name="SingleSession">A Boolean value that specifies whether the packages that are listed in an answer file will be processed in a single session or in multiple sessions.</param>
            /// <returns>Returns S_OK on success.</returns>
            /// <remarks>When you use DISM to apply an answer file to an image, the unattended settings in the offlineServicing configuration pass are applied to the Windows image. For more information, see Unattended Servicing Command-Line Options.
            /// 
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh825840.aspx"/>
            /// HRESULT WINAPI DismApplyUnattend (_In_ DismSession Session, _In_ PCWSTR UnattendFile, _In_ BOOL SingleSession);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismApplyUnattend(DismSession Session, string UnattendFile, [MarshalAs(UnmanagedType.Bool)] bool SingleSession);

            /// <summary>
            /// Checks whether the image can be serviced or whether it is corrupted.
            /// </summary>
            /// <param name="Session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
            /// <param name="ScanImage">A Boolean value that specifies whether to scan the image or just check for flags from a previous scan.</param>
            /// <param name="CancelEvent">Optional. You can set a CancelEvent for this function in order to cancel the operation in progress when signaled by the client. If the CancelEvent is received at a stage when the operation cannot be canceled, the operation will continue and return a success code. If the CancelEvent is received and the operation is canceled, the image state is unknown. You should verify the image state before continuing or discard the changes and start again.</param>
            /// <param name="Progress">Optional. A pointer to a client-defined DismProgressCallback Function.</param>
            /// <param name="UserData">Optional. User defined custom data.</param>
            /// <param name="ImageHealth">A pointer to the DismImageHealthState Enumeration. The enumeration value is set during this operation.</param>
            /// <returns>Returns S_OK on success.</returns>
            /// <remarks>If ScanImage is set to True, this function will take longer to finish.
            /// 
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824769.aspx"/>
            /// HRESULT WINAPI DismCheckImageHealth(_In_ DismSession Session, _In_ BOOL ScanImage, _In_opt_ HANDLE CancelEvent, _In_opt_ DISM_PROGRESS_CALLBACK Progress, _In_opt_ PVOID UserData, _Out_ DismImageHealthState* ImageHealth);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismCheckImageHealth(DismSession Session, [MarshalAs(UnmanagedType.Bool)] bool ScanImage, SafeWaitHandle CancelEvent, DismProgressCallback Progress, IntPtr UserData, out DismImageHealthState ImageHealth);

            /// <summary>
            /// Removes files and releases resources associated with corrupted or invalid mount paths.
            /// </summary>
            /// <returns>Returns S_OK on success.</returns>
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824743.aspx"/>
            /// HRESULT WINAPI DismCleanupMountpoints( );
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismCleanupMountpoints();

            /// <summary>
            /// Closes a DISMSession created by DismOpenSession Function. This function does not unmount the image. To unmount the image, use the DismUnmountImage Function once all sessions are closed.
            /// </summary>
            /// <param name="Session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
            /// <returns>Returns S_OK on success.
            ///
            /// If the DISMSession is performing operations on other threads, those operations will complete before the DISMSession is destroyed. If additional operations are invoked by other threads after DismCloseSession is called, but before DismCloseSession returns, those operations will fail and return a DISMAPI_E_INVALID_DISM_SESSION error.
            ///
            /// The DISMSession handle will become invalid after completion of this call. Operations invoked on the DISMSession after completion of DismCloseSession will fail and return the error E_INVALIDARG.</returns>
            /// <remarks>The DISMSession will be shut down after this call is completed but the image will not be unmounted. To unmount the image, use the DismUnmountImage Function once all sessions are closed.</remarks>
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh825839.aspx"/>
            /// HRESULT WINAPI DismCloseSession(_In_ DismSession Session);
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismCloseSession(IntPtr Session);

            /// <summary>
            /// Commits the changes made to a Windows® image in a mounted .wim or .vhd file. The image must be mounted using the DismMountImage Function.
            /// </summary>
            /// <param name="Session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
            /// <param name="Flags">The commit flags to use for this operation. For more information about mount flags, see DISM API Constants.</param>
            /// <param name="CancelEvent">Optional. You can set a CancelEvent for this function in order to cancel the operation in progress when signaled by the client. If the CancelEvent is received at a stage when the operation cannot be canceled, the operation will continue and return a success code. If the CancelEvent is received and the operation is canceled, the image state is unknown. You should verify the image state before continuing or discard the changes and start again.</param>
            /// <param name="Progress">Optional. A pointer to a client-defined DismProgressCallback Function.</param>
            /// <param name="UserData">Optional. User defined custom data.</param>
            /// <returns></returns>
            /// <remarks>The DismCommitImage function does not unmount the image.
            /// <para>DismCommitImage can only be used on an image that is mounted within the DISM infrastructure. It does not apply to images mounted by another tool, such as the DiskPart tool, which are serviced using the DismOpenSession Function. You must use the DismMountImage Function to mount an image within the DISM infrastructure.</para>
            /// 
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh825835.aspx"/>
            /// HRESULT WINAPI DismCommitImage(_In_ DismSession Session, _In_ DWORD Flags, _In_opt_ HANDLE CancelEvent, _In_opt_ DISM_PROGRESS_CALLBACK Progress, _In_opt_ PVOID UserData);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismCommitImage(DismSession Session, UInt32 Flags, SafeWaitHandle CancelEvent, DismProgressCallback Progress, IntPtr UserData);

            /// <summary>
            /// Releases resources held by a structure or an array of structures returned by other DISM API functions.
            /// </summary>
            /// <param name="DismStructure">A pointer to the structure, or array of structures, to be deleted. The structure must have been returned by an earlier call to a DISM API function.</param>
            /// <returns>Returns S_OK on success.</returns>
            /// <remarks>All structures that are returned by DISM API functions are allocated on the heap. The client must not delete or free these structures directly. Instead, the client should call DismDelete and pass in the pointer that was returned by the earlier DISM API call.
            /// 
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824768.aspx" />
            /// HRESULT WINAPI DismDelete(_In_ VOID* DismStructure);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismDelete(IntPtr DismStructure);

            /// <summary>
            /// Disables a feature in the current image.
            /// </summary>
            /// <param name="Session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
            /// <param name="FeatureName">The name of the feature that you want to disable. To disable more than one feature, separate each feature name with a semicolon.</param>
            /// <param name="PackageName">Optional. The name of the parent package that the feature is a part of.
            ///
            /// This is an optional parameter. If no package is specified, then the default Windows® Foundation package is used.</param>
            /// <param name="RemovePayload">A Boolean value specifying whether to remove the files required to enable the feature.</param>
            /// <param name="CancelEvent">Optional. You can set a CancelEvent for this function in order to cancel the operation in progress when signaled by the client. If the CancelEvent is received at a stage when the operation cannot be canceled, the operation will continue and return a success code. If the CancelEvent is received and the operation is canceled, the image state is unknown. You should verify the image state before continuing or discard the changes and start again.</param>
            /// <param name="Progress">Optional. A pointer to a client-defined DismProgressCallback Function.</param>
            /// <param name="UserData"></param>
            /// <returns>Returns S_OK on success.</returns>
            /// <remarks>
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824766.aspx"/>
            /// HRESULT WINAPI DismDisableFeature (_In_ DismSession Session, _In_ PCWSTR FeatureName, _In_opt_ PCWSTR PackageName, _In_ BOOL RemovePayload, _In_opt_ HANDLE CancelEvent, _In_opt_ DISM_PROGRESS_CALLBACK Progress, _In_opt_ PVOID UserData);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismDisableFeature(DismSession Session, string FeatureName, string PackageName, [MarshalAs(UnmanagedType.Bool)] bool RemovePayload, SafeWaitHandle CancelEvent, DismProgressCallback Progress, IntPtr UserData);

            /// <summary>
            /// Enables a feature in an image. Features are identified by a name and can optionally be tied to a package.
            /// </summary>
            /// <param name="Session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
            /// <param name="FeatureName">The name of the feature that is being enabled. To enable more than one feature, separate each feature name with a semicolon.</param>
            /// <param name="Identifier">Optional. Either an absolute path to a .cab file or the package name for the parent package of the feature to be enabled.</param>
            /// <param name="PackageIdentifier">Optional. A valid DismPackageIdentifier Enumeration value. DismPackageName should be used when the Identifier parameter is pointing to a package name, and DismPackagePath should be used when Identifier points to the absolute path of a .cab file. If Identifier field is not NULL, you must specify a valid PackageIdentifier parameter. If the Identifier field is NULL, the PackageIdentifier parameter is ignored.</param>
            /// <param name="LimitAccess">A Boolean value indicating whether Windows Update (WU) should be contacted as a source location for downloading files if none are found in other specified locations. Before checking WU, DISM will check for the files in the SourcePaths provided and in any locations specified in the registry by group policy. If the files required to enable the feature are still present on the computer, this flag is ignored.</param>
            /// <param name="SourcePaths">Optional. A list of source locations to check for files needed to enable the feature.</param>
            /// <param name="SourcePathCount">Optional. The number of source locations specified.</param>
            /// <param name="EnableAll">Enable all dependencies of the feature. If the specified feature or any one of its dependencies cannot be enabled, none of them will be changed from their existing state.</param>
            /// <param name="CancelEvent">Optional. You can set a CancelEvent for this function in order to cancel the operation in progress when signaled by the client. If the CancelEvent is received at a stage when the operation cannot be canceled, the operation will continue and return a success code. If the CancelEvent is received and the operation is canceled, the image state is unknown. You should verify the image state before continuing or discard the changes and start again.</param>
            /// <param name="Progress">Optional. A pointer to a client-defined DismProgressCallback Function.</param>
            /// <param name="UserData">Optional. User defined custom data.</param>
            /// <returns>Returns S_OK on success.</returns>
            /// <remarks>If the feature is present in the foundation package, you do not have to specify any package information. If the feature is in an optional package or feature pack that has already been installed in the image, specify a package name in the Identifier parameter and specify DismPackageName as the PackageIdentifier.If the feature cannot be enabled due to the parent feature not being enabled, a special error code will be returned. You can use EnableAll to enable the parent features when you enable the specified features, or you can use the DismGetFeatureParent Function to enumerate the parent features and enable them first.
            ///
            /// If the feature to be enabled is not a component of the foundation package, you must add the parent optional package with the DismAddPackage Function before you enable the feature. Do not you specify a path to a .cab file of an optional package that has not been added to the image in the Identifier parameter. If you specify a package that has not been added, and you specify DismPackagePath as the PackageIdentifier, the function will complete successfully but the feature will not be enabled.
            /// 
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824737.aspx"/>
            /// HRESULT WINAPI DismEnableFeature (_In_ DismSession Session, _In_ PCWSTR FeatureName, _In_opt_ PCWSTR Identifier, _In_opt_ DismPackageIdentifier PackageIdentifier, _In_ BOOL LimitAccess, _In_reads_opt_(SourcePathCount) PCWSTR* SourcePaths, _In_opt_ UINT SourcePathCount, _In_opt_ HANDLE CancelEvent, _In_opt_ DISM_PROGRESS_CALLBACK Progress, _In_opt_ PVOID UserData);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismEnableFeature(DismSession Session, string FeatureName, string Identifier, DismPackageIdentifier PackageIdentifier, [MarshalAs(UnmanagedType.Bool)] bool LimitAccess, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 6)] string[] SourcePaths, UInt32 SourcePathCount, [MarshalAs(UnmanagedType.Bool)] bool EnableAll, SafeWaitHandle CancelEvent, DismProgressCallback Progress, IntPtr UserData);

            /// <summary>
            /// Gets DISM capabilities.
            /// </summary>
            /// <param name="Session">A valid DismSession. The DismSession must be associated with an image. You can associate a session with an image by using the DismOpenSession.</param>
            /// <param name="Capability">Pointer that will receive the info of capability.</param>
            /// <param name="Count">The number of DismCapability structures that were returned.</param>
            /// <returns>Returns S_OK on success.</returns>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismGetCapabilities(DismSession Session, out IntPtr Capability, out UInt32 Count);

            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismGetCapabilityInfo(DismSession Session, string Name, out IntPtr Info);

            /// <summary>
            /// Gets information about an .inf file in a specified image.
            /// </summary>
            /// <param name="Session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
            /// <param name="DriverPath">A relative or absolute path to the driver .inf file.</param>
            /// <param name="Driver">A pointer to the address of an array of DismDriver Structure objects.</param>
            /// <param name="Count">The number of DismDriver structures that were returned.</param>
            /// <param name="DriverPackage">Optional. A pointer to the address of a DismDriverPackage Structure object.</param>
            /// <returns>Returns S_OK on success.</returns>
            /// <remarks>This function returns information about the .inf file installed on the image. The driver associated with the .inf file may or may not be installed in the image.
            /// 
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824733.aspx"/>
            /// HRESULT WINAPI DismGetDriverInfo (_In_ DismSession Session, _In_ PCWSTR DriverPath, _Outptr_result_buffer_(*Count) DismDriver** Driver, _Out_ UINT* Count, _Out_opt_ DismDriverPackage** DriverPackage);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismGetDriverInfo(DismSession Session, string DriverPath, out IntPtr Driver, out UInt32 Count, out IntPtr DriverPackage);

            /// <summary>
            /// Lists the drivers in an image.
            /// </summary>
            /// <param name="Session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
            /// <param name="AllDrivers">A Boolean value specifying which drivers to retrieve.</param>
            /// <param name="DriverPackage">A pointer to the address of an array of DismDriverPackage Structure objects.</param>
            /// <param name="Count">The number of DismDriverPackage structures that were returned.</param>
            /// <returns>Returns S_OK on success.</returns>
            /// <remarks>
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824784.aspx"/>
            /// HRESULT WINAPI DismGetDrivers (_In_ DismSession Session, _In_ BOOL AllDrivers, _Outptr_result_buffer_(*Count) DismDriverPackage** DriverPackage, _Out_ UINT* Count);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismGetDrivers(DismSession Session, [MarshalAs(UnmanagedType.Bool)] bool AllDrivers, out IntPtr DriverPackage, out UInt32 Count);

            /// <summary>
            /// Gets detailed information for the specified feature.
            /// </summary>
            /// <param name="Session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
            /// <param name="FeatureName">The name of the feature that you want to get more information about.</param>
            /// <param name="Identifier">Optional. Either an absolute path to a .cab file or the package name, depending on the PackageIdentifier parameter value.</param>
            /// <param name="PackageIdentifier">Optional. A valid DismPackageIdentifier Enumeration value.</param>
            /// <param name="FeatureInfo">A pointer to the address of an array of DismFeatureInfo Structure objects.</param>
            /// <returns>Returns S_OK on success.</returns>
            /// <remarks>You can use this function to get the custom properties of a feature. If the feature has custom properties, they will be stored in the CustomProperty field as an array. Not all features have custom properties.
            /// 
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824735.aspx"/>
            /// HRESULT WINAPI DismGetFeatureInfo (_In_ DismSession Session, _In_ PCWSTR FeatureName, _In_opt_ PCWSTR Identifier, _In_opt_ DismPackageIdentifier PackageIdentifier, _Out_ DismFeatureInfo** FeatureInfo);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismGetFeatureInfo(DismSession Session, string FeatureName, string Identifier, DismPackageIdentifier PackageIdentifier, out IntPtr FeatureInfo);

            /// <summary>
            /// Gets the parent features of a specified feature.
            /// </summary>
            /// <param name="Session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
            /// <param name="FeatureName">The name of the feature that you want to find the parent of.</param>
            /// <param name="Identifier">Optional. Either an absolute path to a .cab file or the package name, depending on the PackageIdentifier parameter value.</param>
            /// <param name="PackageIdentifier">Optional. A valid DismPackageIdentifier Enumeration value.</param>
            /// <param name="Feature">A pointer to the address of an array of DismFeature Structure objects.</param>
            /// <param name="Count">The number of DismFeature structures that were returned.</param>
            /// <returns>Returns S_OK on success.</returns>
            /// <remarks>For a feature to be enabled, one or more of its parent features must be enabled. You can use this function to enumerate the parent features and determine which parent needs to be enabled.
            /// 
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824798.aspx"/>
            /// HRESULT WINAPI DismGetFeatureParent (_In_ DismSession Session, _In_ PCWSTR FeatureName, _In_opt_ PCWSTR Identifier, _In_opt_ DismPackageIdentifier PackageIdentifier, _Outptr_result_buffer_(*Count) DismFeature** Feature, _Out_ UINT* Count);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismGetFeatureParent(DismSession Session, string FeatureName, string Identifier, DismPackageIdentifier PackageIdentifier, out IntPtr Feature, out UInt32 Count);

            /// <summary>
            /// Gets all the features in an image, regardless of whether the features are enabled or disabled.
            /// </summary>
            /// <param name="Session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
            /// <param name="Identifier">Optional. Either an absolute path to a .cab file or the package name, depending on the PackageIdentifier parameter value.</param>
            /// <param name="PackageIdentifier">Optional. A valid DismPackageIdentifier Enumeration value.</param>
            /// <param name="Feature">A pointer to the address of an array of DismFeature Structure objects.</param>
            /// <param name="Count">The number of DismFeature structures that were returned.</param>
            /// <returns>Returns S_OK on success.</returns>
            /// <remarks>
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824771.aspx"/>
            /// HRESULT WINAPI DismGetFeatures (_In_ DismSession Session, _In_opt_ PCWSTR Identifier, _In_opt_ DismPackageIdentifier PackageIdentifier, _Outptr_result_buffer_(*Count) DismFeature** Feature, _Out_ UINT* Count);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismGetFeatures(DismSession Session, string Identifier, DismPackageIdentifier PackageIdentifier, out IntPtr Feature, out UInt32 Count);

            /// <summary>
            /// Retrieves the error message in the current thread immediately after a failure.
            /// </summary>
            /// <param name="ErrorMessage">The detailed error message in the current thread.</param>
            /// <returns>Returns OK on success.</returns>
            /// <remarks>You can retrieve a detailed error message immediately after a DISM API failure. The last error message is maintained on a per-thread basis. An error message on a thread will not overwrite the last error message on another thread.
            ///
            /// DismGetLastErrorMessage does not apply to the DismShutdown function, DismDelete function, or the DismGetLastErrorMessage function.
            /// 
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824754.aspx"/>
            /// HRESULT WINAPI DismGetLastErrorMessage(_Out_ DismString** ErrorMessage);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismGetLastErrorMessage(out IntPtr ErrorMessage);

            /// <summary>
            /// Returns an array of DismImageInfo Structure elements that describe the images in a .wim or .vhd file.
            /// </summary>
            /// <param name="ImageFilePath">A relative or absolute path to a .wim or .vhd file.</param>
            /// <param name="ImageInfo">A pointer to the address of an array of DismImageInfo Structure objects.</param>
            /// <param name="Count">The number of DismImageInfo structures that are returned.</param>
            /// <returns>S_OK on success.</returns>
            /// <remarks>The array of DismImageInfo structures are allocated by DISM API on the heap.
            ///
            /// Important
            /// You must call the DismDelete Function, passing the ImageInfo pointer, to free the resources associated with the DismImageInfo structures.
            /// 
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824767.aspx" />
            /// HRESULT WINAPI DismGetImageInfo(_In_ PCWSTR ImageFilePath, _Outptr_result_buffer_(*Count) DismImageInfo** ImageInfo, _Out_ UINT* Count);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismGetImageInfo(string ImageFilePath, out IntPtr ImageInfo, out UInt32 Count);

            /// <summary>
            /// Returns an array of DismMountedImageInfo Structure elements that describe the images that are mounted currently.
            /// </summary>
            /// <param name="MountedImageInfo">A pointer to the address of an array of DismMountedImageInfo Structure objects.</param>
            /// <param name="Count">The number of DismMountedImageInfo structures that are returned.</param>
            /// <returns>Returns S_OK on success.</returns>
            /// <remarks>Only images mounted using the DISM infrastructure will be returned. If a .vhd file is mounted outside of DISM, such as with the DiskPart tool, this call will not return information about that image. You must use the DismMountImage Function to mount the image.
            ///
            /// The array of DismMountedImageInfo structures are allocated by the DISM API on the heap.
            ///
            /// You must call the DismDelete Function, passing the ImageInfo pointer, to free the resources associated with the DismImageInfo structures.
            /// 
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824745.aspx"/>
            /// HRESULT WINAPI DismGetMountedImageInfo(_Outptr_result_buffer_(*Count) DismMountedImageInfo** MountedImageInfo, _Out_ UINT* Count);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismGetMountedImageInfo(out IntPtr MountedImageInfo, out UInt32 Count);

            /// <summary>
            /// Retrieves all of the standard package properties as the DismGetPackages Function, as well as more specific package information and custom properties.
            /// </summary>
            /// <param name="DismSession">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
            /// <param name="Identifier">Either an absolute path to a .cab file or the package name, depending on the PackageIdentifier parameter value.</param>
            /// <param name="PackageIdentifier">A valid DismPackageIdentifier Enumeration value.</param>
            /// <param name="PackageInfo">A pointer to the address of an array of DismPackageInfo Structure objects.</param>
            /// <returns>Returns S_OK on success.</returns>
            /// <remarks>You can use this function to determine whether a package is applicable to the specified image. The DismPackageInfo Structure contains an Applicable field, which is a Boolean that returns TRUE if the package is applicable and FALSE if the package is not applicable to the specified image.
            /// 
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824801.aspx"/>
            /// HRESULT WINAPI DismGetPackageInfo (_In_ DismSession Session, _In_ PCWSTR Identifier, _In_ DismPackageIdentifier PackageIdentifier, _Out_ DismPackageInfo** PackageInfo);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismGetPackageInfo(DismSession DismSession, string Identifier, DismPackageIdentifier PackageIdentifier, out IntPtr PackageInfo);

            /// <summary>
            /// Lists each package in an image and provides basic information about each package, such as the package name and type of package.
            /// </summary>
            /// <param name="DismSession">A valid DISMSession. The DismSession must be associated with an image.</param>
            /// <param name="PackageInfo">A pointer to the array of DismPackage Structure objects.</param>
            /// <param name="Count">The number of DismPackage structures that are returned.</param>
            /// <returns>Returns S_OK on success.
            ///
            /// Package points to an array of DismPackage Structure objects. You can manipulate this array using normal array notation in order to get information about each package in the image.</returns>
            /// <remarks>When you are finished with the Package array, you must remove it by using the DismDelete Function.
            /// 
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824759.aspx"/>
            /// HRESULT WINAPI DismGetPackages (_In_ DismSession Session, _Outptr_result_buffer_(*Count) DismPackage** Package, _Out_ UINT* Count);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismGetPackages(DismSession DismSession, out IntPtr PackageInfo, out UInt32 Count);

            /// <summary>
            /// Initializes DISM API. DismInitialize must be called once per process before calling any other DISM API functions.
            /// </summary>
            /// <param name="LogLevel">A DismLogLevel Enumeration value, such as DismLogErrorsWarnings.</param>
            /// <param name="LogFilePath">Optional. A relative or absolute path to a log file. All messages generated will be logged to this path. If NULL, the default log path, %windir%\Logs\DISM\dism.log, will be used.</param>
            /// <param name="ScratchDirectory">Optional. A relative or absolute path to a scratch directory. DISM API will use this directory for internal operations. If NULL, the default temp directory, \Windows\%Temp%, will be used.</param>
            /// <returns>Returns S_OK on success.
            ///
            /// Returns DISMAPI_E_DISMAPI_ALREADY_INITIALIZED if DismInitialize has already been called by the process without a matching call to DismShutdown.
            ///
            /// Returns ERROR_ELEVATION_REQUIRED as an HRESULT if the process is not elevated.</returns>
            /// <remarks>The client code must call DismInitialize once per process. DISM API will serialize concurrent calls to DismInitialize. The first call will succeed and the others will fail. For more information, see Using the DISM API.
            ///
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824803.aspx" />
            /// HRESULT WINAPI DismInitialize(_In_ DismLogLevel LogLevel, _In_opt_ PCWSTR LogFilePath, _In_opt_ PCWSTR ScratchDirectory);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismInitialize(DismLogLevel LogLevel, string LogFilePath, string ScratchDirectory);

            /// <summary>
            /// Mounts a WIM or VHD image file to a specified location.
            /// </summary>
            /// <param name="ImageFilePath">The path to the WIM or VHD file on the local computer. A .wim, .vhd, or .vhdx file name extension is required.</param>
            /// <param name="MountPath">The path of the location where the image should be mounted. This mount path must already exist on the computer. The Windows image in a .wim, .vhd, or .vhdx file can be mounted to an empty folder on an NTFS formatted drive. A Windows image in a .vhd or .vhdx file can also be mounted to an unassigned drive letter. You cannot mount an image to the root of the existing drive.</param>
            /// <param name="ImageIndex">The index of the image in the WIM file that you want to mount. For a VHD file, you must specify an index of 1.</param>
            /// <param name="ImageName">Optional. The name of the image that you want to mount.</param>
            /// <param name="ImageIdentifier">A DismImageIdentifier Enumeration value such as DismImageIndex.</param>
            /// <param name="Flags">The mount flags to use for this operation. For more information about mount flags, see DISM API Constants.</param>
            /// <param name="CancelEvent">Optional. You can set a CancelEvent for this function in order to cancel the operation in progress when signaled by the client. If the CancelEvent is received at a stage when the operation cannot be canceled, the operation will continue and return a success code. If the CancelEvent is received and the operation is canceled, the image state is unknown. You should verify the image state before continuing or discard the changes and start again.</param>
            /// <param name="Progress">Optional. A pointer to a client-defined DismProgressCallback Function.</param>
            /// <param name="UserData">Optional. User defined custom data.</param>
            /// <returns>Returns OK on success.
            ///
            /// Returns E_INVALIDARG if any of the paths are not well-formed or if MountPath or ImageFilePath does not exist or is invalid.
            ///
            /// Returns a Win32 error code mapped to an HRESULT for other errors.</returns>
            /// <remarks>After mounting an image, use the DismOpenSession Function to start a servicing session. For more information, see Using the DISM API.
            ///
            /// Mounting an image from a WIM or VHD file that is stored on the network is not supported. You must specify a file on the local computer.
            ///
            /// To mount an image from a VHD file, you must specify an ImageIndex of 1.
            ///
            /// The MountPath must be a file path that already exists on the computer. Images in WIM and VHD files can be mounted to an empty folder on an NTFS formatted drive. You can also mount an image from a VHD file to an unassigned drive letter. You cannot mount an image to the root of the existing drive.
            ///
            /// When mounting an image in a WIM file, the image can either be identified by the image index number specified by ImageIndex, or the name of the image specified by ImageName. ImageIdentifier specifies whether to use the ImageIndex or ImageName parameter to identify the image.
            /// 
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824731.aspx"/>
            /// HRESULT WINAPI DismMountImage (_In_ PCWSTR ImageFilePath, _In_ PCWSTR MountPath, _In_ UINT ImageIndex, _In_opt_ PCWSTR ImageName, _In_ DismImageIdentifier ImageIdentifier, _In_ DWORD Flags, _In_opt_ HANDLE CancelEvent, _In_opt_ DISM_PROGRESS_CALLBACK Progress, _In_opt_ PVOID UserData);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismMountImage(string ImageFilePath, string MountPath, UInt32 ImageIndex, string ImageName, DismImageIdentifier ImageIdentifier, UInt32 Flags, SafeWaitHandle CancelEvent, DismProgressCallback Progress, IntPtr UserData);

            /// <summary>
            /// Associates an offline or online Windows image with a DISMSession.
            /// </summary>
            /// <param name="ImagePath">Set ImagePath to one of the following values:
            ///
            /// An absolute or relative path to the root directory of an offline Windows image.
            ///
            /// An absolute or relative path to the root directory of a mounted Windows image. You can mount the image before calling DismOpenSession by using an external tool or by using the DismMountImage Function.
            ///
            /// DISM_ONLINE_IMAGE to associate the DISMSession with the online Windows installation.</param>
            /// <param name="WindowsDirectory">Optional. A relative or absolute path to the Windows directory. The path is relative to the mount point. If the value of WindowsDirectory is NULL, the default value of Windows is used.
            ///
            /// The WindowsDirectory parameter cannot be used when the ImagePath parameter is set to DISM_ONLINE_IMAGE.</param>
            /// <param name="SystemDrive">Optional. The letter of the system drive that contains the boot manager. If SystemDrive is NULL, the default value of the drive containing the mount point is used.
            ///
            /// The SystemDrive parameter cannot be used when the ImagePath parameter is set to DISM_ONLINE_IMAGE.</param>
            /// <param name="Session">A pointer to a valid DISMSession. The DISMSession will be associated with the image after this call is successfully completed.</param>
            /// <returns>Returns S_OK on success.
            ///
            /// Returns DISMAPI_E_ALREADY_ASSOCIATED if the DISMSession already has an image associated with it.
            ///
            /// Returns a Win32 error code mapped to an HRESULT for other errors.</returns>
            /// <remarks>The DISMSession can be used to service the image after the DISMOpenSession call is successfully completed. The DISMSession must be shut down by calling the DismCloseSession Function.
            /// 
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824800.aspx"/>
            /// HRESULT WINAPI DismOpenSession(_In_ PCWSTR ImagePath, _In_opt_ PCWSTR WindowsDirectory, _In_opt_ WCHAR* SystemDrive, _Out_ DismSession* Session);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismOpenSession(string ImagePath, string WindowsDirectory, string SystemDrive, out DismSession Session);

            /// <summary>
            /// Remounts a Windows image from the .wim or .vhd file that was previously mounted at the path specified by MountPath. Use the DismOpenSession Function to associate the image with a DISMSession after it is remounted.
            ///
            /// You can use the DismRemountImage function when the image is in the DismMountStatusNeedsRemount state, as described by the DismMountStatus Enumeration. The image may enter this state if it is mounted and then a reboot occurs.
            /// </summary>
            /// <param name="MountPath">A relative or absolute path to the mount directory of the image.</param>
            /// <returns>Returns S_OK on success.</returns>
            /// <remarks>
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824778.aspx"/>
            /// HRESULT WINAPI DismRemountImage(_In_ PCWSTR MountPath);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismRemountImage(string MountPath);

            /// <summary>
            /// Add a capability to an image.
            /// </summary>
            /// <param name="Session">A valid DismSession. The DismSession must be associated with an image. You can associate a session with an image by using the DismOpenSession.</param>
            /// <param name="Name">The name of the capability that is being removed</param>
            /// <param name="CancelEvent">This is a handle to an event for cancellation.</param>
            /// <param name="Progress">Pointer to a client defined callback function to report progress.</param>
            /// <param name="UserData">User defined custom data. This will be passed back to the user through the callback.</param>
            /// <returns>Returns S_OK on success.</returns>
            /// <remarks>
            /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/mt684925.aspx"/>
            /// HRESULT WINAPI DismRemoveCapability(_In_ DismSession Session, _In_ PCWSTR Name, _In_opt_ HANDLE CancelEvent, _In_opt_ DISM_PROGRESS_CALLBACK  Progress, _In_opt_ PVOID UserData);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismRemoveCapability(DismSession Session, string Name, SafeWaitHandle CancelEvent, DismProgressCallback Progress, IntPtr UserData);

            /// <summary>
            /// Removes an out-of-box driver from an offline image.
            /// </summary>
            /// <param name="Session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
            /// <param name="DriverPath">The published file name of the driver that has been added to the image, for example OEM1.inf. You can use the DismGetDrivers Function to get the published name of the driver.</param>
            /// <returns>Returns S_OK on success.</returns>
            /// <remarks>This function only supports offline images.
            ///
            /// Important
            /// Removing a boot-critical driver can make the offline Windows image unable to boot.
            /// 
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824729.aspx"/>
            /// HRESULT WINAPI DismRemoveDriver (_In_ DismSession Session, _In_ PCWSTR DriverPath);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            public static extern int DismRemoveDriver(DismSession Session, string DriverPath);

            /// <summary>
            /// Removes a package from an image.
            /// </summary>
            /// <param name="Session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
            /// <param name="Identifier">Either an absolute path to a .cab file or the package name, depending on the PackageIdentifier parameter value.</param>
            /// <param name="PackageIdentifier">The parameter for a DismPackageIdentifier Enumeration.</param>
            /// <param name="CancelEvent">Optional. You can set a CancelEvent for this function in order to cancel the operation in progress when signaled by the client. If the CancelEvent is received at a stage when the operation cannot be canceled, the operation will continue and return a success code. If the CancelEvent is received and the operation is canceled, the image state is unknown. You should verify the image state before continuing or discard the changes and start again.</param>
            /// <param name="Progress">Optional. A pointer to a client-defined DismProgressCallback Function.</param>
            /// <param name="UserData">Optional. User defined custom data.</param>
            /// <returns>Returns S_OK on success.</returns>
            /// <remarks>The DismRemovePackage function does not support .msu files.</remarks>
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824732.aspx"/>
            /// HRESULT WINAPI DismRemovePackage (_In_ DismSession Session, _In_ PCWSTR Identifier, _In_ DismPackageIdentifier PackageIdentifier, _In_opt_ HANDLE CancelEvent, _In_opt_ DISM_PROGRESS_CALLBACK Progress, _In_opt_ PVOID UserData);
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismRemovePackage(DismSession Session, string Identifier, DismPackageIdentifier PackageIdentifier, SafeWaitHandle CancelEvent, DismProgressCallback Progress, IntPtr UserData);

            /// <summary>
            /// Repairs a corrupted image that has been identified as repairable by the DismCheckImageHealth Function.
            /// </summary>
            /// <param name="Session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
            /// <param name="SourcePaths">Optional. A list of source locations to check for repair files.</param>
            /// <param name="SourcePathCount">Optional. The number of source locations specified.</param>
            /// <param name="LimitAccess">Optional. A list of source locations to check for repair files.</param>
            /// <param name="CancelEvent">Optional. You can set a CancelEvent for this function in order to cancel the operation in progress when signaled by the client. If the CancelEvent is received at a stage when the operation cannot be canceled, the operation will continue and return a success code. If the CancelEvent is received and the operation is canceled, the image state is unknown. You should verify the image state before continuing or discard the changes and start again.</param>
            /// <param name="Progress">Optional. A pointer to a client-defined DismProgressCallback Function.</param>
            /// <param name="UserData">Optional. User defined custom data.</param>
            /// <returns>Returns S_OK on success.</returns>
            /// <remarks>Run the DismCheckImageHealth Function to determine if the image is corrupted and if the image is repairable. If the DismCheckImageHealth Function returns DismImageRepairable, the DismRestoreImageHealth function can repair the image.
            ///
            /// If a repair file is not found in any of the locations specified by the SourcePaths parameter or the location paths in the registry specified by Group Policy, the DismRestoreImageHealth function will contact WU to check for a repair file unless the LimitAccess parameter is set to True.
            /// 
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh825836.aspx"/>
            /// HRESULT WINAPI DismRestoreImageHealth(_In_ DismSession Session, _In_reads_opt_(SourcePathCount) PCWSTR* SourcePaths, _In_opt_ UINT SourcePathCount, _In_ BOOL LimitAccess, _In_opt_ DISM_PROGRESS_CALLBACK Progress, _In_opt_ PVOID UserData);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismRestoreImageHealth(DismSession Session, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 2)] string[] SourcePaths, UInt32 SourcePathCount, [MarshalAs(UnmanagedType.Bool)] bool LimitAccess, SafeWaitHandle CancelEvent, DismProgressCallback Progress, IntPtr UserData);

            /// <summary>
            /// Shuts down DISM API. DismShutdown must be called once per process. Other DISM API function calls will fail after DismShutdown has been called.
            /// </summary>
            /// <returns>Returns S_OK on success.
            ///
            /// Returns DISMAPI_E_DISMAPI_NOT_INITIALIZED if the DismInitialize Function has not been called.
            ///
            /// Returns DISMAPI_E_OPEN_SESSION_HANDLES if any open DISMSession have not been closed.</returns>
            /// <remarks>You must call DismShutdown once per process. Calls to DismShutdown must be matched to an earlier call to the DismInitialize Function. DISM API will serialize concurrent calls to DismShutdown. The first call will succeed and the other calls will fail.
            ///
            /// Before calling DismShutdown, you must close all DISMSession using the DismCloseSession Function. If there are open DismSessions when calling DismShutdown, then the DismShutdown call will fail. For more information, see Using the DISM API.
            /// 
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824773.aspx" />
            /// HRESULT WINAPI DismShutdown( );
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismShutdown();

            /// <summary>
            /// Unmounts a Windows image from a specified location.
            /// </summary>
            /// <param name="MountPath">A relative or absolute path to the mount directory of the image.</param>
            /// <param name="Flags">v</param>
            /// <param name="CancelEvent">Optional. You can set a CancelEvent for this function in order to cancel the operation in progress when signaled by the client. If the CancelEvent is received at a stage when the operation cannot be canceled, the operation will continue and return a success code. If the CancelEvent is received and the operation is canceled, the image state is unknown. You should verify the image state before continuing or discard the changes and start again.</param>
            /// <param name="Progress">Optional. A pointer to a client-defined DismProgressCallback Function.</param>
            /// <param name="UserData">Optional. User defined custom data.</param>
            /// <returns>Returns OK on success.</returns>
            /// <remarks>After you use the DismCloseSession Function to end every active DISMSession, you can unmount the image using the DismUnmountImage function.
            /// 
            /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824802.aspx"/>
            /// HRESULT WINAPI DismUnmountImage(_In_ PCWSTR MountPath, _In_ DWORD Flags, _In_opt_ HANDLE CancelEvent, _In_opt_ DISM_PROGRESS_CALLBACK Progress, _In_opt_ PVOID UserData);
            /// </remarks>
            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismUnmountImage(string MountPath, UInt32 Flags, SafeWaitHandle CancelEvent, DismProgressCallback Progress, IntPtr UserData);

            #region Undocumented functions found in the DISM PowerShell module

            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int _DismAddProvisionedAppxPackage(DismSession Session, [MarshalAs(UnmanagedType.LPWStr)] string AppPath, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 3)] string[] DependencyPackages, UInt32 DependencyPackageCount, [MarshalAs(UnmanagedType.LPWStr)] string LicensePath, bool SkipLicense, [MarshalAs(UnmanagedType.LPWStr)] string CustomDataPath);

            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int _DismGetProvisionedAppxPackages(DismSession Session, out IntPtr PackageBufPtr, out UInt32 PackageCount);

            [DllImport(DismDllName, CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int _DismRemoveProvisionedAppxPackage(DismSession Session, [MarshalAs(UnmanagedType.LPWStr)] string PackageName);

            #endregion Undocumented functions found in the DISM PowerShell module
        }
    }
}