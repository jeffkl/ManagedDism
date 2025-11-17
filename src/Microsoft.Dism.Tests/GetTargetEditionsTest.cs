// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Dism.Tests
{
    public class GetTargetEditionsTest : DismTestBase
    {
        public GetTargetEditionsTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public void GetTargetEditionsOnlineSession()
        {
            using DismSession session = DismApi.OpenOnlineSession();
            DismEditionCollection editionCollection = DismApi.GetTargetEditions(session);

            editionCollection.ShouldNotBeNull();
        }
    }
}
