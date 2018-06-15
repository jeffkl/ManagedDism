// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;
using System;

namespace Microsoft.Dism.Tests
{
    public class DismWimCustomizedInfoTest : DismStructTest<DismWimCustomizedInfo>
    {
        private readonly DismApi.DismWimCustomizedInfo_ _customizedInfo = new DismApi.DismWimCustomizedInfo_
        {
            CreatedTime = DateTime.Today.AddDays(-15),
            DirectoryCount = 123,
            FileCount = 456,
            ModifiedTime = DateTime.Today.AddDays(-1),
            Size = 789,
        };

        protected override DismWimCustomizedInfo Item => new DismWimCustomizedInfo(_customizedInfo);

        protected override object Struct => _customizedInfo;

        protected override void VerifyProperties(DismWimCustomizedInfo item)
        {
            item.CreatedTime.ShouldBe((DateTime)_customizedInfo.CreatedTime);
            item.DirectoryCount.ShouldBe(_customizedInfo.DirectoryCount);
            item.FileCount.ShouldBe(_customizedInfo.FileCount);
            item.ModifiedTime.ShouldBe((DateTime)_customizedInfo.ModifiedTime);
            item.Size.ShouldBe(_customizedInfo.Size);
        }
    }
}