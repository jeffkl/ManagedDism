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
        /// Describes basic information about a feature, such as the feature name and feature state.
        /// </summary>
        /// <remarks>
        /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824780.aspx"/>
        /// typedef struct _DismFeature
        /// {
        ///     PCWSTR FeatureName;
        ///     DismPackageFeatureState State;
        /// } DismFeature;
        /// </remarks>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        internal struct DismFeature_
        {
            /// <summary>
            /// The name of the feature.
            /// </summary>
            public string FeatureName;

            /// <summary>
            /// A valid DismPackageFeatureState Enumeration value such as DismStateInstalled.
            /// </summary>
            public DismPackageFeatureState State;
        }
    }

    /// <summary>
    /// Describes basic information about a feature, such as the feature name and feature state.
    /// </summary>
    public sealed class DismFeature : IEquatable<DismFeature>
    {
        private readonly DismApi.DismFeature_ _feature;

        /// <summary>
        /// Initializes a new instance of the <see cref="DismFeature"/> class.
        /// </summary>
        /// <param name="featurePtr">A native pointer to a DismFeature_ struct.</param>
        internal DismFeature(IntPtr featurePtr)
            : this(featurePtr.ToStructure<DismApi.DismFeature_>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DismFeature"/> class.
        /// </summary>
        /// <param name="feature">A native DismFeature_ struct to copy data from.</param>
        internal DismFeature(DismApi.DismFeature_ feature)
        {
            _feature = feature;
        }

        /// <summary>
        /// Gets the name of the feature.
        /// </summary>
        public string FeatureName => _feature.FeatureName;

        /// <summary>
        /// Gets the state of the feature.
        /// </summary>
        public DismPackageFeatureState State => _feature.State;

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as DismFeature);
        }

        /// <summary>
        /// Determines whether the specified <see cref="DismFeature"/> is equal to the current <see cref="DismFeature"/>.
        /// </summary>
        /// <param name="feature">The <see cref="DismFeature"/> object to compare with the current object.</param>
        /// <returns>true if the specified <see cref="DismFeature"/> is equal to the current <see cref="DismFeature"/>; otherwise, false.</returns>
        public bool Equals(DismFeature feature)
        {
            return feature != null && FeatureName == feature.FeatureName && State == feature.State;
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="T:System.Object"/>.</returns>
        public override int GetHashCode()
        {
            return (String.IsNullOrEmpty(FeatureName) ? 0 : FeatureName.GetHashCode()) ^ State.GetHashCode();
        }
    }

    /// <summary>
    /// Represents a collection of <see cref="DismFeature"/> objects.
    /// </summary>
    public sealed class DismFeatureCollection : DismCollection<DismFeature>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DismFeatureCollection"/> class.
        /// </summary>
        internal DismFeatureCollection()
            : base(new List<DismFeature>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DismFeatureCollection"/> class.
        /// </summary>
        /// <param name="list">An existing list of DismFeature objects to expose as a read-only collection.</param>
        internal DismFeatureCollection(IList<DismFeature> list)
            : base(list)
        {
        }
    }
}