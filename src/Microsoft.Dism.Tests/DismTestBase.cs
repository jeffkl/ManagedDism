// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System.IO;
using Xunit.Abstractions;

namespace Microsoft.Dism.Tests
{
    public abstract class DismTestBase : TestBase
    {
        private readonly FileInfo _logFilePath;
        private readonly ITestOutputHelper _testOutput;

        protected DismTestBase(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template)
        {
            _testOutput = testOutput;

            _logFilePath = new FileInfo(Path.Combine(TestDirectory.FullName, "dism.log"));

            DismApi.Initialize(DismLogLevel.LogErrorsWarningsInfo, _logFilePath.FullName);
        }

        public override void Dispose()
        {
            DismApi.Shutdown();

            _testOutput.WriteLine(File.ReadAllText(_logFilePath.FullName));

            base.Dispose();
        }
    }
}