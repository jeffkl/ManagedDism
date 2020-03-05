// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Dism.Tests
{
    public class GetPackagesTest : DismTestBase
    {
        public GetPackagesTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public void GetPackagesSimple()
        {
            using (DismSession session = DismApi.OpenOnlineSession())
            {
                DismPackageCollection packages = DismApi.GetPackages(session);

                packages.ShouldNotBeNull();

                foreach (DismPackage package in packages)
                {
                    package.PackageName.ShouldNotBeNullOrWhiteSpace();
                    package.PackageState.ShouldBeOneOf(Enum.GetValues(typeof(DismPackageFeatureState)).Cast<DismPackageFeatureState>().ToArray());
                    package.ReleaseType.ShouldBeOneOf(Enum.GetValues(typeof(DismReleaseType)).Cast<DismReleaseType>().ToArray());

                    switch (package.PackageState)
                    {
                        case DismPackageFeatureState.InstallPending:
                        case DismPackageFeatureState.NotPresent:
                        case DismPackageFeatureState.Staged:
                            package.InstallTime.ShouldBe(DateTime.MinValue, () => $"{package.PackageName} / {package.PackageState} / {package.ReleaseType}");
                            break;
                        case DismPackageFeatureState.Installed:
                        case DismPackageFeatureState.PartiallyInstalled:
                        case DismPackageFeatureState.Resolved:
                        case DismPackageFeatureState.UninstallPending:
                            package.InstallTime.ShouldBeGreaterThan(DateTime.MinValue, () => $"{package.PackageName} / {package.PackageState} / {package.ReleaseType}");
                            break;
                        case DismPackageFeatureState.Superseded:
                            break;
                    }

                    GetFeaturesTest.ValidateFeatures(DismApi.GetFeaturesByPackageName(session, package.PackageName));
                }
            }
        }
    }
}
