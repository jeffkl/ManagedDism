// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using Xunit;

namespace Microsoft.Dism.Tests
{
    /// <summary>
    /// A base class for tests.
    /// </summary>
    [Collection(nameof(TestWimTemplate))]
    public abstract class TestBase : IDisposable
    {
        protected TestBase(TestWimTemplate template)
        {
            Template = template;
        }

        public TestWimTemplate Template { get; }

        public virtual void Dispose()
        {
        }
    }
}