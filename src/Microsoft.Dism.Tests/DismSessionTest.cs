// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Dism.Tests
{
    public class DismSessionTest : DismTestBase
    {
        public DismSessionTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public void SessionOptionsDefaults()
        {
            DismSessionOptions options = new DismSessionOptions();

            options.ThrowExceptionOnRebootRequired.ShouldBeTrue();
        }

        [Theory]
        [ClassData(typeof(SessionOptionsBehaviorData))]
        public void SessionOptionsBehavior(DismSessionOptions options, Func<DismSessionOptions, DismSession> sessionFunc)
        {
            using (DismSession session = sessionFunc(options))
            {
                session.Options.ShouldNotBeNull();
                session.RebootRequired.ShouldBeFalse();

                if (options == null)
                {
                    session.Options.ThrowExceptionOnRebootRequired.ShouldBeTrue();
                }
                else
                {
                    session.Options.ShouldBe(options);
                }

                DismUtilities.ThrowIfFail(DismApi.ERROR_SUCCESS, session);
                session.RebootRequired.ShouldBeFalse();

                if (session.Options.ThrowExceptionOnRebootRequired)
                {
                    Should.Throw<DismRebootRequiredException>(() => DismUtilities.ThrowIfFail(DismApi.ERROR_SUCCESS_REBOOT_REQUIRED, session));
                }
                else
                {
                    DismUtilities.ThrowIfFail(DismApi.ERROR_SUCCESS_REBOOT_REQUIRED, session);
                }

                session.RebootRequired.ShouldBeTrue();

                session.RebootRequired = false;
                session.RebootRequired.ShouldBeTrue();
            }
        }

        private class SessionOptionsBehaviorData : TheoryData<DismSessionOptions?, Func<DismSessionOptions, DismSession>>
        {
            public SessionOptionsBehaviorData()
            {
                Add(null, (_) => DismApi.OpenOnlineSession());
                Add(null, (_) => DismApi.OpenOfflineSession(DismApi.DISM_ONLINE_IMAGE));
                Add(null, (_) => DismApi.OpenOfflineSession(DismApi.DISM_ONLINE_IMAGE, windowsDirectory: null, systemDrive: null));

                Add(null, (options) => DismApi.OpenOnlineSessionEx(options));
                Add(null, (options) => DismApi.OpenOfflineSessionEx(DismApi.DISM_ONLINE_IMAGE, options));
                Add(null, (options) => DismApi.OpenOfflineSessionEx(DismApi.DISM_ONLINE_IMAGE, windowsDirectory: null, systemDrive: null, options));

                Add(new DismSessionOptions(), (options) => DismApi.OpenOnlineSessionEx(options));
                Add(new DismSessionOptions(), (options) => DismApi.OpenOfflineSessionEx(DismApi.DISM_ONLINE_IMAGE, options));
                Add(new DismSessionOptions(), (options) => DismApi.OpenOfflineSessionEx(DismApi.DISM_ONLINE_IMAGE, windowsDirectory: null, systemDrive: null, options));

                Add(new DismSessionOptions { ThrowExceptionOnRebootRequired = false }, (options) => DismApi.OpenOnlineSessionEx(options));
                Add(new DismSessionOptions { ThrowExceptionOnRebootRequired = false }, (options) => DismApi.OpenOfflineSessionEx(DismApi.DISM_ONLINE_IMAGE, options));
                Add(new DismSessionOptions { ThrowExceptionOnRebootRequired = false }, (options) => DismApi.OpenOfflineSessionEx(DismApi.DISM_ONLINE_IMAGE, windowsDirectory: null, systemDrive: null, options));
            }
        }
    }
}
