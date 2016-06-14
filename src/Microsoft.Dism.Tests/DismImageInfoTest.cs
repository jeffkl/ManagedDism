using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using NUnit.Framework;
using Shouldly;

namespace Microsoft.Dism.Tests
{
    [TestFixture]
    public class DismImageInfoCollectionTest : DismCollectionTest<DismImageInfoCollection, DismImageInfo>
    {
        protected override DismImageInfoCollection CreateCollection(List<DismImageInfo> expectedCollection)
        {
            return new DismImageInfoCollection(expectedCollection);
        }

        protected override DismImageInfoCollection CreateCollection()
        {
            return new DismImageInfoCollection();
        }

        protected override List<DismImageInfo> GetCollection()
        {
            return new List<DismImageInfo>
            {
                new DismImageInfo(new DismApi.DismImageInfo_
                {
                    Architecture = DismProcessorArchitecture.IA64,
                    Bootable = DismImageBootable.ImageBootableYes,
                    Build = 1000,
                    EditionId = "EditionId",
                    Hal = "Hal",
                    ImageDescription = "ImageDescription",
                    ImageIndex = 2,
                    ImageName = "ImageName",
                    ImageSize = 999,
                    ImageType = DismImageType.Wim,
                    InstallationType = "InstallationType",
                    MajorVersion = 2,
                    MinorVersion = 3,
                    ProductName = "ProductName",
                    ProductSuite = "ProductSuite",
                    ProductType = "ProductType",
                    SpBuild = 4,
                    SpLevel = 5,
                    SystemRoot = "SystemRoot",
                })
            };
        }
    }

    [TestFixture]
    public class DismImageInfoTest : DismStructTest<DismImageInfo>
    {
        private readonly DismApi.DismWimCustomizedInfo_ _wimCustomizedInfo = new DismApi.DismWimCustomizedInfo_
        {
            CreatedTime = DateTime.Today.AddDays(-7),
            DirectoryCount = 1234,
            FileCount = 5678,
            ModifiedTime = DateTime.Today,
            Size = 10,
        };

        private DismApi.DismImageInfo_ _imageInfo = new DismApi.DismImageInfo_
        {
            Architecture = DismProcessorArchitecture.IA64,
            Bootable = DismImageBootable.ImageBootableYes,
            Build = 1000,
            EditionId = "EditionId",
            Hal = "Hal",
            ImageDescription = "ImageDescription",
            ImageIndex = 2,
            ImageName = "ImageName",
            ImageSize = 999,
            ImageType = DismImageType.Wim,
            InstallationType = "InstallationType",
            MajorVersion = 2,
            MinorVersion = 3,
            ProductName = "ProductName",
            ProductSuite = "ProductSuite",
            ProductType = "ProductType",
            SpBuild = 4,
            SpLevel = 5,
            SystemRoot = "SystemRoot",
        };

        private readonly List<DismApi.DismLanguage> _languages = new List<DismApi.DismLanguage>
        {
            new DismApi.DismLanguage
            {
                Value = "en-us",
            },
            new DismApi.DismLanguage
            {
                Value = "es-es",
            },
        };

        protected override DismImageInfo Item => ItemPtr != IntPtr.Zero ? new DismImageInfo(ItemPtr) : new DismImageInfo(_imageInfo);

        protected override object Struct => _imageInfo;

        [OneTimeTearDown]
        public void TestCleanup()
        {
            Marshal.FreeHGlobal(_imageInfo.Language);

            Marshal.FreeHGlobal(_imageInfo.CustomizedInfo);
        }

        [OneTimeSetUp]
        public void TestInitialize()
        {
            _imageInfo.Language = ListToPtrArray(_languages);
            _imageInfo.LanguageCount = (uint)_languages.Count;
            _imageInfo.DefaultLanguageIndex = 1;

            _imageInfo.CustomizedInfo = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(DismApi.DismWimCustomizedInfo_)));

            Marshal.StructureToPtr(_wimCustomizedInfo, _imageInfo.CustomizedInfo, false);
        }

        protected override void VerifyProperties(DismImageInfo item)
        {
            IList<CultureInfo> languages = _languages.Select(i => new CultureInfo(i.Value)).ToList();

            item.Architecture.ShouldBe(_imageInfo.Architecture);
            item.Bootable.ShouldBe(_imageInfo.Bootable);
            item.ProductVersion.Build.ShouldBe((int)_imageInfo.Build);
            item.EditionId.ShouldBe(_imageInfo.EditionId);
            item.Hal.ShouldBe(_imageInfo.Hal);
            item.ImageDescription.ShouldBe(_imageInfo.ImageDescription);
            item.ImageIndex.ShouldBe((int)_imageInfo.ImageIndex);
            item.ImageName.ShouldBe(_imageInfo.ImageName);
            item.ImageSize.ShouldBe(_imageInfo.ImageSize);
            item.ImageType.ShouldBe(_imageInfo.ImageType);
            item.InstallationType.ShouldBe(_imageInfo.InstallationType);
            item.ProductVersion.Major.ShouldBe((int)_imageInfo.MajorVersion);
            item.ProductVersion.Minor.ShouldBe((int)_imageInfo.MinorVersion);
            item.ProductName.ShouldBe(_imageInfo.ProductName);
            item.ProductSuite.ShouldBe(_imageInfo.ProductSuite);
            item.ProductType.ShouldBe(_imageInfo.ProductType);
            item.ProductVersion.Revision.ShouldBe((int)_imageInfo.SpBuild);
            item.SpLevel.ShouldBe((int)_imageInfo.SpLevel);
            item.SystemRoot.ShouldBe(_imageInfo.SystemRoot);
            item.DefaultLanguageIndex.ShouldBe((int)_imageInfo.DefaultLanguageIndex);
            item.Languages.ShouldBe(_languages.Select(i => new CultureInfo(i.Value)));
            item.DefaultLanguage.ShouldBe(new CultureInfo(_languages[(int)_imageInfo.DefaultLanguageIndex]));

            item.CustomizedInfo.CreatedTime.ShouldBe((DateTime)_wimCustomizedInfo.CreatedTime);
            item.CustomizedInfo.DirectoryCount.ShouldBe(_wimCustomizedInfo.DirectoryCount);
            item.CustomizedInfo.FileCount.ShouldBe(_wimCustomizedInfo.FileCount);
            item.CustomizedInfo.ModifiedTime.ShouldBe((DateTime)_wimCustomizedInfo.ModifiedTime);
            item.CustomizedInfo.Size.ShouldBe(_wimCustomizedInfo.Size);
        }
    }
}