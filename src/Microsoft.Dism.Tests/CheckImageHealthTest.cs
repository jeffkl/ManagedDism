// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Dism.Tests
{
    public class CheckImageHealthTest : DismTestBase
    {
        public CheckImageHealthTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public void CheckImageHealthOnlineSession()
        {
            using (DismSession session = DismApi.OpenOnlineSession())
            {
                DismImageHealthState imageHealthState = DismApi.CheckImageHealth(session, scanImage: false, progressCallback: null, userData: null);

                imageHealthState.ShouldBe(DismImageHealthState.Healthy);
            }
        }

        [Fact]
        public void CheckImageHealthOnlineSessionWithCallback()
        {
            const string expectedUserData = "257E41FC307248608E28C3B1F8D5CF09";

            string userData = null;

            int current = -1;
            int total = -1;

            using (DismSession session = DismApi.OpenOnlineSession())
            {
                try
                {
                    DismApi.CheckImageHealth(
                        session: session,
                        scanImage: true, // Setting scanImage to true seems to trigger the callback being called
                        progressCallback: progress =>
                        {
                            userData = progress.UserData as string;
                            current = progress.Current;
                            total = progress.Total;

                            // Cancel the operation, otherwise it takes ~1 min
                            progress.Cancel = true;
                        },
                        userData: expectedUserData);
                }
                catch (OperationCanceledException)
                {
                }

                userData.ShouldBe(expectedUserData);
                current.ShouldBe(50);
                total.ShouldBeGreaterThanOrEqualTo(1000);
            }
        }
    }
}