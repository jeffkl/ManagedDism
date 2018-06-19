// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.IO;
using Xunit;

namespace Microsoft.Dism.Tests
{
    public class DismInitializeTest : TestBase
    {
        public DismInitializeTest(TestWimTemplate template)
            : base(template)
        {
        }

        [Theory]
        [InlineData(DismLogLevel.LogErrors)]
        [InlineData(DismLogLevel.LogErrorsWarnings)]
        [InlineData(DismLogLevel.LogErrorsWarningsInfo)]
        public void InitializeExSimple(DismLogLevel logLevel)
        {
            DismApi.InitializeEx(logLevel);

            try
            {
            }
            finally
            {
                DismApi.Shutdown();
            }
        }

        [Fact]
        public void InitializeExWithLogFilePath()
        {
            string logFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.log");

            DismApi.InitializeEx(DismLogLevel.LogErrors, logFilePath);

            try
            {
            }
            finally
            {
                DismApi.Shutdown();

                if (File.Exists(logFilePath))
                {
                    File.Delete(logFilePath);
                }
            }
        }

        [Fact]
        public void InitializeExWithScratchDirectory()
        {
            FileInfo logFile = new FileInfo(Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.log"));
            DirectoryInfo scratchDirectory = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N")));

            DismApi.InitializeEx(DismLogLevel.LogErrors, logFile.FullName, scratchDirectory.FullName);

            try
            {
            }
            finally
            {
                DismApi.Shutdown();

                if (logFile.Exists)
                {
                    logFile.Delete();
                }

                if (scratchDirectory.Exists)
                {
                    scratchDirectory.Delete(recursive: true);
                }
            }
        }

        [Theory]
        [InlineData(DismLogLevel.LogErrors)]
        [InlineData(DismLogLevel.LogErrorsWarnings)]
        [InlineData(DismLogLevel.LogErrorsWarningsInfo)]
        public void InitializeSimple(DismLogLevel logLevel)
        {
            DismApi.Initialize(logLevel);

            try
            {
            }
            finally
            {
                DismApi.Shutdown();
            }
        }

        [Fact]
        public void InitializeWithLogFilePath()
        {
            string logFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.log");

            DismApi.Initialize(DismLogLevel.LogErrors, logFilePath);

            try
            {
            }
            finally
            {
                DismApi.Shutdown();

                if (File.Exists(logFilePath))
                {
                    File.Delete(logFilePath);
                }
            }
        }

        [Fact]
        public void InitializeWithScratchDirectory()
        {
            FileInfo logFile = new FileInfo(Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.log"));
            DirectoryInfo scratchDirectory = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N")));

            DismApi.Initialize(DismLogLevel.LogErrors, logFile.FullName, scratchDirectory.FullName);

            try
            {
            }
            finally
            {
                DismApi.Shutdown();

                if (logFile.Exists)
                {
                    logFile.Delete();
                }

                if (scratchDirectory.Exists)
                {
                    scratchDirectory.Delete(recursive: true);
                }
            }
        }
    }
}