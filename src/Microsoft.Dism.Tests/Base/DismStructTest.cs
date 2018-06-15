// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Xunit;

namespace Microsoft.Dism.Tests
{
    public abstract class DismStructTest<T> : TestBase
        where T : class
    {
        protected abstract T Item
        {
            get;
        }

        protected IntPtr ItemPtr
        {
            get;
            private set;
        }

        protected abstract object Struct
        {
            get;
        }

        [Fact]
        public void PropertyTest()
        {
            VerifyProperties(Item);
        }

        [Fact]
        public void PropertyTest_IntPtr()
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

        protected IntPtr ListToPtrArray<TItem>(IList<TItem> items)
        {
            var sizeStruct = Marshal.SizeOf(typeof(TItem));

            var ptr = Marshal.AllocHGlobal(sizeStruct * items.Count);

            for (var i = 0; i < items.Count; i++)
            {
                Marshal.StructureToPtr(items[i], ptr + (i * sizeStruct), false);
            }

            return ptr;
        }

        protected abstract void VerifyProperties(T item);
    }
}