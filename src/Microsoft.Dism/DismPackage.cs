// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Dism
{
    public static partial class DismApi
    {
        /// <summary>
        /// Describes basic information about a package, including the date and time that the package was installed.
        /// </summary>
        /// <remarks>
        /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824763.aspx"/>
        /// typedef struct _DismPackage
        /// {
        ///     PCWSTR PackageName;
        ///     DismPackageFeatureState PackageState;
        ///     DismReleaseType ReleaseType;
        ///     SYSTEMTIME InstallTime;
        /// } DismPackage;
        /// </remarks>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        internal struct DismPackage_
        {
            /// <summary>
            /// The package name.
            /// </summary>
            public string PackageName;

            /// <summary>
            /// A DismPackageFeatureState Enumeration value, for example, DismStateResolved.
            /// </summary>
            public DismPackageFeatureState PackageState;

            /// <summary>
            /// A DismReleaseType Enumeration value, for example, DismReleaseTypeDriver.
            /// </summary>
            public DismReleaseType ReleaseType;

            /// <summary>
            /// The date and time that the package was installed. This field is local time relative to the servicing host computer.
            /// </summary>
            public SYSTEMTIME InstallTime;
        }
    }

    /// <summary>
    /// Represents basic information about a package, including the date and time that the package was installed.
    /// </summary>
    public sealed class DismPackage : IEquatable<DismPackage>
    {
        private readonly DismApi.DismPackage_ _package;

        /// <summary>
        /// Initializes a new instance of the <see cref="DismPackage"/> class.
        /// </summary>
        /// <param name="packagePtr">A pointer to a native DismPackage_ struct.</param>
        internal DismPackage(IntPtr packagePtr)
            : this(packagePtr.ToStructure<DismApi.DismPackage_>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DismPackage"/> class.
        /// </summary>
        /// <param name="package">A <see cref="DismApi.DismPackage_"/> structure.</param>
        internal DismPackage(DismApi.DismPackage_ package)
        {
            _package = package;
        }

        /// <summary>
        /// Gets the date and time the package was installed.
        /// </summary>
        public DateTime InstallTime => _package.InstallTime;

        /// <summary>
        /// Gets the package name.
        /// </summary>
        public string PackageName => _package.PackageName;

        /// <summary>
        /// Gets the state of the package.
        /// </summary>
        public DismPackageFeatureState PackageState => _package.PackageState;

        /// <summary>
        /// Gets the release type of the package.
        /// </summary>
        public DismReleaseType ReleaseType => _package.ReleaseType;

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as DismPackage);
        }

        /// <summary>
        /// Determines whether the specified <see cref="DismPackage"/> is equal to the current <see cref="DismPackage"/>.
        /// </summary>
        /// <param name="package">The <see cref="DismPackage"/> object to compare with the current object.</param>
        /// <returns>true if the specified <see cref="DismPackage"/> is equal to the current <see cref="DismPackage"/>; otherwise, false.</returns>
        public bool Equals(DismPackage package)
        {
            return package != null
                   && InstallTime == package.InstallTime
                   && PackageName == package.PackageName
                   && PackageState == package.PackageState
                   && ReleaseType == package.ReleaseType;
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="T:System.Object"/>.</returns>
        public override int GetHashCode()
        {
            return InstallTime.GetHashCode()
                   ^ (String.IsNullOrEmpty(PackageName) ? 0 : PackageName.GetHashCode())
                   ^ PackageState.GetHashCode()
                   ^ ReleaseType.GetHashCode();
        }
    }

    /// <summary>
    /// Represents a collection of <see cref="DismPackage"/> objects.
    /// </summary>
    public sealed class DismPackageCollection : DismCollection<DismPackage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DismPackageCollection"/> class.
        /// </summary>
        internal DismPackageCollection()
            : base(new List<DismPackage>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DismPackageCollection"/> class.
        /// </summary>
        /// <param name="list">An existing list of DismPackage objects to expose as a read-only collection.</param>
        internal DismPackageCollection(IList<DismPackage> list)
            : base(list)
        {
        }
    }
}