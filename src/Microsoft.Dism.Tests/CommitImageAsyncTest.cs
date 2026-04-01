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
    public class CommitImageAsyncTest : DismInstallWimTestBase
    {
        public CommitImageAsyncTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public async Task CommitImageAsyncCancellation()
        {
            using CancellationTokenSource cts = new();
            cts.Cancel();

            bool canceled = false;

            try
            {
                await DismApi.CommitImageAsync(Session, discardChanges: false, cancellationToken: cts.Token);
            }
            catch (OperationCanceledException)
            {
                canceled = true;
            }

            canceled.ShouldBeTrue();
        }

        [Fact]
        public async Task CommitImageAsyncWithProgress()
        {
            using CancellationTokenSource cts = new();

            SynchronousProgress<DismProgress> progress = new(_ =>
            {
                cts.Cancel();
            });

            try
            {
                await DismApi.CommitImageAsync(Session, discardChanges: false, progress: progress, cancellationToken: cts.Token);
            }
            catch (OperationCanceledException)
            {
            }

            // Progress may or may not be called depending on whether changes exist to commit
        }

        [Fact]
        public async Task CommitImageAsyncHappyPath()
        {
            await DismApi.CommitImageAsync(Session, discardChanges: false, cancellationToken: TestContext.Current.CancellationToken);
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
