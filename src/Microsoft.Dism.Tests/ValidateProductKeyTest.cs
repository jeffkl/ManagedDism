// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Dism.Tests
{
    public class ValidateProductKeyTest : DismTestBase
    {
        public ValidateProductKeyTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public void ValidateGenericProfessionalKey()
        {
            using (DismSession session = DismApi.OpenOnlineSession())
            {
                DismApi.ValidateProductKey(session, "VK7JG-NPHTM-C97JM-9MPGT-3V66T").ShouldBeTrue();
            }
        }

        [Fact]
        public void ValidateInvalidKey()
        {
            using (DismSession session = DismApi.OpenOnlineSession())
            {
                DismApi.ValidateProductKey(session, "AAAAA-BBBBB-CCCCC-DDDDD-EEEEE").ShouldBeFalse();
                DismApi.ValidateProductKey(session, "AAA-BBB-CCC-DDD-EEE").ShouldBeFalse();
            }
        }
    }
}
