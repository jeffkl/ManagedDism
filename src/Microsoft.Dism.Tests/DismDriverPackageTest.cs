// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Dism.Tests
{
    public class DismDriverPackageCollectionTest : DismCollectionTest<DismDriverPackageCollection, DismDriverPackage>
    {
        private static readonly List<DismApi.DismDriverPackage_> Items = new List<DismApi.DismDriverPackage_>
        {
            new DismApi.DismDriverPackage_
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
            },
            new DismApi.DismDriverPackage_
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
            },
        };

        public DismDriverPackageCollectionTest(TestWimTemplate template)
            : base(template)
        {
        }

        protected override IntPtr Pointer => Items.ToPtr();

        protected override DismDriverPackageCollection GetActual(IntPtr pointer)
        {
            return new DismDriverPackageCollection(pointer, (uint)Items.Count);
        }

        protected override ReadOnlyCollection<DismDriverPackage> GetExpected()
        {
            return new ReadOnlyCollection<DismDriverPackage>(Items.Select(i => new DismDriverPackage(i)).ToList());
        }
    }
}