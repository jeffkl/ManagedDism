/*
/// OVERVIEW:
/// 
/// This class was written to hold non-DISM-specific data structures that are useful to a DISM API user, but do not neatly fit
/// into the managed, wrapped classes.
/// 
/// USAGE AND NOTES:
/// 
/// The standard usage of the DismApi begins with "DismApi.Initialize()" and ends with "DismApi.Shutdown()", wrapping the same standard functionality 
/// of the Microsoft DISM C library. However, this functionality is limited to the (local) version of dismapi.dll, located (in most cases) in \System32.
/// 
/// DISM has not always been built-in to Windows, however, and is also provided (as part of a stand-alone tool package) known as "Windows Assessment and Installation Kit" 
/// (or "WAIK", Windows 7 era), and now as "Windows Assessment and Deployment Kit" (or "WADK", Windows 8.0 - now.)
/// 
/// Users install WADK (and/or WAIK) tools to include standard Windows PE images, DISM tools, and other Windows management tooling.
/// 
/// In servicing images (online or .WIM), the built-in version of DISM (if even available) may not be able to service different version(s) of images.
/// For example, as of the Windows 10 Fall Creator's Update (DISM 10.0.16299.15), the built-in version of DISM cannot open a session against a Windows PE 4.0 image (based on Windows 8.0.)
/// I presume the same is true of Windows PE 5.0 (based on Windows 8.1.) Thus, in order to service different versions of images, the tooling of DISM from WADK/WAIK is necessary.
/// (Upon investigation, the DISM libraries included in WADK/WAIK include more libraries than what's included in Windows alone, including downleveling libraries, which are presumably for handling 
/// older/other versions/architectures, etc.)
/// 
/// WITH RESPECT TO DISM TOOLING:
/// 
/// 1.  Microsoft was calling the toolset "Windows Assessment and Installation Kit" as of Windows 7, but now calls it the "Windows Assessment and Deployment Kit" (starting with 8.0 and continuing on.)
/// 
/// 2.  WAIK (Windows 7) can be installed to provide Windows PE 3.0 (and DISM servicing), OR WAIK Service Pack 1 can be installed for Windows PE 3.1 (and DISM servicing), but both cannot be
///     installed at the same time.
///     
/// 3.  WADK (Windows 8.0) can be installed to provide support for Windows PE 4.0 (and DISM servicing), OR WADK (Windows 8.1) can be installed for Windows PE 5.0 (and DISM servicing), but both
///     cannot be installed at the same time.
///     
/// 4.  Only one version of WADK (Windows 10.x) can be installed to provide support for Windows PE 10 (and DISM servicing) at the same time.
/// 
/// 5.  Thus, a user could potentially have WAIK 3.0 or 3.1 installed, WADK 4.0 or 5.0 installed, and WADK 10 installed, all at the same time. In doing so, they could create and service Windows PE images
///     from Windows 7/7 SP1, 8/8.1, and 10.
///     
/// 6.  Regardless of what OS you (or a user) has, it is recommended to install the latest version of the WADK (10.x as of this writing), as the latest tooling should contain support for and be able to service older images.
///     Therefore, WADK 10.x (and it's DISM libraries) can service images of Windows 7, 8, 8.1, 10.x.
///     
/// LOADING A SPECIFIC DISM LIBRARY:
/// 
/// 1.  Use the Properties ("WADK10DISMAPIPath") to determine if said version of WADK/WAIK is installed, and if so, where the DISM API libraries are installed. 
///     "GetLatestDismGeneration()" will search all paths and provide the latest installed version, if available, as an enumeration.
///     
/// 2.  Call "LoadDismGenerationLibrary()" BEFORE you call DismApi.Initialize(). This works because we use the native "LoadLibrary()" method to load the specified DISM generational library into the process space,
///     and when you call "DismApi.Initialize()", the Marshal (via DllImport()) looks in process space first for a loaded module. Upon finding a module that matches the requested one's name, it then loads it 
///     (and any other associated libraries) as needed.
///     
/// 3.  After calling "DismApi.Shutdown()", call "UnloadDismGenerationLibrary()" to remove the loaded DISM library from the process space. 
/// 
/// 4.  In this fashion, one could load DISM from WADK 10, unload it, load DISM from WAIK, unload it, load DISM from the System, etc.
/// 
/// 5.  This method of loading/unloading DISM generation libraries supports ANYCPU, X86, and AMD64, and also supports the CLR "Prefer 32-bit" setting.
*/

