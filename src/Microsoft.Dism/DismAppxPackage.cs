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
#pragma warning disable SA1600 // Elements must be documented
        /// <summary>
        /// This struct is currently undocumented.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        internal struct DismAppxPackage_
        {
            [MarshalAs(UnmanagedType.LPWStr)]
            public string PackageName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string DisplayName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string PublisherId;
            public UInt32 MajorVersion;
            public UInt32 MinorVersion;
            public UInt32 Build;
            public UInt32 Revision;
            public UInt32 Architecture;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string ResourceId;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string InstallLocation;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string Regions;
        }
#pragma warning restore SA1600 // Elements must be documented
    }

    /// <summary>
    /// Represents information about an Appx package.
    /// </summary>
    public sealed class DismAppxPackage : IEquatable<DismAppxPackage>
    {
        private readonly DismApi.DismAppxPackage_ _appxPackage;

        /// <summary>
        /// Initializes a new instance of the <see cref="DismAppxPackage"/> class.
        /// </summary>
        /// <param name="appxPackagePtr">An <see cref="IntPtr"/> of a <see cref="DismApi.DismAppxPackage_"/> structure.</param>
        internal DismAppxPackage(IntPtr appxPackagePtr)
            : this(appxPackagePtr.ToStructure<DismApi.DismAppxPackage_>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DismAppxPackage"/> class.
        /// </summary>
        /// <param name="appxPackage">A <see cref="DismApi.DismAppxPackage_"/> structure.</param>
        internal DismAppxPackage(DismApi.DismAppxPackage_ appxPackage)
        {
            _appxPackage = appxPackage;

            Version = new Version((int)appxPackage.MajorVersion, (int)appxPackage.MinorVersion, (int)appxPackage.Build, (int)appxPackage.Revision);
        }

        /// <summary>
        /// Gets the architecture of the package.
        /// </summary>
        public DismProcessorArchitecture Architecture => (DismProcessorArchitecture)_appxPackage.Architecture;

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
        public Version Version { get; }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as DismAppxPackage);
        }

        /// <summary>
        /// Determines whether the specified <see cref="DismAppxPackage"/> is equal to the current <see cref="DismAppxPackage"/>.
        /// </summary>
        /// <param name="appxPackage">The <see cref="DismAppxPackage"/> object to compare with the current object.</param>
        /// <returns>true if the specified <see cref="DismAppxPackage"/> is equal to the current <see cref="DismAppxPackage"/>; otherwise, false.</returns>
        public bool Equals(DismAppxPackage appxPackage)
        {
            return appxPackage != null
                   && Architecture == appxPackage.Architecture
                   && DisplayName == appxPackage.DisplayName
                   && InstallLocation == appxPackage.InstallLocation
                   && PackageName == appxPackage.PackageName
                   && PublisherId == appxPackage.PublisherId
                   && ResourceId == appxPackage.ResourceId
                   && Version == appxPackage.Version;
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="T:System.Object"/>.</returns>
        public override int GetHashCode()
        {
            return Architecture.GetHashCode()
                   ^ DisplayName?.GetHashCode() ?? 0
                   ^ InstallLocation?.GetHashCode() ?? 0
                   ^ PackageName?.GetHashCode() ?? 0
                   ^ PublisherId?.GetHashCode() ?? 0
                   ^ ResourceId?.GetHashCode() ?? 0
                   ^ Version.GetHashCode();
        }
    }

    /// <summary>
    /// Represents a collection of <see cref="DismAppxPackage"/> objects.
    /// </summary>
    public sealed class DismAppxPackageCollection : DismCollection<DismAppxPackage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DismAppxPackageCollection"/> class.
        /// </summary>
        internal DismAppxPackageCollection()
            : base(new List<DismAppxPackage>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DismAppxPackageCollection"/> class.
        /// </summary>
        /// <param name="list">An <see cref="IList{DismAppxPackage}"/> to wrap.</param>
        internal DismAppxPackageCollection(IList<DismAppxPackage> list)
            : base(list)
        {
        }
    }
}