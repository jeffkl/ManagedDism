// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Dism.Tests
{
    public class GetPackageInfoExTest : DismTestBase
    {
        public GetPackageInfoExTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public void GetPackageInfoExSimple()
        {
            using (DismSession session = DismApi.OpenOnlineSession())
            {
                DismPackage package = DismApi.GetPackages(session).FirstOrDefault(i => i.PackageState == DismPackageFeatureState.Installed);

                package.ShouldNotBeNull();

                DismPackageInfoEx packageInfoEx = DismApi.GetPackageInfoExByName(session, package.PackageName);

                packageInfoEx.CapabilityId.ShouldNotBeNullOrWhiteSpace();
            }
        }
    }
}