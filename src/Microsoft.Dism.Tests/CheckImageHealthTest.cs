// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;

using System;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace Microsoft.Dism.Tests
{
    public class CheckImageHealthTest : DismTestBase
    {
        public CheckImageHealthTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public async Task CheckImageHealthAsyncCancellation()
        {
            using DismSession session = DismApi.OpenOnlineSession();
            using CancellationTokenSource cts = new();

            cts.Cancel();

            bool canceled = false;

            try
            {
                await DismApi.CheckImageHealthAsync(session, scanImage: false, cancellationToken: cts.Token);
            }
            catch (OperationCanceledException)
            {
                canceled = true;
            }

            canceled.ShouldBeTrue();
        }

        [Fact]
        public async Task CheckImageHealthAsyncOnlineSession()
        {
            using DismSession session = DismApi.OpenOnlineSession();

            DismImageHealthState imageHealthState = await DismApi.CheckImageHealthAsync(session, scanImage: false, cancellationToken: TestContext.Current.CancellationToken);

            imageHealthState.ShouldBe(DismImageHealthState.Healthy);
        }

        [Fact]
        public async Task CheckImageHealthAsyncWithProgress()
        {
            int current = -1;
            int total = -1;

            using DismSession session = DismApi.OpenOnlineSession();
            using CancellationTokenSource cts = new();

            Progress<DismProgress> progress = new(dismProgress =>
            {
                current = dismProgress.Current;
                total = dismProgress.Total;

                dismProgress.Cancel = true; // Cancel the operation, otherwise it takes a few minutes
            });

            try
            {
                await DismApi.CheckImageHealthAsync(session, scanImage: true, progress: progress, cancellationToken: cts.Token);
            }
            catch (OperationCanceledException)
            {
            }

            current.ShouldBeGreaterThan(-1);
            total.ShouldBeGreaterThan(-1);
        }

        [Fact]
        public void CheckImageHealthOnlineSession()
        {
            using DismSession session = DismApi.OpenOnlineSession();
            DismImageHealthState imageHealthState = DismApi.CheckImageHealth(session, scanImage: false, progressCallback: null, userData: null);

            imageHealthState.ShouldBe(DismImageHealthState.Healthy);
        }

        [Fact]
        public void CheckImageHealthOnlineSessionWithCallback()
        {
            const string expectedUserData = "257E41FC307248608E28C3B1F8D5CF09";

            string? userData = null;

            int current = -1;
            int total = -1;

            using DismSession session = DismApi.OpenOnlineSession();
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
