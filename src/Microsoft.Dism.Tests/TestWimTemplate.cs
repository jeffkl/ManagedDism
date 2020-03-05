// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Microsoft.Wim;
using Shouldly;
using System;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using Xunit;

namespace Microsoft.Dism.Tests
{
    public class TestWimTemplate : IDisposable
    {
        public const DismProcessorArchitecture Architecture = DismProcessorArchitecture.AMD64;
        public const string EditionId = "WindowsPE";
        public const int FileCount = 10;
        public const int FileLineCount = 100;
        public const int ImageCount = 2;
        public const string ImageNamePrefix = "Test Image ";
        public const string InstallationType = "WindowsPE";
        public const string ProductName = "Microsoft® Windows® Operating System";
        public const string ProductType = "WinNT";
        public const int SpLevel = 0;
        public const string SystemRoot = "WINDOWS";
        public static readonly CultureInfo DefaultLangauge = new CultureInfo("en-US");
        public static readonly CultureInfo Language = new CultureInfo("en-US");
        public static readonly Version ProductVersion = new Version(6, 3, 9600, 16384);

        private const string TestWimTemplateFilename = @"test_template.wim";

        private readonly string _testWimTemplateDirectory = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}")).FullName;
        private readonly Lazy<string> _testWimTemplatePathLazy;
        private readonly string _testWimTempPath;

        public TestWimTemplate()
        {
            _testWimTempPath = Directory.CreateDirectory(Path.Combine(_testWimTemplateDirectory, "temp")).FullName;

            _testWimTemplatePathLazy = new Lazy<string>(CreateTemplateImage, isThreadSafe: true);
        }

        public string FullPath => _testWimTemplatePathLazy.Value;

        public static void CreateTestFiles(string path, int fileCount, int lineCount)
        {
            for (int i = 0; i < fileCount; i++)
            {
                string filePath = Path.Combine(path, $"TestFile{Guid.NewGuid()}.txt");

                using (StreamWriter fs = File.CreateText(filePath))
                {
                    for (int x = 0; x < lineCount; x++)
                    {
                        fs.WriteLine(Guid.NewGuid().ToString());
                    }
                }
            }
        }

        public void Dispose()
        {
            Directory.Delete(_testWimTemplateDirectory, recursive: true);
        }

        private string CaptureTemplateImage(string capturePath)
        {
            string imagePath = Path.Combine(_testWimTemplateDirectory, TestWimTemplateFilename);

            if (!Directory.Exists(capturePath))
            {
                throw new DirectoryNotFoundException(string.Format(CultureInfo.CurrentCulture, "Could not find part of the path '{0}'", capturePath));
            }

            XmlDocument xmlDocument = new XmlDocument
            {
                XmlResolver = null,
            };

            using (WimHandle wimHandle = WimgApi.CreateFile(imagePath, WimFileAccess.Write, WimCreationDisposition.CreateNew, WimCreateFileOptions.None, WimCompressionType.Lzx))
            {
                WimgApi.SetTemporaryPath(wimHandle, _testWimTempPath);

                for (int i = 0; i < ImageCount; i++)
                {
                    // ReSharper disable once UnusedVariable
                    using (WimHandle imageHandle = WimgApi.CaptureImage(wimHandle, capturePath, WimCaptureImageOptions.DisableDirectoryAcl | WimCaptureImageOptions.DisableFileAcl | WimCaptureImageOptions.DisableRPFix))
                    {
                    }
                }

                XPathNavigator xml = WimgApi.GetImageInformation(wimHandle).CreateNavigator();

                xml.ShouldNotBeNull();

                using (StringReader stringReader = new StringReader(xml.OuterXml))
                using (XmlTextReader reader = new XmlTextReader(stringReader)
                {
                    DtdProcessing = DtdProcessing.Prohibit,
                })
                {
                    xmlDocument.Load(reader);
                }

                XmlNodeList imageNodes = xmlDocument.SelectNodes("//WIM/IMAGE");

                imageNodes.ShouldNotBeNull();

                // ReSharper disable once PossibleNullReferenceException
                foreach (XmlElement imageNode in imageNodes)
                {
                    XmlDocumentFragment fragment = xmlDocument.CreateDocumentFragment();

                    fragment.InnerXml =
                        $@"<WINDOWS>
                              <ARCH>{Architecture:D}</ARCH>
                              <PRODUCTNAME>{ProductName}</PRODUCTNAME>
                              <EDITIONID>{EditionId}</EDITIONID>
                              <INSTALLATIONTYPE>{InstallationType}</INSTALLATIONTYPE>
                              <PRODUCTTYPE>{ProductType}</PRODUCTTYPE>
                              <PRODUCTSUITE></PRODUCTSUITE>
                              <LANGUAGES>
                                <LANGUAGE>{Language}</LANGUAGE>
                                <DEFAULT>{DefaultLangauge}</DEFAULT>
                              </LANGUAGES>
                              <VERSION>
                                <MAJOR>{ProductVersion.Major}</MAJOR>
                                <MINOR>{ProductVersion.Minor}</MINOR>
                                <BUILD>{ProductVersion.Build}</BUILD>
                                <SPBUILD>{ProductVersion.Revision}</SPBUILD>
                                <SPLEVEL>{SpLevel}</SPLEVEL>
                              </VERSION>
                              <SYSTEMROOT>{SystemRoot}</SYSTEMROOT>
                            </WINDOWS>";

                    imageNode.AppendChild(fragment);

                    fragment.InnerXml = $@"<NAME>{ImageNamePrefix}{imageNode.Attributes["INDEX"].Value}</NAME>";

                    imageNode.AppendChild(fragment);

                    fragment.InnerXml = $@"<DESCRIPTION>{ImageNamePrefix}{imageNode.Attributes["INDEX"].Value}</DESCRIPTION>";

                    imageNode.AppendChild(fragment);

                    WimgApi.SetImageInformation(wimHandle, xmlDocument);
                }
            }

            return imagePath;
        }

        private string CreateTemplateImage()
        {
            string capturePath = Directory.CreateDirectory(Path.Combine(_testWimTemplateDirectory, "capture")).FullName;

            CreateTestFiles(capturePath, FileCount, FileLineCount);
            try
            {
                return CaptureTemplateImage(capturePath);
            }
            finally
            {
                Directory.Delete(capturePath, recursive: true);
            }
        }
    }

    [CollectionDefinition(nameof(TestWimTemplate))]
    public class TestWimTemplateCollectionDefinition : ICollectionFixture<TestWimTemplate>
    {
    }
}