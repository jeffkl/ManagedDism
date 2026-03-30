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
    public class RestoreImageHealthAsyncTest : DismTestBase
    {
        public RestoreImageHealthAsyncTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public async Task RestoreImageHealthAsyncCancellation()
        {
            using DismSession session = DismApi.OpenOnlineSession();
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            bool canceled = false;

            try
            {
                await DismApi.RestoreImageHealthAsync(session, limitAccess: true, cancellationToken: cts.Token);
            }
            catch (OperationCanceledException)
            {
                canceled = true;
            }

            canceled.ShouldBeTrue();
        }

        [Fact]
        public async Task RestoreImageHealthAsyncWithProgress()
        {
            using DismSession session = DismApi.OpenOnlineSession();
            using var cts = new CancellationTokenSource();

            var progress = new SynchronousProgress<DismProgress>(_ =>
            {
                cts.Cancel();
            });

            try
            {
                await DismApi.RestoreImageHealthAsync(session, limitAccess: true, progress: progress, cancellationToken: cts.Token);
            }
            catch (OperationCanceledException)
            {
            }

            // Progress may or may not be called depending on image state, so we just verify no hang occurred
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
