// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Dism.Tests
{
    public class GetRegistryMountPointTest : DismInstallWimTestBase
    {
        public GetRegistryMountPointTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Theory]
        [InlineData(DismRegistryHive.Software)]
        [InlineData(DismRegistryHive.System)]
        [InlineData(DismRegistryHive.Security)]
        [InlineData(DismRegistryHive.SAM)]
        [InlineData(DismRegistryHive.Default)]
        [InlineData(DismRegistryHive.HKCU)]
        [InlineData(DismRegistryHive.Components)]
        [InlineData(DismRegistryHive.Drivers)]
        public void GetRegistryMountPointOfflineImage(DismRegistryHive dismRegistryHive)
        {
            string value = DismApi.GetRegistryMountPoint(Session, dismRegistryHive);

            value.ShouldNotBeNullOrWhiteSpace();
        }
    }
}
