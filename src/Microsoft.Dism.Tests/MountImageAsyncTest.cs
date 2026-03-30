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
    public class MountImageAsyncTest : DismTestBase
    {
        public MountImageAsyncTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public async Task MountImageAsyncByIndexHappyPath()
        {
            foreach (DismMountedImageInfo mountedImageInfo in DismApi.GetMountedImages())
            {
                DismApi.UnmountImage(mountedImageInfo.MountPath, false);
            }

            DismApi.CleanupMountpoints();

            try
            {
                await DismApi.MountImageAsync(InstallWimPath.FullName, MountPath.FullName, imageIndex: 1, readOnly: true, options: DismMountImageOptions.None, progress: null, cancellationToken: CancellationToken.None);
            }
            finally
            {
                try
                {
                    DismApi.UnmountImage(MountPath.FullName, commitChanges: false);
                }
                catch
                {
                    // Best effort cleanup
                }
            }
        }

        [Fact]
        public async Task MountImageAsyncByIndexCancellation()
        {
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            bool canceled = false;

            try
            {
                await DismApi.MountImageAsync(InstallWimPath.FullName, MountPath.FullName, imageIndex: 1, readOnly: true, options: DismMountImageOptions.None, progress: null, cancellationToken: cts.Token);
            }
            catch (OperationCanceledException)
            {
                canceled = true;
            }

            canceled.ShouldBeTrue();
        }

        [Fact]
        public async Task MountImageAsyncByNameCancellation()
        {
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            bool canceled = false;

            try
            {
                await DismApi.MountImageAsync(InstallWimPath.FullName, MountPath.FullName, imageName: "FakeImage", readOnly: true, options: DismMountImageOptions.None, progress: null, cancellationToken: cts.Token);
            }
            catch (OperationCanceledException)
            {
                canceled = true;
            }

            canceled.ShouldBeTrue();
        }

        [Fact]
        public async Task MountImageAsyncWithProgress()
        {
            bool progressCalled = false;

            foreach (DismMountedImageInfo mountedImageInfo in DismApi.GetMountedImages())
            {
                DismApi.UnmountImage(mountedImageInfo.MountPath, false);
            }

            DismApi.CleanupMountpoints();

            using var cts = new CancellationTokenSource();

            var progress = new SynchronousProgress<DismProgress>(_ =>
            {
                progressCalled = true;
            });

            try
            {
                await DismApi.MountImageAsync(InstallWimPath.FullName, MountPath.FullName, imageIndex: 1, readOnly: true, options: DismMountImageOptions.None, progress: progress, cancellationToken: cts.Token);
            }
            finally
            {
                try
                {
                    DismApi.UnmountImage(MountPath.FullName, commitChanges: false);
                }
                catch
                {
                    // Best effort cleanup
                }
            }

            progressCalled.ShouldBeTrue();
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
