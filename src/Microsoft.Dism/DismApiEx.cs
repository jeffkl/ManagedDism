using System;

namespace Microsoft.Dism
{
    public static partial class DismApi
    {
        /// <summary>
        /// Represents the DISM Generational Library initialized for use with the DismApi Wrapper (via InitializeEx()). Returns the specific DismGeneration in use; otherwise, returns DismGeneration.NotFound.
        /// </summary>
        private static DismGeneration CurrentDismGeneration { get; set; } = DismGeneration.NotFound;

        /// <summary>
        /// Initializes DISM API, using the latest installed DISM Generation. Initialize must be called once per process before calling any other DISM API functions.
        /// </summary>
        /// <param name="logLevel">Indicates the level of logging.</param>
        /// <exception cref="Exception">If an error occurs loading the latest DISM Generational Library.</exception> 
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void InitializeEx(DismLogLevel logLevel)
        {
            InitializeEx(logLevel, String.Empty, String.Empty, DismUtilities.GetLatestDismGeneration());
        }

        /// <summary>
        /// Initializes DISM API, using the latest installed DISM Generation. Initialize must be called once per process before calling any other DISM API functions.
        /// </summary>
        /// <param name="logLevel">Indicates the level of logging.</param>
        /// <param name="logFilePath">A relative or absolute path to a log file. All messages generated will be logged to this path. If NULL, the default log path, %windir%\Logs\DISM\dism.log, will be used.</param>
        /// <exception cref="Exception">If an error occurs loading the latest DISM Generational Library.</exception>  
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void InitializeEx(DismLogLevel logLevel, string logFilePath)
        {
            InitializeEx(logLevel, logFilePath, String.Empty, DismUtilities.GetLatestDismGeneration());
        }

        /// <summary>
        /// Initializes DISM API, using the latest installed DISM Generation. Initialize must be called once per process before calling any other DISM API functions.
        /// </summary>
        /// <param name="logLevel">Indicates the level of logging.</param>
        /// <param name="logFilePath">A relative or absolute path to a log file. All messages generated will be logged to this path. If NULL, the default log path, %windir%\Logs\DISM\dism.log, will be used.</param>
        /// <param name="scratchDirectory">A relative or absolute path to a scratch directory. DISM API will use this directory for internal operations. If null, the default temp directory, \Windows\%Temp%, will be used.</param>
        /// /// <exception cref="Exception">If an error occurs loading the latest DISM Generational Library.</exception> 
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void InitializeEx(DismLogLevel logLevel, string logFilePath, string scratchDirectory)
        {
            InitializeEx(logLevel, logFilePath, scratchDirectory, DismUtilities.GetLatestDismGeneration());
        }

        /// <summary>
        /// Initializes DISM API, using the specified DISM Generation. Initialize must be called once per process before calling any other DISM API functions.
        /// </summary>
        /// <param name="logLevel">Indicates the level of logging.</param>
        /// <param name="logFilePath">A relative or absolute path to a log file. All messages generated will be logged to this path. If NULL, the default log path, %windir%\Logs\DISM\dism.log, will be used.</param>
        /// <param name="scratchDirectory">A relative or absolute path to a scratch directory. DISM API will use this directory for internal operations. If null, the default temp directory, \Windows\%Temp%, will be used.</param>
        /// <param name="dismGeneration">The DISM Generational Library to be used.</param>
        /// /// <exception cref="Exception">If an error occurs loading the latest DISM Generational Library.</exception> 
        /// <exception cref="DismException">When a failure occurs.</exception>
        public static void InitializeEx(DismLogLevel logLevel, string logFilePath, string scratchDirectory, DismGeneration dismGeneration)
        {
            if (CurrentDismGeneration != DismGeneration.NotFound)
                throw new Exception(String.Format("A DISM Generation library is already loaded ({0}). Please call Shutdown() first to release the existing library.", dismGeneration.ToString()));

            if (dismGeneration != DismGeneration.NotFound && !DismUtilities.LoadDismGenerationLibrary(dismGeneration))
                throw new Exception(String.Format("Loading the latest DISM Generation library ({0}) failed.", dismGeneration.ToString()));

            CurrentDismGeneration = dismGeneration;

            DismApi.Initialize(logLevel, logFilePath, scratchDirectory);
        }
    }
}
