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

        private class SessionOptionsBehaviorData : TheoryData<DismSessionOptions, Func<DismSessionOptions, DismSession>>
        {
            private readonly List<DismSessionOptions> _sessionOptions = new List<DismSessionOptions>
            {
                null,
                new DismSessionOptions(),
                new DismSessionOptions { ThrowExceptionOnRebootRequired = false },
            };

            private readonly List<Func<DismSessionOptions, DismSession>> _publicSessionMethods = new List<Func<DismSessionOptions, DismSession>>
            {
                (_) => DismApi.OpenOnlineSession(),
                (_) => DismApi.OpenOfflineSession(DismApi.DISM_ONLINE_IMAGE),
                (_) => DismApi.OpenOfflineSession(DismApi.DISM_ONLINE_IMAGE, null, null),
            };

            private readonly List<Func<DismSessionOptions, DismSession>> _publicSessionExMethods = new List<Func<DismSessionOptions, DismSession>>
            {
                (options) => DismApi.OpenOnlineSessionEx(options),
                (options) => DismApi.OpenOfflineSessionEx(DismApi.DISM_ONLINE_IMAGE, options),
                (options) => DismApi.OpenOfflineSessionEx(DismApi.DISM_ONLINE_IMAGE, null, null, options),
            };

            public SessionOptionsBehaviorData()
            {
                foreach (var sessionMethod in _publicSessionMethods)
                {
                    Add(null, sessionMethod);
                }

                foreach (DismSessionOptions options in _sessionOptions)
                {
                    foreach (var sessionMethodEx in _publicSessionExMethods)
                    {
                        Add(options, sessionMethodEx);
                    }
                }
            }
        }
    }
}
