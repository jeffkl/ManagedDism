// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Dism.Tests
{
    public class GetDriversTest : DismInstallWimTestBase
    {
        public GetDriversTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public void GetDriversFromOnlineSession()
        {
            using (DismSession session = DismApi.OpenOnlineSession())
            {
                DismDriverPackageCollection drivers = DismApi.GetDrivers(session, allDrivers: false);

                ValidateDrivers(drivers);
            }
        }

        [Fact]
        public void GetDriversFromWim()
        {
            DismDriverPackageCollection drivers = DismApi.GetDrivers(Session, allDrivers: true);

            ValidateDrivers(drivers);
        }

        private void ValidateDrivers(DismDriverPackageCollection drivers)
        {
            drivers.ShouldNotBeNull();

            drivers.Count.ShouldNotBe(0);

            foreach (DismDriverPackage driver in drivers)
            {
                driver.ClassDescription.ShouldNotBeNullOrWhiteSpace();
                driver.ClassGuid.ShouldNotBeNullOrWhiteSpace();
                driver.OriginalFileName.ShouldNotBeNullOrWhiteSpace();
            }
        }
    }
}