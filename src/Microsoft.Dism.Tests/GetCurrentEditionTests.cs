// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;
using Xunit;

namespace Microsoft.Dism.Tests
{
    public class GetCurrentEditionTests : DismInstallWimTestBase
    {
        public GetCurrentEditionTests(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public void GetCurrentEditionFromOnlineSession()
        {
            using DismSession session = DismApi.OpenOnlineSession();
            string edition = DismApi.GetCurrentEdition(session);

            edition.ShouldNotBeNullOrWhiteSpace();
        }
    }
}
