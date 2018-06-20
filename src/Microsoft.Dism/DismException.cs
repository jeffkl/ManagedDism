// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Microsoft.Dism.Properties;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Microsoft.Dism
{
    /// <summary>
    /// Represents an exception in the DismApi.
    /// </summary>
    [Serializable]
    public class DismException : Win32Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DismException"/> class with a specified error that is the cause of this exception.
        /// </summary>
        /// <param name="errorCode">The HRESULT, a coded numerical value that is assigned to a specific exception.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        internal DismException(int errorCode, string message)
            : base(errorCode, message)
        {
            HResult = errorCode;
        }

        /// <summary>
        /// Gets a <see cref="DismException"/> or <see cref="Exception"/> for the specified error code.
        /// </summary>
        /// <param name="errorCode">The error code to get an exception for.</param>
        /// <returns>A <see cref="DismException"/> or <see cref="Exception"/> that represents the error code.</returns>
        internal static Exception GetDismExceptionForHResult(int errorCode)
        {
            // Look for known error codes
            switch ((uint)errorCode)
            {
                case DismApi.ERROR_REQUEST_ABORTED:
                case 0x80070000 ^ DismApi.ERROR_REQUEST_ABORTED:
                case DismApi.ERROR_CANCELLED:
                case 0x80070000 ^ DismApi.ERROR_CANCELLED:
                    return new OperationCanceledException();

                case DismApi.ERROR_SUCCESS_REBOOT_REQUIRED:
                    return new DismRebootRequiredException(errorCode);

                case DismApi.DISMAPI_S_RELOAD_IMAGE_SESSION_REQUIRED:
                    return new DismReloadImageSessionRequiredException(errorCode);

                case DismApi.DISMAPI_E_DISMAPI_NOT_INITIALIZED:
                    // User has not called DismApi.Initialize()
                    return new DismNotInitializedException(errorCode);

                case DismApi.DISMAPI_E_OPEN_SESSION_HANDLES:
                    // User has not called CloseSession() on open sessions
                    return new DismOpenSessionsException(errorCode);
            }

            // Attempt to get an error message from the DismApi
            string lastError = DismApi.GetLastErrorMessage();

            // See if the result is not null
            if (!String.IsNullOrEmpty(lastError))
            {
                // Return a DismException object
                return new DismException(errorCode, lastError.Trim());
            }

            // Return an Exception for the HResult
            return Marshal.GetExceptionForHR(errorCode);
        }
    }

    /// <summary>
    /// The exception that is thrown when an attempt to use the DismApi occurs without first calling <see cref="DismApi.Initialize(DismLogLevel)"/>.
    /// </summary>
    [Serializable]
    public sealed class DismNotInitializedException : DismException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DismNotInitializedException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code to associate with the exception.</param>
        internal DismNotInitializedException(int errorCode)
            : base(errorCode, Resources.DismExceptionMessageNotInitialized)
        {
        }
    }

    /// <summary>
    /// The exception that is thrown when an attempt to shutdown the Dism API occurs while there are open sessions.
    /// </summary>
    [Serializable]
    public sealed class DismOpenSessionsException : DismException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DismOpenSessionsException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code to associate with the exception.</param>
        internal DismOpenSessionsException(int errorCode)
            : base(errorCode, Resources.DismExceptionMessageOpenSessions)
        {
        }
    }

    /// <summary>
    /// The exception that is thrown when the previous operations requires a reboot.
    /// </summary>
    [Serializable]
    public class DismRebootRequiredException : DismException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DismRebootRequiredException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code to associate with the exception.</param>
        internal DismRebootRequiredException(int errorCode)
            : base(errorCode, Resources.DismExceptionMessageRebootRequired)
        {
        }
    }

    /// <summary>
    /// The exception that is thrown when the previous operations requires a reload of image session.
    /// </summary>
    [Serializable]
    public class DismReloadImageSessionRequiredException : DismException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DismReloadImageSessionRequiredException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code to associate with the exception.</param>
        internal DismReloadImageSessionRequiredException(int errorCode)
            : base(errorCode, Resources.DismExceptionMessageReloadImageSessionRequired)
        {
        }
    }
}