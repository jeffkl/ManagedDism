# Managed DismApi Wrapper
===

[![NuGet](https://img.shields.io/nuget/v/Microsoft.Dism.svg?maxAge=2592000)]()

This is a managed wrapper for the native Deployment Image Servicing and Management (DISM) API. 

This assembly allows .NET developers to call directly into the DismApi without having to shell out to Dism.exe. This allows for better integration and richer errors. 

Reference to the native DismApi on MSDN: Deployment Image Servicing and Management (DISM) API

This managed wrapper works as similarly to the native API as possible with a few managed wrappers to make it more like a .NET API.



``` C#
DismApi.Initialize(DismLogLevel.LogErrors);

DismApi.Shutdown();
```