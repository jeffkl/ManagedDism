// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Dism.Tests
{
    public class DismFeatureInfoTest : DismStructTest<DismFeatureInfo>
    {
        private readonly DismApi.DismFeatureInfo_ _featureInfo = new DismApi.DismFeatureInfo_
        {
            CustomProperty = new List<DismApi.DismCustomProperty_>
            {
                new DismApi.DismCustomProperty_
                {
                    Name = "Property1",
                    Path = "Path1",
                    Value = "Value1",
                },
                new DismApi.DismCustomProperty_
                {
                    Name = "Property2",
                    Path = "Path2",
                    Value = "Value2",
                },
            }.ToPtr(),
            CustomPropertyCount = 2,
            Description = "CB45BE3A-10FC-49DC-A273-317ABBED46D9",
            DisplayName = "6411A803-B0CB-4570-9BE7-19644412B3C4",
            FeatureName = "8F24F552-3970-4EC1-A6CE-50C3A08C83CB",
            FeatureState = DismPackageFeatureState.Installed,
            RestartRequired = DismRestartType.Possible,
        };

        public DismFeatureInfoTest(TestWimTemplate template)
            : base(template)
        {
        }

        protected override DismFeatureInfo Item => new DismFeatureInfo(ItemPtr);

        protected override object Struct => _featureInfo;

        public override void Dispose()
        {
            Marshal.FreeHGlobal(_featureInfo.CustomProperty);

            base.Dispose();
        }

        protected override void VerifyProperties(DismFeatureInfo item)
        {
            item.CustomProperties.ShouldBe(new DismCustomPropertyCollection(_featureInfo.CustomProperty, _featureInfo.CustomPropertyCount));
            item.Description.ShouldBe(_featureInfo.Description);
            item.DisplayName.ShouldBe(_featureInfo.DisplayName);
            item.FeatureName.ShouldBe(_featureInfo.FeatureName);
            item.FeatureState.ShouldBe(_featureInfo.FeatureState);
            item.RestartRequired.ShouldBe(_featureInfo.RestartRequired);
        }
    }
}