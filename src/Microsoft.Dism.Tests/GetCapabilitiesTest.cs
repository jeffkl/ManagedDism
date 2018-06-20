// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Dism.Tests
{
    public class GetCapabilitiesTest : DismTestBase
    {
        public GetCapabilitiesTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public void GetCapabilitiesOnlineSession()
        {
            using (DismSession session = DismApi.OpenOnlineSession())
            {
                DismCapabilityCollection capabilities = DismApi.GetCapabilities(session);

                capabilities.Count.ShouldBeGreaterThan(0);

                foreach (DismCapability capability in capabilities)
                {
                    capability.Name.ShouldNotBeNullOrWhiteSpace();

                    capability.State.ShouldBeOneOf(DismPackageFeatureState.Installed, DismPackageFeatureState.NotPresent);
                }
            }
        }
    }
}