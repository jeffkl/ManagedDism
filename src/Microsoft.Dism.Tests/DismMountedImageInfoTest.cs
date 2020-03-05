// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Dism.Tests
{
    public class DismMountedImageInfoCollectionTest : DismCollectionTest<DismMountedImageInfoCollection, DismMountedImageInfo>
    {
        private static readonly List<DismApi.DismMountedImageInfo_> Items = new List<DismApi.DismMountedImageInfo_>
        {
            new DismApi.DismMountedImageInfo_
            {
                ImageFilePath = "ImageFilePath1",
                ImageIndex = 1,
                MountMode = DismMountMode.ReadOnly,
                MountPath = "MountPath1",
                MountStatus = DismMountStatus.Invalid,
            },
            new DismApi.DismMountedImageInfo_
            {
                ImageFilePath = "ImageFilePath2",
                ImageIndex = 2,
                MountMode = DismMountMode.ReadWrite,
                MountPath = "MountPath2",
                MountStatus = DismMountStatus.NeedsRemount,
            },
            new DismApi.DismMountedImageInfo_
            {
                ImageFilePath = "ImageFilePath3",
                ImageIndex = 3,
                MountMode = DismMountMode.ReadOnly,
                MountPath = "MountPath3",
                MountStatus = DismMountStatus.Ok,
            },
        };

        public DismMountedImageInfoCollectionTest(TestWimTemplate template)
            : base(template)
        {
        }

        protected override IntPtr Pointer => Items.ToPtr();

        protected override DismMountedImageInfoCollection GetActual(IntPtr pointer)
        {
            return new DismMountedImageInfoCollection(pointer, (uint)Items.Count);
        }

        protected override ReadOnlyCollection<DismMountedImageInfo> GetExpected()
        {
            return new ReadOnlyCollection<DismMountedImageInfo>(Items.Select(i => new DismMountedImageInfo(i)).ToList());
        }
    }
}