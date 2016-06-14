using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;

namespace Microsoft.Dism.Tests
{
    [TestFixture]
    public class DismDriverPackageCollectionTest : DismCollectionTest<DismDriverPackageCollection, DismDriverPackage>
    {
        protected override DismDriverPackageCollection CreateCollection(List<DismDriverPackage> expectedCollection)
        {
            return new DismDriverPackageCollection(expectedCollection);
        }

        protected override DismDriverPackageCollection CreateCollection()
        {
            return new DismDriverPackageCollection();
        }

        protected override List<DismDriverPackage> GetCollection()
        {
            return new List<DismDriverPackage>
            {
                new DismDriverPackage(new DismApi.DismDriverPackage_
                {
                   BootCritical = true,
                   Build = 1000,
                   CatalogFile = "CatalogFile",
                   ClassDescription = "ClassDescription",
                   ClassGuid = "ClassGuid",
                   ClassName = "ClassName",
                   Date = DateTime.Today,
                   DriverSignature = DismDriverSignature.Signed,
                   InBox = true,
                   MajorVersion = 1,
                   MinorVersion = 0,
                   OriginalFileName = "OriginalFileName",
                   ProviderName = "ProviderName",
                   PublishedName = "PublishedName",
                   Revision = 2,
                }),
            };
        }
    }

    [TestFixture]
    public class DismDriverPackageTest : DismStructTest<DismDriverPackage>
    {
        private readonly DismApi.DismDriverPackage_ _driverPackage = new DismApi.DismDriverPackage_
        {
            BootCritical = true,
            Build = 1000,
            CatalogFile = "13AA07FF-1F49-4C8B-9A76-11A87F4F5A5B",
            ClassDescription = "D76A8CCC-362F-4796-A88A-E1B5E2645188",
            ClassGuid = "7F794C19-FA72-4200-8F9F-3E1383902C5A",
            ClassName = "8E27AEEE-A8E5-47CA-9E8F-9B39D369339C",
            Date = DateTime.Today,
            DriverSignature = DismDriverSignature.Signed,
            InBox = true,
            MajorVersion = 1,
            MinorVersion = 2,
            OriginalFileName = "C95C1E65-3E4E-4CEE-8C4B-C8BF733D92DB",
            ProviderName = "8A996476-6623-4377-ACBD-438657AE052B",
            PublishedName = "C5D23D3E-C66F-454D-8093-E585B38B7AA6",
            Revision = 3,
        };

        protected override DismDriverPackage Item => ItemPtr != IntPtr.Zero ? new DismDriverPackage(ItemPtr) : new DismDriverPackage(_driverPackage);

        protected override object Struct => _driverPackage;

        protected override void VerifyProperties(DismDriverPackage item)
        {
            item.BootCritical.ShouldBe(_driverPackage.BootCritical);
            item.Version.Build.ShouldBe((int)_driverPackage.Build);
            item.CatalogFile.ShouldBe(_driverPackage.CatalogFile);
            item.ClassDescription.ShouldBe(_driverPackage.ClassDescription);
            item.ClassGuid.ShouldBe(_driverPackage.ClassGuid);
            item.ClassName.ShouldBe(_driverPackage.ClassName);
            item.Date.ShouldBe((DateTime)_driverPackage.Date);
            item.DriverSignature.ShouldBe(_driverPackage.DriverSignature);
            item.InBox.ShouldBe(_driverPackage.InBox);
            item.Version.Major.ShouldBe((int)_driverPackage.MajorVersion);
            item.Version.Minor.ShouldBe((int)_driverPackage.MinorVersion);
            item.OriginalFileName.ShouldBe(_driverPackage.OriginalFileName);
            item.ProviderName.ShouldBe(_driverPackage.ProviderName);
            item.PublishedName.ShouldBe(_driverPackage.PublishedName);
            item.Version.Revision.ShouldBe((int)_driverPackage.Revision);
        }
    }
}