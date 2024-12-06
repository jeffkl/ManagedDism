// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;

namespace Microsoft.Dism
{
    /// <summary>
    /// Represents information related to a Windows product key, such as the Windows edition ID and channel.
    /// </summary>
    public class DismProductKeyInfo : IEquatable<DismProductKeyInfo>
    {
        private readonly string _editionId;
        private readonly string _channel;

        /// <summary>
        /// Initializes a new instance of the <see cref="DismProductKeyInfo" /> class.
        /// </summary>
        /// <param name="editionId">The edition ID.</param>
        /// <param name="channel">The channel name.</param>
        internal DismProductKeyInfo(string editionId, string channel)
        {
            _editionId = editionId!;
            _channel = channel!;
        }

        /// <summary>
        /// Gets the edition ID.
        /// </summary>
        public string EditionId => _editionId;

        /// <summary>
        /// Gets the channel name.
        /// </summary>
        public string Channel => _channel;

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />, otherwise <see langword="false" />.</returns>
        public override bool Equals(object? obj)
        {
            return obj != null && Equals(obj as DismProductKeyInfo);
        }

        /// <summary>
        /// Determines whether the specified <see cref="DismProductKeyInfo" /> is equal to the current <see cref="DismProductKeyInfo" />.
        /// </summary>
        /// <param name="other">The <see cref="DismProductKeyInfo" /> object to compare with the current object.</param>
        /// <returns><see langword="true" /> if the specified <see cref="DismProductKeyInfo" /> is equal to the current <see cref="DismProductKeyInfo" />, otherwise <see langword="false" />.</returns>
        public bool Equals(DismProductKeyInfo? other)
        {
            return other != null
                   && EditionId == other.EditionId
                   && Channel == other.Channel;
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
        public override int GetHashCode()
        {
            return EditionId.GetHashCode() ^ Channel.GetHashCode();
        }
    }
}
