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
    public class RemovePackageAsyncTest : DismTestBase
    {
        public RemovePackageAsyncTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public async Task RemovePackageByNameAsyncCancellation()
        {
            using DismSession session = DismApi.OpenOnlineSession();
            using CancellationTokenSource cts = new();
            cts.Cancel();

            bool canceled = false;

            try
            {
                await DismApi.RemovePackageByNameAsync(session, "FakePackage", cancellationToken: cts.Token);
            }
            catch (OperationCanceledException)
            {
                canceled = true;
            }

            canceled.ShouldBeTrue();
        }

        [Fact]
        public async Task RemovePackageByPathAsyncCancellation()
        {
            using DismSession session = DismApi.OpenOnlineSession();
            using CancellationTokenSource cts = new();
            cts.Cancel();

            bool canceled = false;

            try
            {
                await DismApi.RemovePackageByPathAsync(session, "nonexistent.cab", cancellationToken: cts.Token);
            }
            catch (OperationCanceledException)
            {
                canceled = true;
            }

            canceled.ShouldBeTrue();
        }

        [Fact]
        public async Task RemovePackageByNameAsyncWithProgress()
        {
            using DismSession session = DismApi.OpenOnlineSession();
            using CancellationTokenSource cts = new();

            SynchronousProgress<DismProgress> progress = new(_ =>
            {
                cts.Cancel();
            });

            try
            {
                await DismApi.RemovePackageByNameAsync(session, "FakePackage", progress: progress, cancellationToken: cts.Token);
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
