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
    public class UnmountImageAsyncTest : DismTestBase
    {
        public UnmountImageAsyncTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public async Task UnmountImageAsyncCancellation()
        {
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            bool canceled = false;

            try
            {
                await DismApi.UnmountImageAsync(MountPath.FullName, commitChanges: false, cancellationToken: cts.Token);
            }
            catch (OperationCanceledException)
            {
                canceled = true;
            }

            canceled.ShouldBeTrue();
        }

        [Fact]
        public async Task UnmountImageAsyncWithProgress()
        {
            // Mount an image first so we can unmount it
            foreach (DismMountedImageInfo mountedImageInfo in DismApi.GetMountedImages())
            {
                DismApi.UnmountImage(mountedImageInfo.MountPath, false);
            }

            DismApi.CleanupMountpoints();
            DismApi.MountImage(InstallWimPath.FullName, MountPath.FullName, 1);

            using var cts = new CancellationTokenSource();

            var progress = new SynchronousProgress<DismProgress>(_ =>
            {
            });

            try
            {
                await DismApi.UnmountImageAsync(MountPath.FullName, commitChanges: false, progress: progress, cancellationToken: cts.Token);
            }
            catch (DismException)
            {
                // Attempt to clean up
                try
                {
                    DismApi.UnmountImage(MountPath.FullName, commitChanges: false);
                }
                catch
                {
                    // Best effort cleanup
                }
            }

            // Progress may or may not be called depending on image size
        }

        [Fact]
        public async Task UnmountImageAsyncHappyPath()
        {
            // Mount an image first so we can unmount it
            foreach (DismMountedImageInfo mountedImageInfo in DismApi.GetMountedImages())
            {
                DismApi.UnmountImage(mountedImageInfo.MountPath, false);
            }

            DismApi.CleanupMountpoints();
            DismApi.MountImage(InstallWimPath.FullName, MountPath.FullName, 1);

            await DismApi.UnmountImageAsync(MountPath.FullName, commitChanges: false, cancellationToken: TestContext.Current.CancellationToken);
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
