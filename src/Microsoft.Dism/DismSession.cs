using System.Runtime.ConstrainedExecution;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Dism
{
    /// <summary>
    /// Represents a DismSession handle.
    /// </summary>
    public sealed class DismSession : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        /// Initializes a new instance of the DismSessionHandle class.
        /// </summary>
        public DismSession()
            : base(true)
        {
        }

        /// <summary>
        /// Releases the DismSession handle.
        /// </summary>
        /// <returns>true if the handle is released successfully; otherwise, in the event of a catastrophic failure, false.</returns>
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        protected override bool ReleaseHandle()
        {
            // See if the handle is valid and hasn't already been closed
            if (!IsInvalid)
            {
                // Close the session
                return DismApi.NativeMethods.DismCloseSession(DangerousGetHandle()) == 0;
            }

            // Return true
            return true;
        }
    }
}