// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Dism.Tests
{
    public class GetProductKeyInfoTest : DismTestBase
    {
        public GetProductKeyInfoTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public void GetProductKeyInfo()
        {
            using (DismSession session = DismApi.OpenOnlineSession())
            {
                DismProductKeyInfo productKeyInfo = DismApi.GetProductKeyInfo(session, "VK7JG-NPHTM-C97JM-9MPGT-3V66T");

                productKeyInfo.ShouldNotBeNull();
                productKeyInfo.EditionId.ShouldBe("Professional");
                productKeyInfo.Channel.ShouldBe("Retail");
            }
        }
    }
}
