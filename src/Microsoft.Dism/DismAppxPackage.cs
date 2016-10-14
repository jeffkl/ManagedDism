using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Dism
{
    public static partial class DismApi
    {
        /// <summary>
        /// This struct is currently undocumented.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        internal struct DismAppxPackage_
        {
            [MarshalAs(UnmanagedType.LPWStr)] public string PackageName;
            [MarshalAs(UnmanagedType.LPWStr)] public string DisplayName;
            [MarshalAs(UnmanagedType.LPWStr)] public string PublisherId;
            public UInt32 MajorVersion;
            public UInt32 MinorVersion;
            public UInt32 Build;
            public UInt32 Revision;
            public UInt32 Architecture;
            [MarshalAs(UnmanagedType.LPWStr)] public string ResourceId;
            [MarshalAs(UnmanagedType.LPWStr)] public string InstallLocation;
        }
    }

    /// <summary>
    /// Represents information about an Appx package.
    /// </summary>
    public sealed class DismAppxPackage
    {
        private readonly DismApi.DismAppxPackage_ _appxPackage;

        internal DismAppxPackage(IntPtr appxPackagePtr)
            : this(appxPackagePtr.ToStructure<DismApi.DismAppxPackage_>())
        {
        }

        internal DismAppxPackage(DismApi.DismAppxPackage_ appxPackage)
        {
            _appxPackage = appxPackage;

            Version = new Version((int) appxPackage.MajorVersion, (int) appxPackage.MinorVersion,
                (int) appxPackage.Build, (int) appxPackage.Revision);
        }

        /// <summary>
        /// Get the architecture of the package.
        /// </summary>
        public DismProcessorArchitecture Architecture => (DismProcessorArchitecture) _appxPackage.Architecture;

        /// <summary>
        /// Gets the display name of the package.
        /// </summary>
        public string DisplayName => _appxPackage.DisplayName;

        /// <summary>
        /// Gets the installation path of the package.
        /// </summary>
        public string InstallLocation => _appxPackage.InstallLocation;

        /// <summary>
        /// Gets the name of the package.
        /// </summary>
        public string PackageName => _appxPackage.PackageName;

        /// <summary>
        /// Gets the publisher identifier of the package.
        /// </summary>
        public string PublisherId => _appxPackage.PublisherId;

        /// <summary>
        /// Gets the resource identifier of the package.
        /// </summary>
        public string ResourceId => _appxPackage.ResourceId;

        /// <summary>
        /// Gets the version of the package.
        /// </summary>
        public Version Version { get; private set; }
    }

    /// <summary>
    /// Represents a collection of <see cref="DismAppxPackage"/> objects.
    /// </summary>
    public sealed class DismAppxPackageCollection : DismCollection<DismAppxPackage>
    {
        /// <summary>
        /// Initializes a new instance of the DismDriverPackageCollection class.
        /// </summary>
        internal DismAppxPackageCollection()
            : base(new List<DismAppxPackage>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the DismDriverPackageCollection class.
        /// </summary>
        /// <param name="list"></param>
        internal DismAppxPackageCollection(IList<DismAppxPackage> list)
            : base(list)
        {
        }
    }
}