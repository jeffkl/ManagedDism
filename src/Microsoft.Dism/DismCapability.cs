using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

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

        public string Name => _capability.Name;

        public DismPackageFeatureState State => _capability.State;

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as DismCapability);
        }

        public bool Equals(DismCapability other)
        {
            return other != null && Name == other.Name && State == other.State;
        }

        public override int GetHashCode()
        {
            return (String.IsNullOrEmpty(Name) ? 0 : Name.GetHashCode()) ^ State.GetHashCode();
        }
    }

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
