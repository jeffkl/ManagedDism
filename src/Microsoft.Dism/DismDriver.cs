// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Dism
{
    public static partial class DismApi
    {
        /// <summary>
        /// Describes the architecture and hardware that the driver supports. The DismGetDriverInfo Function returns an object that includes an array of DismDriver structures. If you specify a DriverPath using the published name of the driver installed in the image, for example OEM1.inf, the array includes only the applicable hardware and architectures that are installed in the image. You can also specify a DriverPath using the source location of an .inf file on the technician computer. If you use the source location, the array includes all of the supported architectures and hardware that exist in the .inf file before it is installed to an architecture-specific image.
        /// </summary>
        /// <remarks>
        /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/hh824796.aspx"/>
        /// typedef struct _DismDriver
        /// {
        ///     PCWSTR ManufacturerName;
        ///     PCWSTR HardwareDescription;
        ///     PCWSTR HardwareId;
        ///     WORD Architecture;
        ///     PCWSTR ServiceName;
        ///     PCWSTR CompatibleIds;
        ///     PCWSTR ExcludeIds;
        /// } DismDriverInfo;
        /// </remarks>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        internal struct DismDriver_
        {
            /// <summary>
            /// The manufacturer name of the driver.
            /// </summary>
            public string ManufacturerName;

            /// <summary>
            /// A hardware description of the driver.
            /// </summary>
            public string HardwareDescription;

            /// <summary>
            /// The hardware ID of the driver.
            /// </summary>
            public string HardwareId;

            /// <summary>
            /// The architecture of the driver.
            /// </summary>
            public UInt16 Architecture;

            /// <summary>
            /// The service name of the driver.
            /// </summary>
            public string ServerName;

            /// <summary>
            /// The service name of the driver.
            /// </summary>
            public string CompatibleIds;

            /// <summary>
            /// The exclude IDs of the driver.
            /// </summary>
            public string ExcludeIds;
        }
    }

    /// <summary>
    /// Describes the architecture and hardware that the driver supports.
    /// </summary>
    public sealed class DismDriver : IEquatable<DismDriver>
    {
        private readonly DismApi.DismDriver_ _driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="DismDriver"/> class.
        /// </summary>
        /// <param name="driverPtr">A native pointer to a DismDriver_ struct.</param>
        internal DismDriver(IntPtr driverPtr)
            : this(driverPtr.ToStructure<DismApi.DismDriver_>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DismDriver"/> class.
        /// </summary>
        /// <param name="driver">A native DismDriver_ struct to copy data from.</param>
        internal DismDriver(DismApi.DismDriver_ driver)
        {
            // Copy data from the struct
            _driver = driver;
        }

        /// <summary>
        /// Gets the architecture of the driver.
        /// </summary>
        public DismProcessorArchitecture Architecture => (DismProcessorArchitecture)_driver.Architecture;

        /// <summary>
        /// Gets the service name of the driver.
        /// </summary>
        public string CompatibleIds => _driver.CompatibleIds;

        /// <summary>
        /// Gets the exclude IDs of the driver.
        /// </summary>
        public string ExcludeIds => _driver.ExcludeIds;

        /// <summary>
        /// Gets the hardware description of the driver.
        /// </summary>
        public string HardwareDescription => _driver.HardwareDescription;

        /// <summary>
        /// Gets the hardware ID of the driver.
        /// </summary>
        public string HardwareId => _driver.HardwareId;

        /// <summary>
        /// Gets the manufacturer name of the driver.
        /// </summary>
        public string ManufacturerName => _driver.ManufacturerName;

        /// <summary>
        /// Gets the service name of the driver.
        /// </summary>
        public string ServerName => _driver.ServerName;

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as DismDriver);
        }

        /// <summary>
        /// Determines whether the specified <see cref="DismDriver"/> is equal to the current <see cref="DismDriver"/>.
        /// </summary>
        /// <param name="driver">The <see cref="DismDriver"/> object to compare with the current object.</param>
        /// <returns>true if the specified <see cref="DismDriver"/> is equal to the current <see cref="DismDriver"/>; otherwise, false.</returns>
        public bool Equals(DismDriver driver)
        {
            return driver != null
                   && Architecture == driver.Architecture
                   && CompatibleIds == driver.CompatibleIds
                   && ExcludeIds == driver.ExcludeIds
                   && HardwareDescription == driver.HardwareDescription
                   && HardwareId == driver.HardwareId
                   && ManufacturerName == driver.ManufacturerName
                   && ServerName == driver.ServerName;
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="T:System.Object"/>.</returns>
        public override int GetHashCode()
        {
            return Architecture.GetHashCode()
                ^ (String.IsNullOrEmpty(CompatibleIds) ? 0 : CompatibleIds.GetHashCode())
                ^ (String.IsNullOrEmpty(ExcludeIds) ? 0 : ExcludeIds.GetHashCode())
                ^ (String.IsNullOrEmpty(HardwareDescription) ? 0 : HardwareDescription.GetHashCode())
                ^ (String.IsNullOrEmpty(HardwareId) ? 0 : HardwareId.GetHashCode())
                ^ (String.IsNullOrEmpty(ManufacturerName) ? 0 : ManufacturerName.GetHashCode())
                ^ (String.IsNullOrEmpty(ServerName) ? 0 : ServerName.GetHashCode());
        }
    }

    /// <summary>
    /// Represents a collection of <see cref="DismDriver"/> objects.
    /// </summary>
    public sealed class DismDriverCollection : DismCollection<DismDriver>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DismDriverCollection"/> class.
        /// </summary>
        internal DismDriverCollection()
            : base(new List<DismDriver>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DismDriverCollection"/> class.
        /// </summary>
        /// <param name="list">An <see cref="IList{DismDriver}"/> to wrap.</param>
        internal DismDriverCollection(IList<DismDriver> list)
            : base(list)
        {
        }
    }
}