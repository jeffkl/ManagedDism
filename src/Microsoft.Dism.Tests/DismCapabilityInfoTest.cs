using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Dism.Tests
{
    [TestFixture]
    public class DismCapabilityInfoTest : DismStructTest<DismCapabilityInfo>
    {
        private DismApi.DismCapabilityInfo_ _capabilityInfo = new DismApi.DismCapabilityInfo_
        {
            Name = "6CDAF47E-B7D8-4A46-9B83-E13CDA1706A7",
            DisplayName = "6411A803-B0CB-4570-9BE7-19644412B3C4",
            Description = "CB45BE3A-10FC-49DC-A273-317ABBED46D9",
            State = DismPackageFeatureState.Installed,
            DownloadSize = 1000,
            InstallSize = 2000
        };

        protected override DismCapabilityInfo Item => ItemPtr != IntPtr.Zero ? new DismCapabilityInfo(ItemPtr) : new DismCapabilityInfo(_capabilityInfo);

        protected override object Struct => _capabilityInfo;

        protected override void VerifyProperties(DismCapabilityInfo item)
        {
            item.Name.ShouldBe(_capabilityInfo.Name);
            item.DisplayName.ShouldBe(_capabilityInfo.DisplayName);
            item.Description.ShouldBe(_capabilityInfo.Description);
            item.State.ShouldBe(_capabilityInfo.State);
            item.DownloadSize.ShouldBe((int)_capabilityInfo.DownloadSize);
            item.InstallSize.ShouldBe((int)_capabilityInfo.InstallSize);
        }
    }
}
