// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

#if !NETFRAMEWORK
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.Dism.Tests
{
    public class CheckImageHealthAsyncTest : DismTestBase
    {
        public CheckImageHealthAsyncTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public async Task CheckImageHealthAsyncOnlineSession()
        {
            using DismSession session = DismApi.OpenOnlineSession();

            DismImageHealthState imageHealthState = await DismApi.CheckImageHealthAsync(session, scanImage: false, cancellationToken: TestContext.Current.CancellationToken);

            imageHealthState.ShouldBe(DismImageHealthState.Healthy);
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
        public async Task CheckImageHealthAsyncWithProgress()
        {
            int current = -1;
            int total = -1;

            using DismSession session = DismApi.OpenOnlineSession();
            using CancellationTokenSource cts = new();

            SynchronousProgress<DismProgress> progress = new(p =>
            {
                current = p.Current;
                total = p.Total;

                // Cancel the operation, otherwise it takes ~1 min
                cts.Cancel();
            });

            try
            {
                await DismApi.CheckImageHealthAsync(session, scanImage: true, progress: progress, cancellationToken: cts.Token);
            }
            catch (OperationCanceledException)
            {
            }

            // Verify that progress was reported at least once before cancellation took effect
            current.ShouldBeGreaterThanOrEqualTo(0);
            total.ShouldBeGreaterThan(0);
        }

        private sealed class SynchronousProgress<T> : IProgress<T>
        {
            private readonly Action<T> _handler;

            public SynchronousProgress(Action<T> handler)
            {
                _handler = handler;
            }

            public void Report(T value)
            {
                _handler(value);
            }
        }
    }
}
#endif
