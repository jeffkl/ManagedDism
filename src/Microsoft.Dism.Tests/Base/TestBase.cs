using NUnit.Framework;

namespace Microsoft.Dism.Tests
{
    /// <summary>
    /// A base class for tests.
    /// </summary>
    public abstract class TestBase
    {
        public TestContext TestContext
        {
            get;
            set;
        }
    }
}