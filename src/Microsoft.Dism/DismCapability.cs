﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Dism
{
    public static partial class DismApi
    {
        /// <summary>
        /// Describes capability basic information.
        /// </summary>
        /// <remarks>
        /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/mt684921(v=vs.85).aspx"/>
        /// typedef struct _DismCapability {
        ///   PCWSTR Name;
        ///   DismPackageFeatureState State;
        /// } DismCapability;
        /// </remarks>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        internal struct DismCapability_
        {
            /// <summary>
            /// The manufacturer name of the driver.
            /// </summary>
            public string Name;

            /// <summary>
            /// A hardware description of the driver.
            /// </summary>
            public DismPackageFeatureState State;
        }
    }


    /// <summary>
    /// Describes capability basic information.
    /// </summary>
    public sealed class DismCapability : IEquatable<DismCapability>
    {
        private readonly DismApi.DismCapability_ _capability;

        internal DismCapability(IntPtr capabilityPtr)
            : this(capabilityPtr.ToStructure<DismApi.DismCapability_>())
        {
        }

        internal DismCapability(DismApi.DismCapability_ capability)
        {
            _capability = capability;
        }

        /// <summary>
        /// Gets the name of the capability.
        /// </summary>
        public string Name => _capability.Name;

        /// <summary>
        /// Gets the state of the capability.
        /// </summary>
        public DismPackageFeatureState State => _capability.State;

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as DismCapability);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">The <see cref="DismCapability"/> object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(DismCapability other)
        {
            return other != null && Name == other.Name && State == other.State;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return (String.IsNullOrEmpty(Name) ? 0 : Name.GetHashCode()) ^ State.GetHashCode();
        }
    }

    /// <summary>
    /// Represents a collection of <see cref="DismCapability"/> objects.
    /// </summary>
    public sealed class DismCapabilityCollection : DismCollection<DismCapability>
    {
        internal DismCapabilityCollection()
            :base(new List<DismCapability>())
        {
        }

        internal DismCapabilityCollection(IList<DismCapability> list)
            :base(list)
        {
        }
    }
}
