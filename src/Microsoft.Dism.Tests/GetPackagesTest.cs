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
            using DismSession session = DismApi.OpenOnlineSession();
            DismPackageCollection packages = DismApi.GetPackages(session);

            packages.ShouldNotBeNull();

            foreach (DismPackage package in packages)
            {
                package.PackageName.ShouldNotBeNullOrWhiteSpace();
                package.PackageState.ShouldBeOneOf(Enum.GetValues(typeof(DismPackageFeatureState)).Cast<DismPackageFeatureState>().ToArray());
                package.ReleaseType.ShouldBeOneOf(Enum.GetValues(typeof(DismReleaseType)).Cast<DismReleaseType>().ToArray());

                GetFeaturesTest.ValidateFeatures(DismApi.GetFeaturesByPackageName(session, package.PackageName));
            }
        }
    }
}
