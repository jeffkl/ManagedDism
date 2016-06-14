using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;

namespace Microsoft.Dism.Tests
{
    [TestFixture]
    public abstract class DismCollectionTest<TCollection, TItem> : TestBase
        where TCollection : DismCollection<TItem>
        where TItem : class
    {
        [Test]
        public void CollectionTest()
        {
            var expectedCollection = GetCollection();

            VerifyCollection(expectedCollection, CreateCollection(expectedCollection));
        }

        [Test]
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