// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using Xunit.Abstractions;

namespace Microsoft.Dism.Tests
{
    public abstract class DismInstallWimTestBase : DismTestBase
    {
        private readonly Lazy<DismSession> _session;

        protected DismInstallWimTestBase(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
            _session = new Lazy<DismSession>(GetSession, isThreadSafe: true);
        }

        protected DismSession Session => _session.Value;

        public override void Dispose()
        {
            if (_session.IsValueCreated)
            {
                _session.Value.Dispose();

                DismApi.UnmountImage(MountPath, commitChanges: false);
            }

            base.Dispose();
        }

        private DismSession GetSession()
        {
            foreach (DismMountedImageInfo mountedImageInfo in DismApi.GetMountedImages())
            {
                DismApi.UnmountImage(mountedImageInfo.MountPath, false);
            }

            DismApi.CleanupMountpoints();

            DismApi.MountImage(InstallWimPath, MountPath, 1);

            return DismApi.OpenOfflineSession(MountPath);
        }
    }
}