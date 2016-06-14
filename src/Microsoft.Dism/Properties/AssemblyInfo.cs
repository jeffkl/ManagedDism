using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Microsoft.Dism")]
[assembly: AssemblyDescription("Managed API for Deployment Image Servicing and Management (DISM)")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
[assembly: AssemblyCompany("Microsoft Corporation")]
[assembly: AssemblyProduct("DISM")]
[assembly: AssemblyCopyright("Copyright ©  2012")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: CLSCompliant(false)]
[assembly: ComVisible(false)]
[assembly: AssemblyVersion("1.5.*")]
[assembly: AssemblyInformationalVersion("1.5.1")]
[assembly: InternalsVisibleTo("Microsoft.Dism.Tests")]