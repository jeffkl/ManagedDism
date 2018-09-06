# Managed DismApi Wrapper

[![Microsoft.Dism](https://img.shields.io/nuget/v/Microsoft.Dism.svg?maxAge=2592000)](https://www.nuget.org/packages/Microsoft.Dism) [![Build Status](https://jeffkl.visualstudio.com/CBT/_apis/build/status/ManagedDism%20CI)](https://jeffkl.visualstudio.com/CBT/_build/latest?definitionId=15)

This is a managed wrapper for the native Deployment Image Servicing and Management (DISM) API. 

This assembly allows .NET developers to call directly into the DismApi without having to shell out to Dism.exe. This allows for better integration and richer errors. 

Reference to the native DismApi on MSDN: Deployment Image Servicing and Management (DISM) API

This managed wrapper works as similarly to the native API as possible with a few managed wrappers to make it more like a .NET API.

``` C#
DismApi.Initialize(DismLogLevel.LogErrors);

DismApi.Shutdown();
```

See the [wiki](https://github.com/josemesona/ManagedDism/wiki) for more examples on using the API.

# Contributing
Please read the [Contributing doc](CONTRIBUTING.md) for information on building the code and contributing to the project.
