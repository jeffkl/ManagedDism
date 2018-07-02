// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Dism.Tests
{
    public class ApplyUnattendTest : DismInstallWimTestBase
    {
        public ApplyUnattendTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public void ApplyUnattendSimple()
        {
            const string unattendXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<unattend xmlns=""urn:schemas-microsoft-com:unattend"">
<settings pass=""windowsPE"">
        <component name=""Microsoft-Windows-Setup"" processorArchitecture=""x86"" publicKeyToken=""31bf3856ad364e35"" language=""neutral"" versionScope=""nonSxS"" xmlns:wcm=""http://schemas.microsoft.com/WMIConfig/2002/State"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
            <UserData>
                <ProductKey>
                    <WillShowUI>Never</WillShowUI>
                    <Key>XXXX-XXXX-XXXX-XXXX-XXXX</Key>
                </ProductKey>
                <FullName>Full Name</FullName>
                <Organization>Organization Name</Organization>
                <AcceptEula>true</AcceptEula>
            </UserData>
        </component>
    </settings>
                     </unattend>";

            string unattendXmlFile = Path.Combine(TestDirectory, "unattend.xml");

            File.WriteAllText(unattendXmlFile, unattendXml);

            DismApi.ApplyUnattend(Session, unattendXmlFile, singleSession: true);
        }
    }
}