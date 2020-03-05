// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;

namespace Microsoft.Dism.Tests
{
    public class DismImageInfoCollectionTest : DismCollectionTest<DismImageInfoCollection, DismImageInfo>
    {
        private readonly List<DismApi.DismImageInfo_> _items = new List<DismApi.DismImageInfo_>
        {
            new DismApi.DismImageInfo_
            {
                Architecture = DismProcessorArchitecture.IA64,
                Bootable = DismImageBootable.ImageBootableYes,
                Build = 1000,
                CustomizedInfo = new DismApi.DismWimCustomizedInfo_
                {
                    CreatedTime = DateTime.Today.AddDays(-7),
                    DirectoryCount = 1234,
                    FileCount = 5678,
                    ModifiedTime = DateTime.Today,
                    Size = 10,
                }.ToPtr(),
                EditionId = "EditionId",
                Hal = "Hal",
                ImageDescription = "ImageDescription",
                ImageIndex = 2,
                ImageName = "ImageName",
                ImageSize = 999,
                ImageType = DismImageType.Wim,
                InstallationType = "InstallationType",
                Language = new List<DismApi.DismLanguage>
                {
                    new DismApi.DismLanguage("en-us"),
                    new DismApi.DismLanguage("es-es"),
                }.ToPtr(),
                LanguageCount = 2,
                MajorVersion = 2,
                MinorVersion = 3,
                ProductName = "ProductName",
                ProductSuite = "ProductSuite",
                ProductType = "ProductType",
                SpBuild = 4,
                SpLevel = 5,
                SystemRoot = "SystemRoot",
            },
            new DismApi.DismImageInfo_
            {
                Architecture = DismProcessorArchitecture.IA64,
                Bootable = DismImageBootable.ImageBootableYes,
                Build = 1000,
                CustomizedInfo = new DismApi.DismWimCustomizedInfo_
                {
                    CreatedTime = DateTime.Today.AddDays(-1),
                    DirectoryCount = 456,
                    FileCount = 789,
                    ModifiedTime = DateTime.Today,
                    Size = 13,
                }.ToPtr(),
                EditionId = "EditionId",
                Hal = "Hal",
                ImageDescription = "ImageDescription",
                ImageIndex = 2,
                ImageName = "ImageName",
                ImageSize = 999,
                ImageType = DismImageType.Wim,
                InstallationType = "InstallationType",
                Language = new List<DismApi.DismLanguage>
                {
                    new DismApi.DismLanguage("es-es"),
                }.ToPtr(),
                LanguageCount = 1,
                MajorVersion = 2,
                MinorVersion = 3,
                ProductName = "ProductName",
                ProductSuite = "ProductSuite",
                ProductType = "ProductType",
                SpBuild = 4,
                SpLevel = 5,
                SystemRoot = "SystemRoot",
            },
        };

        public DismImageInfoCollectionTest(TestWimTemplate template)
            : base(template)
        {
        }

        protected override IntPtr Pointer => _items.ToPtr();

        public override void Dispose()
        {
            foreach (DismApi.DismImageInfo_ imageInfo in _items)
            {
                Marshal.FreeHGlobal(imageInfo.CustomizedInfo);

                Marshal.FreeHGlobal(imageInfo.Language);
            }

            base.Dispose();
        }

        protected override DismImageInfoCollection GetActual(IntPtr pointer)
        {
            return new DismImageInfoCollection(pointer, (uint)_items.Count);
        }

        protected override ReadOnlyCollection<DismImageInfo> GetExpected()
        {
            return new ReadOnlyCollection<DismImageInfo>(_items.Select(i => new DismImageInfo(i)).ToList());
        }
    }
}