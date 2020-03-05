// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Dism.Tests
{
    public class DismCapabilityCollectionTest : DismCollectionTest<DismCapabilityCollection, DismCapability>
    {
        private static readonly List<DismApi.DismCapability_> Items = new List<DismApi.DismCapability_>
        {
            new DismApi.DismCapability_
            {
                Name = "CapabilityName1",
                State = DismPackageFeatureState.Installed,
            },
            new DismApi.DismCapability_
            {
                Name = "CapabilityName2",
                State = DismPackageFeatureState.Removed,
            },
        };

        public DismCapabilityCollectionTest(TestWimTemplate template)
            : base(template)
        {
        }

        protected override IntPtr Pointer => Items.ToPtr();

        protected override DismCapabilityCollection GetActual(IntPtr pointer)
        {
            return new DismCapabilityCollection(pointer, (uint)Items.Count);
        }

        protected override ReadOnlyCollection<DismCapability> GetExpected()
        {
            return new ReadOnlyCollection<DismCapability>(Items.Select(i => new DismCapability(i)).ToList());
        }
    }
}