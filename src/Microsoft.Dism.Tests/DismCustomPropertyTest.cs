// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Dism.Tests
{
    public class DismCustomPropertyCollectionTest : DismCollectionTest<DismCustomPropertyCollection, DismCustomProperty>
    {
        private static readonly List<DismApi.DismCustomProperty_> Items = new List<DismApi.DismCustomProperty_>
        {
            new DismApi.DismCustomProperty_
            {
                Name = "Name1",
                Path = "Path1",
                Value = "Value1",
            },
            new DismApi.DismCustomProperty_
            {
                Name = "Name2",
                Path = "Path2",
                Value = "Value2",
            },
        };

        public DismCustomPropertyCollectionTest(TestWimTemplate template)
            : base(template)
        {
        }

        protected override IntPtr Pointer => Items.ToPtr();

        protected override DismCustomPropertyCollection GetActual(IntPtr pointer)
        {
            return new DismCustomPropertyCollection(pointer, (uint)Items.Count);
        }

        protected override ReadOnlyCollection<DismCustomProperty> GetExpected()
        {
            return new ReadOnlyCollection<DismCustomProperty>(Items.Select(i => new DismCustomProperty(i)).ToList());
        }
    }
}