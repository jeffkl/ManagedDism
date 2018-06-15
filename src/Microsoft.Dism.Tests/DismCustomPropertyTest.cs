// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;
using System;
using System.Collections.Generic;

namespace Microsoft.Dism.Tests
{
    public class DismCustomPropertyCollectionTest : DismCollectionTest<DismCustomPropertyCollection, DismCustomProperty>
    {
        protected override DismCustomPropertyCollection CreateCollection(List<DismCustomProperty> expectedCollection)
        {
            return new DismCustomPropertyCollection(expectedCollection);
        }

        protected override DismCustomPropertyCollection CreateCollection()
        {
            return new DismCustomPropertyCollection();
        }

        protected override List<DismCustomProperty> GetCollection()
        {
            return new List<DismCustomProperty>
            {
                new DismCustomProperty(new DismApi.DismCustomProperty_
                {
                    Name = "Name1",
                    Path = "Path1",
                    Value = "Value1",
                }),
                new DismCustomProperty(new DismApi.DismCustomProperty_
                {
                    Name = "Name2",
                    Path = "Path2",
                    Value = "Value2",
                }),
            };
        }
    }

    public class DismCustomPropertyTest : DismStructTest<DismCustomProperty>
    {
        private readonly DismApi.DismCustomProperty_ _customPropertyStruct = new DismApi.DismCustomProperty_()
        {
            Name = "6CE9BE8B-85A3-44D6-9297-8ED1636FD68C",
            Path = "B86BC312-CF9D-4AA2-85F9-9BFC4A76338B",
            Value = "A5801BF4-2907-4873-84E7-A378BA6712C2",
        };

        protected override DismCustomProperty Item => ItemPtr != IntPtr.Zero ? new DismCustomProperty(ItemPtr) : new DismCustomProperty(_customPropertyStruct);

        protected override object Struct => _customPropertyStruct;

        protected override void VerifyProperties(DismCustomProperty customProperty)
        {
            customProperty.Name.ShouldBe(_customPropertyStruct.Name);
            customProperty.Path.ShouldBe(_customPropertyStruct.Path);
            customProperty.Value.ShouldBe(_customPropertyStruct.Value);
        }
    }
}