using Microsoft.Win32;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Microsoft.Dism
{
    /// <summary>
    /// Represents "generational" versions of Deployment Imaging and Servicing Management.
    /// </summary>
    public enum DismGeneration
    {
        /// <summary>
        /// DISM libraries associated with WAIK and/or WADK were not found.
        /// </summary>
        NotFound = 0,

        /// <summary>
        /// DISM associated with the Windows 7 or Windows 7 Service Pack 1 version of the Windows Assessment and Installation Kit (WAIK).
        /// With respect to Windows PE, this would be 3.0 (Windows 7) or 3.1 (Windows 7 Service Pack 1).
        /// </summary>
        Win7,

        /// <summary>
        /// DISM associated with the Windows 8 version of the Windows Assessment and Deployment Kit (WADK).
        /// With respect to Windows PE, this would be 4.0 (Windows 8.0).
        /// </summary>
        Win8,

        /// <summary>
        /// DISM associated with the Windows 8.1 version of the Windows Assessment and Deployment Kit (WADK).
        /// With respect to Windows PE, this would be 5.0 (Windows 8.1).
        /// </summary>
        Win8_1,

        /// <summary>
        /// DISM associated with the Windows 10 version of the Windows Assessment and Deployment Kit (WADK).
        /// With respect to Windows PE, this would be 10.x (Windows 10.x).
        /// </summary>
        Win10
    }

    internal static class DismUtilities
    {
        /// <summary>
        /// Native methods necessary for manually loading and unloading Win32 libraries.
        /// </summary>
        internal static class NativeMethods
        {
            [DllImport("kernel32.dll")]
            public static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);

            [DllImport("kernel32.dll")]
            public static extern bool FreeLibrary(IntPtr hModule);
        }

        /// <summary>
        /// The handle of the loaded DISM generational library.
        /// </summary>
        private static IntPtr hDismApi;

        /// <summary>
        /// If installed, returns the file path of "dism.exe", which is the entry point for the DISM API in the Windows 7 generation of tools (WAIK).
        /// Otherwise, returns NULL.
        /// </summary>
        public static string WAIKDISMAPIPath
        {
            get
            {
                using (RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default).OpenSubKey("SOFTWARE\\Microsoft\\ComponentStudio\\6.1.7600.16385"))
                {
                    if (key?.GetValue("ServicingPath")?.ToString() == null)
                        return null;

                    string dismPath = Path.Combine(key.GetValue("ServicingPath").ToString(), "dism.exe");

                    return File.Exists(dismPath) ? dismPath : null;
                }
            }
        }

        /// <summary>
        /// If installed, returns the file path of "dismapi.dll", which is the entry point for the DISM API in the Windows 8 generation of tools (WADK).
        /// Otherwise, returns NULL.
        /// </summary>
        public static string WADK80DISMAPIPath
        {
            get
            {
                using (RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\Windows Kits\\Installed Roots"))
                {
                    if (key?.GetValue("KitsRoot")?.ToString() == null)
                        return null;

                    string dismPath = Path.Combine(
                        key.GetValue("KitsRoot").ToString(),
                        String.Format("Assessment and Deployment Kit\\Deployment Tools\\{0}\\DISM\\dismapi.dll", Environment.Is64BitProcess ? "amd64" : "x86")
                        );

                    return File.Exists(dismPath) ? dismPath : null;
                }
            }
        }

        /// <summary>
        /// If installed, returns the file path of "dismapi.dll", which is the entry point for the DISM API in the Windows 8.1 generation of tools (WADK).
        /// Otherwise, returns NULL.
        /// </summary>
        public static string WADK81DISMAPIPath
        {
            get
            {
                using (RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\Windows Kits\\Installed Roots"))
                {
                    if (key?.GetValue("KitsRoot81")?.ToString() == null)
                        return null;

                    string dismPath = Path.Combine(
                        key.GetValue("KitsRoot81").ToString(),
                        String.Format("Assessment and Deployment Kit\\Deployment Tools\\{0}\\DISM\\dismapi.dll", Environment.Is64BitProcess ? "amd64" : "x86")
                        );

                    return File.Exists(dismPath) ? dismPath : null;
                }
            }
        }

        /// <summary>
        /// If installed, returns the file path of "dismapi.dll", which is the entry point for the DISM API in the Windows 10.x generation of tools (WADK).
        /// Otherwise, returns NULL.
        /// </summary>
        public static string WADK10DISMAPIPath
        {
            get
            {
                using (RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\Windows Kits\\Installed Roots"))
                {
                    if (key?.GetValue("KitsRoot10")?.ToString() == null)
                        return null;

                    string dismPath = Path.Combine(
                        key.GetValue("KitsRoot10").ToString(),
                        String.Format("Assessment and Deployment Kit\\Deployment Tools\\{0}\\DISM\\dismapi.dll", Environment.Is64BitProcess ? "amd64" : "x86")
                        );

                    return File.Exists(dismPath) ? dismPath : null;
                }
            }
        }

        /// <summary>
        /// Loads the DISM API library associated with the provided DismGeneration. 
        /// NOTE: This must be called before calling DismApi.Initialize(), as the initialization takes precedence based on Dynamic Link Library loading.
        /// If a DismGeneration library has already been loaded when initialize() is called, that version of the DISM generation library is utilized. If no DISM API library
        /// is loaded when initialization is called, it will attempt to load the DISM API installed on the local system (System32), if available.
        /// Only a single DISM generation library may be loaded at a given time. To switch versions, the caller can use LoadDismGenerationLibrary() and UnloadDismGenerationLibrary()
        /// to switch between DISM generations (WAIK and/or WADK) and/or what's natively available on the local system (System32).
        /// </summary>
        /// <param name="generation">
        /// The DismGeneration to be loaded.
        /// </param>
        /// <returns>
        /// TRUE if successful, otherwise FALSE.
        /// </returns>
        public static bool LoadDismGenerationLibrary(DismGeneration generation)
        {
            if (hDismApi != IntPtr.Zero)
                return false;

            string dismApiPath = string.Empty;

            switch (generation)
            {
                case DismGeneration.Win10:
                    dismApiPath = WADK10DISMAPIPath;
                    break;

                case DismGeneration.Win8_1:
                    dismApiPath = WADK81DISMAPIPath;
                    break;

                case DismGeneration.Win8:
                    dismApiPath = WADK80DISMAPIPath;
                    break;

                case DismGeneration.Win7:
                    dismApiPath = WAIKDISMAPIPath;
                    break;

                case DismGeneration.NotFound:
                default:
                    return false;
            }

            return ((hDismApi = NativeMethods.LoadLibrary(dismApiPath)) != IntPtr.Zero ? true : false);
        }

        /// <summary>
        /// Returns a DismGeneration enumeration indicating the latest DISM generation installed and available on the local system.
        /// </summary>
        /// <returns></returns>
        public static DismGeneration GetLatestDismGeneration()
        {
            if (!String.IsNullOrEmpty(WADK10DISMAPIPath))
                return DismGeneration.Win10;
            else if (!String.IsNullOrEmpty(WADK81DISMAPIPath))
                return DismGeneration.Win8_1;
            else if (!String.IsNullOrEmpty(WADK80DISMAPIPath))
                return DismGeneration.Win8;
            else if (!String.IsNullOrEmpty(WADK80DISMAPIPath))
                return DismGeneration.Win7;
            else
                return DismGeneration.NotFound;
        }

        /// <summary>
        /// Unloads a previously-loaded DISM generation library.
        /// </summary>
        /// <returns>
        /// TRUE if successful, otherwise FALSE.
        /// </returns>
        public static bool UnloadDismGenerationLibrary()
        {
            if (hDismApi != IntPtr.Zero)
                NativeMethods.FreeLibrary(hDismApi);

            return hDismApi == IntPtr.Zero ? true : false;
        }
    }
}
