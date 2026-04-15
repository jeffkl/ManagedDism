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
    public class MountImageTest : DismTestBase
    {
        public MountImageTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public async Task MountImageAsyncByIndexWithCancellation()
        {
            using CancellationTokenSource cts = new();
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
        public async Task MountImageAsyncByNameWithCancellation()
        {
            using CancellationTokenSource cts = new();
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
        public async Task MountImageAsyncInstallWim()
        {
            await DismApi.MountImageAsync(InstallWimPath.FullName, MountPath.FullName, imageIndex: 1, readOnly: true, DismMountImageOptions.None, progress: null, TestContext.Current.CancellationToken);

            await DismApi.UnmountImageAsync(MountPath.FullName, commitChanges: false, progress: null, cancellationToken: TestContext.Current.CancellationToken);
        }

        [Fact]
        public async Task MountImageAsyncWithProgress()
        {
            bool progressCalled = false;

            using CancellationTokenSource cts = new();

            Progress<DismProgress> progress = new(_ =>
            {
                progressCalled = true;
            });

            await DismApi.MountImageAsync(InstallWimPath.FullName, MountPath.FullName, imageIndex: 1, readOnly: true, options: DismMountImageOptions.None, progress: progress, cancellationToken: cts.Token);

            try
            {
                progressCalled.ShouldBeTrue();
            }
            finally
            {
                await DismApi.UnmountImageAsync(MountPath.FullName, commitChanges: false, progress: null, TestContext.Current.CancellationToken);
            }
        }
    }
}
