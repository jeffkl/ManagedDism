using System;
using Microsoft.Dism.Properties;
using Microsoft.Win32;
using NUnit.Framework;
using Shouldly;

namespace Microsoft.Dism.Tests
{
    [TestFixture]
    public class DismExceptionTest
    {
        [Test]
        public void DismRebootRequiredExceptionTest()
        {
            VerifyDismException<DismRebootRequiredException>(DismApi.ERROR_SUCCESS_REBOOT_REQUIRED, Resources.DismExceptionMessageRebootRequired);
        }

        [Test]
        public void DismNotInitializedExceptionTest()
        {
            VerifyDismException<DismNotInitializedException>(DismApi.DISMAPI_E_DISMAPI_NOT_INITIALIZED, Resources.DismExceptionMessageNotInitialized);
        }

        [Test]
        public void DismOpenSessionsExceptionTest()
        {
            VerifyDismException<DismOpenSessionsException>(DismApi.DISMAPI_E_OPEN_SESSION_HANDLES, Resources.DismExceptionMessageOpenSessions);
        }


        [Test]
        public void GetLastErrorMessageTest()
        {
            const string message = "Hello World";

            DismApi.GetLastErrorMessageTestHook = () => message;

            VerifyDismException<DismException>(DismApi.ERROR_OUTOFMEMORY, message);
        }

        [Test]
        public void Win32ExceptionTest()
        {
            const int errorCode = unchecked((int)0x80020012);

            const string errorMessage = "Attempted to divide by zero.";

            DismApi.GetLastErrorMessageTestHook = () => null;

            Exception exception = DismException.GetDismExceptionForHResult(errorCode);

            exception.ShouldBeOfType<DivideByZeroException>();

            exception.Message.ShouldBe(errorMessage);
        }

        [Test]
        public void OperationCanceledExceptionTest()
        {
            const int errorCode = unchecked((int) 0x800704D3);

            const string errorMessage = "The operation was canceled.";

            DismApi.GetLastErrorMessageTestHook = () => null;

            Exception exception = DismException.GetDismExceptionForHResult(errorCode);

            exception.ShouldBeOfType<OperationCanceledException>();

            exception.Message.ShouldBe(errorMessage);
        }

        private void VerifyDismException<T>(uint errorCode, string message)
            where T : DismException
        {
            Exception exception = DismException.GetDismExceptionForHResult((int)errorCode);
            exception.ShouldBeOfType<T>();

            DismException dismException = (DismException)exception;

            exception.Message.ShouldBe(message);

            dismException.ErrorCode.ShouldBe((int)errorCode);
            dismException.NativeErrorCode.ShouldBe((int)errorCode);
        }
    }
}
