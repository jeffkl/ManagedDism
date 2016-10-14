using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Dism.Tests
{
    [TestFixture]
    public class DismCapabilityCollectionTest : DismCollectionTest<DismCapabilityCollection, DismCapability>
    {
        protected override DismCapabilityCollection CreateCollection(List<DismCapability> expectedCollection)
        {
            return new DismCapabilityCollection(expectedCollection);
        }

        protected override DismCapabilityCollection CreateCollection()
        {
            return new DismCapabilityCollection();
        }

        protected override List<DismCapability> GetCollection()
        {
            return new List<DismCapability>
            {
                new DismCapability(new DismApi.DismCapability_
                {
                   Name = "CapabilityName1",
                   State = DismPackageFeatureState.Installed
                }),
                new DismCapability(new DismApi.DismCapability_
                {
                   Name = "CapabilityName2",
                   State = DismPackageFeatureState.Removed
                })
            };
        }
    }

    [TestFixture]
    public class DismCapabilityTest : DismStructTest<DismCapability>
    {
        private readonly DismApi.DismCapability_ _capability = new DismApi.DismCapability_
        {
            Name = "BDC1F89D-EA9D-44AF-AB49-11414B1700D0",
            State = DismPackageFeatureState.Removed
        };

        protected override DismCapability Item => new DismCapability(_capability);

        protected override object Struct => _capability;

        protected override void VerifyProperties(DismCapability item)
        {
            item.Name.ShouldBe(_capability.Name);
            item.State.ShouldBe(_capability.State);
        }
    }
}
