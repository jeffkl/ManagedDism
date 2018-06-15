// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Microsoft.Dism.Tests
{
    public abstract class DismCollectionTest<TCollection, TItem> : TestBase
        where TCollection : DismCollection<TItem>
        where TItem : class
    {
        [Fact]
        public void CollectionTest()
        {
            var expectedCollection = GetCollection();

            VerifyCollection(expectedCollection, CreateCollection(expectedCollection));
        }

        [Fact]
        public void CollectionTest_Empty()
        {
            VerifyCollection(new List<TItem>(), CreateCollection());
        }

        protected abstract TCollection CreateCollection(List<TItem> expectedCollection);

        protected abstract TCollection CreateCollection();

        protected abstract List<TItem> GetCollection();

        private void VerifyCollection(IList expectedCollection, TCollection actualCollection)
        {
            actualCollection.ShouldBe(expectedCollection);
        }
    }
}