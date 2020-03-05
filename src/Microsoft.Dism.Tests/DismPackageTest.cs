// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Dism.Tests
{
    public class DismPackageCollectionTest : DismCollectionTest<DismPackageCollection, DismPackage>
    {
        private static readonly List<DismApi.DismPackage_> Items = new List<DismApi.DismPackage_>
        {
            new DismApi.DismPackage_
            {
                InstallTime = DateTime.Today,
                PackageName = "PackageName1",
                PackageState = DismPackageFeatureState.Installed,
                ReleaseType = DismReleaseType.LanguagePack,
            },
            new DismApi.DismPackage_
            {
                InstallTime = DateTime.Today.AddDays(-17),
                PackageName = "PackageName2",
                PackageState = DismPackageFeatureState.Removed,
                ReleaseType = DismReleaseType.LanguagePack,
            },
        };

        public DismPackageCollectionTest(TestWimTemplate template)
            : base(template)
        {
        }

        protected override IntPtr Pointer => Items.ToPtr();

        protected override DismPackageCollection GetActual(IntPtr pointer)
        {
            return new DismPackageCollection(pointer, (uint)Items.Count);
        }

        protected override ReadOnlyCollection<DismPackage> GetExpected()
        {
            return new ReadOnlyCollection<DismPackage>(Items.Select(i => new DismPackage(i)).ToList());
        }
    }
}