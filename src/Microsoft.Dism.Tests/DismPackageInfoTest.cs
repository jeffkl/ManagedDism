// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Dism.Tests
{
    public class DismPackageInfoTest : DismStructTest<DismPackageInfo>
    {
        private readonly DismApi.DismPackageInfo_ _packageInfo = new DismApi.DismPackageInfo_
        {
            Applicable = true,
            Company = "Company",
            Copyright = "Copyright",
            CreationTime = DateTime.Today.AddDays(-5),
            CustomProperty = new List<DismApi.DismCustomProperty_>
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
            }.ToPtr(),
            CustomPropertyCount = 2,
            Description = "Description",
            DisplayName = "DisplayName",
            Feature = new List<DismApi.DismFeature_>
            {
                new DismApi.DismFeature_
                {
                    FeatureName = "FeatureName1",
                    State = DismPackageFeatureState.Installed,
                },
                new DismApi.DismFeature_
                {
                    FeatureName = "FeatureName2",
                    State = DismPackageFeatureState.Superseded,
                },
            }.ToPtr(),
            FeatureCount = 2,
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
        };

        public DismPackageInfoTest(TestWimTemplate template)
            : base(template)
        {
        }

        protected override DismPackageInfo Item => new DismPackageInfo(ItemPtr);

        protected override object Struct => _packageInfo;

        public override void Dispose()
        {
            Marshal.FreeHGlobal(_packageInfo.CustomProperty);

            Marshal.FreeHGlobal(_packageInfo.Feature);

            base.Dispose();
        }

        protected override void VerifyProperties(DismPackageInfo item)
        {
            item.Applicable.ShouldBe(_packageInfo.Applicable);
            item.Company.ShouldBe(_packageInfo.Company);
            item.Copyright.ShouldBe(_packageInfo.Copyright);
            item.CreationTime.ShouldBe((DateTime)_packageInfo.CreationTime);
            item.CustomProperties.ShouldBe(new DismCustomPropertyCollection(_packageInfo.CustomProperty, _packageInfo.CustomPropertyCount));
            item.Description.ShouldBe(_packageInfo.Description);
            item.DisplayName.ShouldBe(_packageInfo.DisplayName);
            item.Features.ShouldBe(new DismFeatureCollection(_packageInfo.Feature, _packageInfo.FeatureCount));
            item.FullyOffline.ShouldBe(_packageInfo.FullyOffline);
            item.InstallClient.ShouldBe(_packageInfo.InstallClient);
            item.InstallPackageName.ShouldBe(_packageInfo.InstallPackageName);
            item.InstallTime.ShouldBe((DateTime)_packageInfo.InstallTime);
            item.LastUpdateTime.ShouldBe((DateTime)_packageInfo.LastUpdateTime);
            item.PackageName.ShouldBe(_packageInfo.PackageName);
            item.PackageState.ShouldBe(_packageInfo.PackageState);
            item.ProductVersion.ShouldBe(_packageInfo.ProductVersion);
            item.ReleaseType.ShouldBe(_packageInfo.ReleaseType);
            item.RestartRequired.ShouldBe(_packageInfo.RestartRequired);
            item.SupportInformation.ShouldBe(_packageInfo.SupportInformation);
        }
    }
}