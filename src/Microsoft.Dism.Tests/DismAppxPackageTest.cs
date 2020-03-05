// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Dism.Tests
{
    public class DismAppxPackageCollectionTest : DismCollectionTest<DismAppxPackageCollection, DismAppxPackage>
    {
        private static readonly List<DismApi.DismAppxPackage_> Items = new List<DismApi.DismAppxPackage_>
        {
            new DismApi.DismAppxPackage_
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
            },
            new DismApi.DismAppxPackage_
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
            },
        };

        public DismAppxPackageCollectionTest(TestWimTemplate template)
            : base(template)
        {
        }

        protected override IntPtr Pointer => Items.ToPtr();

        protected override DismAppxPackageCollection GetActual(IntPtr pointer)
        {
            return new DismAppxPackageCollection(pointer, (uint)Items.Count);
        }

        protected override ReadOnlyCollection<DismAppxPackage> GetExpected()
        {
            return new ReadOnlyCollection<DismAppxPackage>(Items.Select(i => new DismAppxPackage(i)).ToList());
        }
    }
}