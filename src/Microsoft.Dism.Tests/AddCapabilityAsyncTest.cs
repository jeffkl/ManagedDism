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
    public class AddCapabilityAsyncTest : DismTestBase
    {
        public AddCapabilityAsyncTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public async Task AddCapabilityAsyncCancellation()
        {
            using DismSession session = DismApi.OpenOnlineSession();
            using CancellationTokenSource cts = new();
            cts.Cancel();

            bool canceled = false;

            try
            {
                await DismApi.AddCapabilityAsync(session, "FakeCapability", limitAccess: true, sourcePaths: null, cancellationToken: cts.Token);
            }
            catch (OperationCanceledException)
            {
                canceled = true;
            }

            canceled.ShouldBeTrue();
        }

        [Fact]
        public async Task AddCapabilityAsyncWithProgress()
        {
            using DismSession session = DismApi.OpenOnlineSession();
            using CancellationTokenSource cts = new();

            SynchronousProgress<DismProgress> progress = new(_ =>
            {
                cts.Cancel();
            });

            try
            {
                await DismApi.AddCapabilityAsync(session, "FakeCapability", limitAccess: true, sourcePaths: null, progress: progress, cancellationToken: cts.Token);
            }
            catch (OperationCanceledException)
            {
            }
            catch (DismException)
            {
                // Expected when the capability does not exist
            }

            // Progress may not be called for an invalid capability, so we just verify no hang occurred
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
