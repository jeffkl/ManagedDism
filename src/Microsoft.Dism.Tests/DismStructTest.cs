// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Runtime.InteropServices;
using Xunit;

namespace Microsoft.Dism.Tests
{
    public abstract class DismStructTest<T> : TestBase
        where T : class
    {
        protected DismStructTest(TestWimTemplate template)
            : base(template)
        {
        }

        protected abstract T Item { get; }

        protected IntPtr ItemPtr { get; private set; }

        protected abstract object Struct { get; }

        [Fact]
        public void PropertyTest()
        {
            ItemPtr = Marshal.AllocHGlobal(Marshal.SizeOf(Struct));

            try
            {
                Marshal.StructureToPtr(Struct, ItemPtr, false);

                VerifyProperties(Item);
            }
            finally
            {
                Marshal.FreeHGlobal(ItemPtr);

                ItemPtr = IntPtr.Zero;
            }
        }

        protected abstract void VerifyProperties(T item);
    }
}