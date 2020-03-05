// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using Xunit;

namespace Microsoft.Dism.Tests
{
    public abstract class DismCollectionTest<TCollection, TItem> : TestBase
        where TCollection : ReadOnlyCollection<TItem>
        where TItem : class
    {
        protected DismCollectionTest(TestWimTemplate template)
            : base(template)
        {
        }

        protected abstract IntPtr Pointer { get; }

        [Fact]
        public void CollectionTest()
        {
            IntPtr pointer = Pointer;

            try
            {
                TCollection actual = GetActual(pointer);
                ReadOnlyCollection<TItem> expected = GetExpected();
                actual.ShouldBe(expected);
            }
            finally
            {
                Marshal.FreeHGlobal(pointer);
            }
        }

        [Fact]
        public void CollectionTest_Empty()
        {
            TCollection actual = GetActual(IntPtr.Zero);
            ReadOnlyCollection<TItem> expected = new ReadOnlyCollection<TItem>(new List<TItem>(0));
            actual.ShouldBe(expected);
        }

        protected abstract TCollection GetActual(IntPtr pointer);

        protected abstract ReadOnlyCollection<TItem> GetExpected();
    }
}