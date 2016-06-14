using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;

namespace Microsoft.Dism.Tests
{
    [TestFixture]
    public class DismMountedImageInfoCollectionTest : DismCollectionTest<DismMountedImageInfoCollection, DismMountedImageInfo>
    {
        protected override DismMountedImageInfoCollection CreateCollection(List<DismMountedImageInfo> expectedCollection)
        {
            return new DismMountedImageInfoCollection(expectedCollection);
        }

        protected override DismMountedImageInfoCollection CreateCollection()
        {
            return new DismMountedImageInfoCollection();
        }

        protected override List<DismMountedImageInfo> GetCollection()
        {
            return new List<DismMountedImageInfo>
            {
                new DismMountedImageInfo(new DismApi.DismMountedImageInfo_
                {
                   ImageFilePath = "ImageFilePath1",
                   ImageIndex = 1,
                   MountMode = DismMountMode.ReadOnly,
                   MountPath = "MountPath1",
                   MountStatus = DismMountStatus.Invalid,
                }),
                new DismMountedImageInfo(new DismApi.DismMountedImageInfo_
                {
                   ImageFilePath = "ImageFilePath2",
                   ImageIndex = 2,
                   MountMode = DismMountMode.ReadWrite,
                   MountPath = "MountPath2",
                   MountStatus = DismMountStatus.NeedsRemount,
                }),
                new DismMountedImageInfo(new DismApi.DismMountedImageInfo_
                {
                   ImageFilePath = "ImageFilePath3",
                   ImageIndex = 3,
                   MountMode = DismMountMode.ReadOnly,
                   MountPath = "MountPath3",
                   MountStatus = DismMountStatus.Ok,
                }),
            };
        }
    }

    [TestFixture]
    public class DismMountedImageInfoTest : DismStructTest<DismMountedImageInfo>
    {
        private readonly DismApi.DismMountedImageInfo_ _mountedImageInfo = new DismApi.DismMountedImageInfo_
        {
            ImageFilePath = "C17B915E-5753-4489-81B9-B75F293CCBBE",
            ImageIndex = 2,
            MountMode = DismMountMode.ReadWrite,
            MountPath = "BBCDB7C4-3A8D-4FCD-9DBD-23858A7C4730",
            MountStatus = DismMountStatus.NeedsRemount,
        };

        protected override DismMountedImageInfo Item => new DismMountedImageInfo(_mountedImageInfo);

        protected override object Struct => _mountedImageInfo;

        protected override void VerifyProperties(DismMountedImageInfo item)
        {
            item.ImageFilePath.ShouldBe(_mountedImageInfo.ImageFilePath);
            item.ImageIndex.ShouldBe((int)_mountedImageInfo.ImageIndex);
            item.MountMode.ShouldBe(_mountedImageInfo.MountMode);
            item.MountPath.ShouldBe(_mountedImageInfo.MountPath);
            item.MountStatus.ShouldBe(_mountedImageInfo.MountStatus);
        }
    }
}