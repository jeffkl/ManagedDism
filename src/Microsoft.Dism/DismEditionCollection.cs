// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Collections.ObjectModel;

namespace Microsoft.Dism
{
    /// <summary>
    /// Represents a collection of edition ID <see langword="string" />s.
    /// </summary>
    public class DismEditionCollection : ReadOnlyCollection<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DismEditionCollection" /> class.
        /// </summary>
        /// <param name="pointer">A pointer to the array of <see cref="DismApi.DismString" /> objects.</param>
        /// <param name="count">The number of objects in the array.</param>
        internal DismEditionCollection(IntPtr pointer, uint count)
            : base(pointer.ToList<string, DismApi.DismString>(count, dismString => dismString!))
        {
        }
    }
}
