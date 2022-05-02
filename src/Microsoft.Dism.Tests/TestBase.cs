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

            MountPath = Directory.CreateDirectory(Path.Combine(TestDirectory.FullName, "mount"));

            InstallWimPath = new FileInfo(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "install.wim"));
        }

        public FileInfo InstallWimPath { get; set; }

        public DirectoryInfo MountPath { get; set; }

        protected TestWimTemplate Template { get; }

        protected DirectoryInfo TestDirectory { get; } = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}"));

        public virtual void Dispose()
        {
            TestDirectory.Delete(recursive: true);
        }
    }
}