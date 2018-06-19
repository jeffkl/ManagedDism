// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Microsoft.Dism.Tests
{
    public class DismFeatureComparer : IComparer<DismFeature>, IComparer
    {
        public int Compare(DismFeature x, DismFeature y)
        {
            return x?.FeatureName == y?.FeatureName && x?.State == y?.State ? 0 : 1;
        }

        public int Compare(object x, object y)
        {
            return Compare((DismFeature)x, (DismFeature)y);
        }
    }

    public class DismPackageInfoTest : DismStructTest<DismPackageInfo>, IDisposable
    {
        private readonly List<DismApi.DismCustomProperty_> _customProperties = new List<DismApi.DismCustomProperty_>
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

        private readonly List<DismApi.DismFeature_> _features = new List<DismApi.DismFeature_>
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
        };

        private readonly DismApi.DismPackageInfo_ _packageInfo = new DismApi.DismPackageInfo_
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
        };

        public DismPackageInfoTest(TestWimTemplate template)
            : base(template)
        {
            _packageInfo.CustomProperty = ListToPtrArray(_customProperties);
            _packageInfo.CustomPropertyCount = (uint)_customProperties.Count;

            _packageInfo.Feature = ListToPtrArray(_features);
            _packageInfo.FeatureCount = (uint)_features.Count;
        }

        protected override DismPackageInfo Item => new DismPackageInfo(_packageInfo);

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
            item.Description.ShouldBe(_packageInfo.Description);
            item.DisplayName.ShouldBe(_packageInfo.DisplayName);
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

            item.CustomProperties.ShouldBe(new DismCustomPropertyCollection(_customProperties.Select(i => new DismCustomProperty(i)).ToList()));

            item.Features.ShouldBe(new DismFeatureCollection(_features.Select(i => new DismFeature(i)).ToList()));
        }
    }
}