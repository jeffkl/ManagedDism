// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.IO;
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

        protected TestWimTemplate Template { get; }

        protected string TestDirectory { get; } = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}")).FullName;

        public virtual void Dispose()
        {
        }
    }
}