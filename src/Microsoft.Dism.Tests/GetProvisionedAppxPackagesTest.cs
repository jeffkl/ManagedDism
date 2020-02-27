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
    public class GetProvisionedAppxPackagesTest : DismTestBase
    {
        public GetProvisionedAppxPackagesTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public void GetProvisionedAppxPackagesSimple()
        {
            using (DismSession onlineSession = DismApi.OpenOnlineSession())
            {
                DismAppxPackageCollection packages = DismApi.GetProvisionedAppxPackages(onlineSession);

                packages.ShouldNotBeNull();

                foreach (DismAppxPackage package in packages)
                {
                    package.Architecture.ShouldBeOneOf(Enum.GetValues(typeof(DismProcessorArchitecture)).Cast<DismProcessorArchitecture>().ToArray());
                    package.DisplayName.ShouldNotBeNullOrWhiteSpace();
                    package.InstallLocation.ShouldNotBeNullOrWhiteSpace();
                    package.PackageName.ShouldNotBeNullOrWhiteSpace();
                    package.PublisherId.ShouldNotBeNullOrWhiteSpace();
                    package.ResourceId.ShouldNotBeNull();
                    package.Version.ShouldBeGreaterThan(Version.Parse("0.0.0.0"));
                }
            }
        }
    }
}
