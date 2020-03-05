// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.IO;
using System.Reflection;
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

            MountPath = Directory.CreateDirectory(Path.Combine(TestDirectory, "mount")).FullName;

            TestAssemblyDirectory = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);

            InstallWimPath = Path.Combine(TestAssemblyDirectory, "install.wim");
        }

        public string InstallWimPath { get; set; }

        public string MountPath { get; set; }

        public string TestAssemblyDirectory { get; set; }

        protected TestWimTemplate Template { get; }

        protected string TestDirectory { get; } = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}")).FullName;

        public virtual void Dispose()
        {
            Directory.Delete(TestDirectory, recursive: true);
        }
    }
}