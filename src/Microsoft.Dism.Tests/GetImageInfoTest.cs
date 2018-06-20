// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Dism.Tests
{
    public class GetImageInfoTest : DismTestBase
    {
        public GetImageInfoTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public void GetImageInfoFromTestWim()
        {
            DismImageInfoCollection imageInfos = DismApi.GetImageInfo(Template.FullPath);

            imageInfos.Count.ShouldBe(2);

            foreach (DismImageInfo imageInfo in imageInfos)
            {
                imageInfo.Architecture.ShouldBe(TestWimTemplate.Architecture);
                imageInfo.DefaultLanguage.ShouldBe(TestWimTemplate.DefaultLangauge);
                imageInfo.EditionId.ShouldBe(TestWimTemplate.EditionId);
                imageInfo.ImageDescription.ShouldStartWith(TestWimTemplate.ImageNamePrefix);
                imageInfo.ImageName.ShouldStartWith(TestWimTemplate.ImageNamePrefix);
                imageInfo.InstallationType.ShouldBe(TestWimTemplate.InstallationType);
                imageInfo.ProductName.ShouldBe(TestWimTemplate.ProductName);
                imageInfo.ProductType.ShouldBe(TestWimTemplate.ProductType);
                imageInfo.ProductVersion.ShouldBe(TestWimTemplate.ProductVersion);
                imageInfo.SpLevel.ShouldBe(TestWimTemplate.SpLevel);
            }
        }
    }
}