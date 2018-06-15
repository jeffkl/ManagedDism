// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Dism
{
    /// <summary>
    /// Represents a generic collection used in the DismApi which is read-only.
    /// </summary>
    /// <typeparam name="T">The type contained in the collection.</typeparam>
    public abstract class DismCollection<T> : ReadOnlyCollection<T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DismCollection{T}"/> class.
        /// </summary>
        internal DismCollection()
            : base(new List<T>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DismCollection{T}"/> class.
        /// </summary>
        /// <param name="list">An existing list of Dism objects to expose as a read-only collection.</param>
        internal DismCollection(IList<T> list)
            : base(list)
        {
        }

        /// <summary>
        /// Adds an item to the internal collection.
        /// </summary>
        /// <param name="item">The item to add to the collection.</param>
        internal void Add(T item)
        {
            // Add the item to the internal collection.
            Items.Add(item);
        }

        /// <summary>
        /// Adds a list of items to the collection.
        /// </summary>
        /// <typeparam name="TStruct">The type of the items.</typeparam>
        /// <param name="ptr">An <see cref="IntPtr"/> pointing to a array of items.</param>
        /// <param name="count">The number of items in the array.</param>
        /// <param name="constructor">A <see cref="Func{TStruct, T}"/> that can create a new instance of the wrapped item.</param>
        internal void AddRange<TStruct>(IntPtr ptr, int count, Func<TStruct, T> constructor)
        {
            // See if an array of pointers was returned
            if (ptr != IntPtr.Zero)
            {
                // Loop through the array of pointers and add them to the collection as a DismDriver object
                foreach (var item in ptr.AsEnumerable<TStruct>(count).Select(constructor))
                {
                    Add(item);
                }
            }
        }
    }
}