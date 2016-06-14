using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Dism.Tests
{
    public class LambdaComparer<T> : IEqualityComparer<T>, IComparer<T>, IComparer
    {
        private readonly Func<T, T, bool> _lambdaComparer;
        private readonly Func<T, int> _lambdaHash;

        public LambdaComparer(Func<T, T, bool> lambdaComparer) :
            this(lambdaComparer, EqualityComparer<T>.Default.GetHashCode)
        {
        }

        public LambdaComparer(Func<T, T, bool> lambdaComparer, Func<T, int> lambdaHash)
        {
            if (lambdaComparer == null)
            {
                throw new ArgumentNullException(nameof(lambdaComparer));
            }
            if (lambdaHash == null)
            {
                throw new ArgumentNullException(nameof(lambdaHash));
            }

            _lambdaComparer = lambdaComparer;
            _lambdaHash = lambdaHash;
        }

        public bool Equals(T x, T y)
        {
            return _lambdaComparer(x, y);
        }

        public int GetHashCode(T obj)
        {
            return _lambdaHash(obj);
        }

        public int Compare(T x, T y)
        {
            return Equals(x, y) ? 0 : 1;
        }

        public int Compare(object x, object y)
        {
            return Compare((T)x, (T)y);
        }
    }
}