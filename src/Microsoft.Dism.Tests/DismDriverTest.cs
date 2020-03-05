// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Dism.Tests
{
    public class DismDriverCollectionTest : DismCollectionTest<DismDriverCollection, DismDriver>
    {
        private static readonly List<DismApi.DismDriver_> Items = new List<DismApi.DismDriver_>
        {
            new DismApi.DismDriver_
            {
                Architecture = (ushort)DismProcessorArchitecture.AMD64,
                CompatibleIds = "CompatibleIds",
                ExcludeIds = "ExcludeIds",
                HardwareDescription = "HardwareDescription",
                HardwareId = "HardwareId",
                ManufacturerName = "ManufacturerName",
                ServerName = "ServerName",
            },
            new DismApi.DismDriver_
            {
                Architecture = 9,
                CompatibleIds = "CompatibleIds",
                ExcludeIds = "ExcludeIds",
                HardwareDescription = "HardwareDescription",
                HardwareId = "HardwareId",
                ManufacturerName = "ManufacturerName",
                ServerName = "ServerName",
            },
        };

        public DismDriverCollectionTest(TestWimTemplate template)
            : base(template)
        {
        }

        protected override IntPtr Pointer => Items.ToPtr();

        protected override DismDriverCollection GetActual(IntPtr pointer)
        {
            return new DismDriverCollection(pointer, (uint)Items.Count);
        }

        protected override ReadOnlyCollection<DismDriver> GetExpected()
        {
            return new ReadOnlyCollection<DismDriver>(Items.Select(i => new DismDriver(i)).ToList());
        }
    }
}