// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Dism.Tests
{
    public class DismFeatureCollectionTest : DismCollectionTest<DismFeatureCollection, DismFeature>
    {
        private static readonly List<DismApi.DismFeature_> Items = new List<DismApi.DismFeature_>
        {
            new DismApi.DismFeature_
            {
                FeatureName = "FeatureName1",
                State = DismPackageFeatureState.PartiallyInstalled,
            },
            new DismApi.DismFeature_
            {
                FeatureName = "FeatureName2",
                State = DismPackageFeatureState.Superseded,
            },
        };

        public DismFeatureCollectionTest(TestWimTemplate template)
            : base(template)
        {
        }

        protected override IntPtr Pointer => Items.ToPtr();

        protected override DismFeatureCollection GetActual(IntPtr pointer)
        {
            return new DismFeatureCollection(pointer, (uint)Items.Count);
        }

        protected override ReadOnlyCollection<DismFeature> GetExpected()
        {
            return new ReadOnlyCollection<DismFeature>(Items.Select(i => new DismFeature(i)).ToList());
        }
    }
}