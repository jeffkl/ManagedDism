using Shouldly;
using System;
using System.Collections.Generic;

namespace Microsoft.Dism.Tests
{
    public class DismAppxPackageCollectionTest : DismCollectionTest<DismAppxPackageCollection, DismAppxPackage>
    {
        protected override DismAppxPackageCollection CreateCollection(List<DismAppxPackage> expectedCollection)
        {
            return new DismAppxPackageCollection(expectedCollection);
        }

        protected override DismAppxPackageCollection CreateCollection()
        {
            return new DismAppxPackageCollection();
        }

        protected override List<DismAppxPackage> GetCollection()
        {
            return new List<DismAppxPackage>
            {
                new DismAppxPackage(new DismApi.DismAppxPackage_
                {
                    Architecture = (uint)DismProcessorArchitecture.AMD64,
                    Build = 3,
                    DisplayName = "display name",
                    InstallLocation = @"C:\something",
                    MajorVersion = 1,
                    MinorVersion = 4,
                    PackageName = "package name",
                    PublisherId = "7017864D-7AEF-492B-96C4-6E776BFD9D65",
                    ResourceId = "6B46D32B-9786-4DD9-9168-088B1B91AFAF",
                    Revision = 2,
                })
            };
        }
    }

    public class DismAppxPackageTest : DismStructTest<DismAppxPackage>
    {
        private readonly DismApi.DismAppxPackage_ _appxPackage = new DismApi.DismAppxPackage_
        {
            Architecture = (uint)DismProcessorArchitecture.IA64,
            Build = 2,
            DisplayName = "A display name",
            InstallLocation = @"C:\Windows\System32\mypackage",
            MajorVersion = 4,
            MinorVersion = 1,
            PackageName = "Something",
            PublisherId = "0E243F30-AD22-42E2-B992-B88B4E0E408E",
            ResourceId = "8ADCC0DC-CC33-4B8A-B284-BEE31F83F10C",
            Revision = 3,
        };

        protected override DismAppxPackage Item => ItemPtr != IntPtr.Zero ? new DismAppxPackage(ItemPtr) : new DismAppxPackage(_appxPackage);
        protected override object Struct => _appxPackage;

        protected override void VerifyProperties(DismAppxPackage item)
        {
            item.Architecture.ShouldBe((DismProcessorArchitecture)_appxPackage.Architecture);
            item.DisplayName.ShouldBe(_appxPackage.DisplayName);
            item.InstallLocation.ShouldBe(_appxPackage.InstallLocation);
            item.PackageName.ShouldBe(_appxPackage.PackageName);
            item.PublisherId.ShouldBe(_appxPackage.PublisherId);
            item.ResourceId.ShouldBe(_appxPackage.ResourceId);
            item.Version.ShouldBe(new Version((int)_appxPackage.MajorVersion, (int)_appxPackage.MinorVersion, (int)_appxPackage.Build, (int)_appxPackage.Revision));
        }
    }
}