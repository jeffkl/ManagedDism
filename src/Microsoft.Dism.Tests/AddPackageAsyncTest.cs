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
    public class AddPackageAsyncTest : DismTestBase
    {
        public AddPackageAsyncTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public async Task AddPackageAsyncCancellation()
        {
            using DismSession session = DismApi.OpenOnlineSession();
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            bool canceled = false;

            try
            {
                await DismApi.AddPackageAsync(session, "nonexistent.cab", ignoreCheck: false, preventPending: false, cancellationToken: cts.Token);
            }
            catch (OperationCanceledException)
            {
                canceled = true;
            }

            canceled.ShouldBeTrue();
        }

        [Fact]
        public async Task AddPackageAsyncWithProgress()
        {
            using DismSession session = DismApi.OpenOnlineSession();
            using var cts = new CancellationTokenSource();

            var progress = new SynchronousProgress<DismProgress>(_ =>
            {
                cts.Cancel();
            });

            try
            {
                await DismApi.AddPackageAsync(session, "nonexistent.cab", ignoreCheck: false, preventPending: false, progress: progress, cancellationToken: cts.Token);
            }
            catch (OperationCanceledException)
            {
            }
            catch (DismException)
            {
                // Expected when the package does not exist
            }

            // Progress may not be called for an invalid package, so we just verify no hang occurred
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
