using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Microsoft.Dism.Tests
{
    public class DismFeatureInfoTest : DismStructTest<DismFeatureInfo>, IDisposable
    {
        private readonly List<DismApi.DismCustomProperty_> _customProperties = new List<DismApi.DismCustomProperty_>
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
        };

        private readonly DismApi.DismFeatureInfo_ _featureInfo = new DismApi.DismFeatureInfo_
        {
            Description = "CB45BE3A-10FC-49DC-A273-317ABBED46D9",
            DisplayName = "6411A803-B0CB-4570-9BE7-19644412B3C4",
            FeatureName = "8F24F552-3970-4EC1-A6CE-50C3A08C83CB",
            FeatureState = DismPackageFeatureState.Installed,
            RestartRequired = DismRestartType.Possible,
        };

        public DismFeatureInfoTest()
        {
            _featureInfo.CustomProperty = ListToPtrArray(_customProperties);

            _featureInfo.CustomPropertyCount = (uint)_customProperties.Count;
        }

        protected override DismFeatureInfo Item => ItemPtr != IntPtr.Zero ? new DismFeatureInfo(ItemPtr) : new DismFeatureInfo(_featureInfo);

        protected override object Struct => _featureInfo;

        public void Dispose()
        {
            Marshal.FreeHGlobal(_featureInfo.CustomProperty);
        }

        protected override void VerifyProperties(DismFeatureInfo item)
        {
            var customProperties = new DismCustomPropertyCollection(_customProperties.Select(i => new DismCustomProperty(i)).ToList());

            item.CustomProperties.ShouldBe(customProperties);

            item.Description.ShouldBe(_featureInfo.Description);
            item.DisplayName.ShouldBe(_featureInfo.DisplayName);
            item.FeatureName.ShouldBe(_featureInfo.FeatureName);
            item.FeatureState.ShouldBe(_featureInfo.FeatureState);
            item.RestartRequired.ShouldBe(_featureInfo.RestartRequired);
        }
    }
}