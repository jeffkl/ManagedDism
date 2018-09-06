// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;
using System;

namespace Microsoft.Dism.Tests
{
    public class DismPackageInfoExTest : DismStructTest<DismPackageInfoEx>
    {
        private readonly DismApi.DismPackageInfoEx_ _packageInfo = new DismApi.DismPackageInfoEx_
        {
            PackageInfo = new DismApi.DismPackageInfo_
            {
                Applicable = true,
                Company = "Company",
                Copyright = "Copyright",
                CreationTime = DateTime.Today.AddDays(-5),
                Description = "Description",
                DisplayName = "DisplayName",
                FullyOffline = DismFullyOfflineInstallableType.FullyOfflineInstallable,
                InstallClient = "InstallClient",
                InstallPackageName = "InstallPackageName",
                InstallTime = DateTime.Today.AddDays(-2),
                LastUpdateTime = DateTime.Today.AddDays(-1),
                PackageName = "PackageName",
                PackageState = DismPackageFeatureState.Staged,
                ProductName = "ProductName",
                ProductVersion = "1.2.3.4",
                ReleaseType = DismReleaseType.ServicePack,
                RestartRequired = DismRestartType.Required,
                SupportInformation = "SupportInformation",
            },
            CapabilityId = "5F27812AB5FE4461AE74036F1C562EF8"
        };

        public DismPackageInfoExTest(TestWimTemplate template)
            : base(template)
        {
        }

        protected override DismPackageInfoEx Item => new DismPackageInfoEx(_packageInfo);

        protected override object Struct => _packageInfo;

        protected override void VerifyProperties(DismPackageInfoEx item)
        {
            item.Applicable.ShouldBe(_packageInfo.PackageInfo.Applicable);
            item.Company.ShouldBe(_packageInfo.PackageInfo.Company);
            item.Copyright.ShouldBe(_packageInfo.PackageInfo.Copyright);
            item.CreationTime.ShouldBe((DateTime)_packageInfo.PackageInfo.CreationTime);
            item.Description.ShouldBe(_packageInfo.PackageInfo.Description);
            item.DisplayName.ShouldBe(_packageInfo.PackageInfo.DisplayName);
            item.FullyOffline.ShouldBe(_packageInfo.PackageInfo.FullyOffline);
            item.InstallClient.ShouldBe(_packageInfo.PackageInfo.InstallClient);
            item.InstallPackageName.ShouldBe(_packageInfo.PackageInfo.InstallPackageName);
            item.InstallTime.ShouldBe((DateTime)_packageInfo.PackageInfo.InstallTime);
            item.LastUpdateTime.ShouldBe((DateTime)_packageInfo.PackageInfo.LastUpdateTime);
            item.PackageName.ShouldBe(_packageInfo.PackageInfo.PackageName);
            item.PackageState.ShouldBe(_packageInfo.PackageInfo.PackageState);
            item.ProductVersion.ShouldBe(_packageInfo.PackageInfo.ProductVersion);
            item.ReleaseType.ShouldBe(_packageInfo.PackageInfo.ReleaseType);
            item.RestartRequired.ShouldBe(_packageInfo.PackageInfo.RestartRequired);
            item.SupportInformation.ShouldBe(_packageInfo.PackageInfo.SupportInformation);

            item.CapabilityId.ShouldBe(_packageInfo.CapabilityId);
        }
    }
}