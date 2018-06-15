// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Runtime.InteropServices;

using WORD = System.UInt16;

// ReSharper disable InconsistentNaming
namespace Microsoft.Dism
{
#pragma warning disable SA1307 // Accessible fields must begin with upper-case letter

    /// <summary>
    /// Specifies a date and time, using individual members for the month, day, year, weekday, hour, minute, second, and millisecond. The time is either in coordinated universal time (UTC) or local time, depending on the function that is being called.
    /// </summary>
    /// <remarks>It is not recommended that you add and subtract values from the SYSTEMTIME structure to obtain relative times. Instead, you should
    /// <list type="bullet">
    ///     <item><description>Convert the SYSTEMTIME structure to a FILETIME structure.</description></item>
    ///     <item><description>Copy the resulting FILETIME structure to a ULARGE_INTEGER structure.</description></item>
    ///     <item><description>Use normal 64-bit arithmetic on the ULARGE_INTEGER value.</description></item>
    /// </list>
    /// The system can periodically refresh the time by synchronizing with a time source. Because the system time can be adjusted either forward or backward, do not compare system time readings to determine elapsed time. Instead, use one of the methods described in Windows Time.</remarks>
    /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms724950(v=vs.85).aspx"/>
    /// <![CDATA[typedef struct _SYSTEMTIME {
    /// WORD wYear;
    /// WORD wMonth;
    /// WORD wDayOfWeek;
    /// WORD wDay;
    /// WORD wHour;
    /// WORD wMinute;
    /// WORD wSecond;
    /// WORD wMilliseconds;
    /// } SYSTEMTIME, *PSYSTEMTIME;]]>
    [StructLayout(LayoutKind.Sequential)]
#pragma warning disable SA1649 // File name must match first type name
    internal struct SYSTEMTIME
#pragma warning restore SA1649 // File name must match first type name
    {
        /// <summary>
        /// The year. The valid values for this member are 1601 through 30827.
        /// </summary>
        public WORD wYear;

        /// <summary>
        /// The month. January = 1 and December = 12
        /// </summary>
        public WORD wMonth;

        /// <summary>
        /// The day of the week. Sunday = 0 and Saturday = 6
        /// </summary>
        public WORD wDayOfWeek;

        /// <summary>
        /// The day of the month. The valid values for this member are 1 through 31.
        /// </summary>
        public WORD wDay;

        /// <summary>
        /// The hour. The valid values for this member are 0 through 23.
        /// </summary>
        public WORD wHour;

        /// <summary>
        /// The minute. The valid values for this member are 0 through 59.
        /// </summary>
        public WORD wMinute;

        /// <summary>
        /// The second. The valid values for this member are 0 through 59.
        /// </summary>
        public WORD wSecond;

        /// <summary>
        /// The millisecond. The valid values for this member are 0 through 999.
        /// </summary>
        public WORD wMilliseconds;

        /// <summary>
        /// Initializes a new instance of the <see cref="SYSTEMTIME"/> struct.
        /// Initializes a new instance of the SYSTEMTIME class.
        /// </summary>
        /// <param name="dateTime">An existing DateTime object to copy data from.</param>
        public SYSTEMTIME(DateTime dateTime)
        {
            DateTime utc = dateTime.ToUniversalTime();

            wYear = (ushort)utc.Year;
            wMonth = (ushort)utc.Month;
            wDay = (ushort)utc.Day;
            wDayOfWeek = (ushort)utc.DayOfWeek;
            wHour = (ushort)utc.Hour;
            wMinute = (ushort)utc.Minute;
            wSecond = (ushort)utc.Second;
            wMilliseconds = (ushort)utc.Millisecond;
        }

        /// <summary>
        /// Converts a <see cref="System.DateTime"/> to a <see cref="SYSTEMTIME"/>.
        /// </summary>
        /// <param name="dateTime">The time to convert.</param>
        public static implicit operator SYSTEMTIME(DateTime dateTime)
        {
            return new SYSTEMTIME(dateTime);
        }

        /// <summary>
        /// Converts a <see cref="SYSTEMTIME"/> to a <see cref="System.DateTime"/>
        /// </summary>
        /// <param name="systemTime">The time to convert.</param>
        public static implicit operator DateTime(SYSTEMTIME systemTime)
        {
            return systemTime.ToDateTime();
        }

        /// <summary>
        /// Returns the SYSTEMTIME as a <see cref="System.DateTime"/> value.
        /// </summary>
        /// <returns>A <see cref="System.DateTime"/> value.</returns>
        public DateTime ToDateTime()
        {
            return wYear == 0
                ? DateTime.MinValue
                : new DateTime(wYear, wMonth, wDay, wHour, wMinute, wSecond, DateTimeKind.Utc);
        }

#pragma warning restore SA1307 // Accessible fields must begin with upper-case letter
    }
}