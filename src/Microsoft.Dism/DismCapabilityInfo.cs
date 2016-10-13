using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Dism
{
    public static partial class DismApi
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        internal struct DismCapabilityInfo_
        {
            public string Name;
            public DismPackageFeatureState State;
            public string DisplayName;
            public string Description;
            public UInt32 DownloadSize;
            public UInt32 InstallSize;
        }
    }

    public sealed class DismCapabilityInfo : IEquatable<DismCapabilityInfo>
    {
        private readonly DismApi.DismCapabilityInfo_ _capabilityInfo;

        internal DismCapabilityInfo(IntPtr capabilityPtr)
            : this(capabilityPtr.ToStructure<DismApi.DismCapabilityInfo_>())
        {
        } 

        internal DismCapabilityInfo(DismApi.DismCapabilityInfo_ capabilityInfo)
        {
            _capabilityInfo = capabilityInfo;
        }

        public string Name => _capabilityInfo.Name;

        public DismPackageFeatureState State => _capabilityInfo.State;

        public string DisplayName => _capabilityInfo.DisplayName;

        public string Description => _capabilityInfo.Description;

        public int DownloadSize => (int)_capabilityInfo.DownloadSize;

        public int InstallSize => (int)_capabilityInfo.InstallSize;

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as DismCapabilityInfo);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(DismCapabilityInfo other)
        {
            return other != null
                && Name == other.Name
                && State == other.State
                && DisplayName == other.DisplayName
                && Description == other.Description
                && DownloadSize == other.DownloadSize
                && InstallSize == other.InstallSize;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return (String.IsNullOrEmpty(Name) ? 0 : Name.GetHashCode())
                ^ State.GetHashCode()
                ^ (String.IsNullOrEmpty(DisplayName) ? 0 : DisplayName.GetHashCode())
                ^ (String.IsNullOrEmpty(Description) ? 0 : Description.GetHashCode())
                ^ DownloadSize
                ^ InstallSize;
        }
    }
}
