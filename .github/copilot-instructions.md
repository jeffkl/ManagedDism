# Copilot Instructions for ManagedDism

## Repository Overview

ManagedDism is a managed .NET wrapper around the native Windows Deployment Image Servicing and Management (DISM) API. The primary package is `Microsoft.Dism`, with integration tests in `Microsoft.Dism.Tests`.

The wrapper intentionally follows the native DISM API closely. Preserve native function names, parameter meanings, enum values, structure layouts, error behavior, and ownership semantics unless a change explicitly requires otherwise.

## Project Structure

- `src/Microsoft.Dism/` contains the shipping library.
- `src/Microsoft.Dism.Tests/` contains xUnit integration tests.
- `src/Microsoft.Dism/PublicAPI.Shipped.txt` and `PublicAPI.Unshipped.txt` track the public API surface.
- Each DISM operation is organized into its own `DismApi.<Operation>.cs` file. That file contains both the public managed wrapper and the corresponding declaration in the nested `DismApi.NativeMethods` partial class, keeping both sides of an operation easy to find together.
- The repository-root `dismapi.h` header is the source used to derive native DISM function signatures, parameter types, flags, structures, and ownership requirements.
- `Directory.Build.props`, `Directory.Build.targets`, and `Directory.Packages.props` contain repository-wide build and package settings.
- `.github/workflows/CI.yml` is the authoritative CI workflow.

## Build and Test

Build the solution from the repository root on Windows:

```powershell
dotnet build Microsoft.Dism.sln
```

The repository uses SDK-style projects, and CI builds them with `dotnet build`. The fact that the library calls Windows-only APIs does not by itself require Visual Studio `msbuild.exe`. Do not switch build engines merely because the library is Windows-only; diagnose the actual restore, SDK, targeting-pack, or MSBuild error first.

Before running tests, verify that the current process is elevated:

```powershell
$principal = [Security.Principal.WindowsPrincipal]::new([Security.Principal.WindowsIdentity]::GetCurrent())
$principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
```

If the result is `False`, do not run the DISM integration tests. Tell the user that the tests require elevation and ask them to restart their terminal, PowerShell, Visual Studio, or Copilot CLI by using **Run as administrator**.

After a successful build, run tests without rebuilding:

```powershell
dotnet test Microsoft.Dism.sln --no-restore --no-build --framework net472
dotnet test Microsoft.Dism.sln --no-restore --no-build --framework net8.0
dotnet test Microsoft.Dism.sln --no-restore --no-build --framework net10.0
```

The DISM integration tests are Windows-specific and require an elevated Administrator process. Do not describe a skipped or unrun test as passing. If elevation, Windows APIs, or test image prerequisites are unavailable, report that limitation explicitly and still run the smallest applicable build or static validation.

Before running tests derived from `DismInstallWimTestBase`, check for existing mounted images with `DismApi.GetMountedImages()` or `dism /Get-MountedWimInfo`. The fixture currently unmounts every registered image without committing changes and calls global mount-point cleanup. Do not run these tests while any pre-existing or unrelated image is mounted.

## Target Frameworks and Compatibility

- The library targets `net472`, `netstandard2.0`, `net8.0`, and `net10.0`.
- Tests target `net472`, `net8.0`, and `net10.0`.
- Preserve compatibility with every target framework. Do not use an API unavailable on older targets without an appropriate conditional implementation.
- Existing native declarations use `LibraryImport` for `NET7_0_OR_GREATER` and `DllImport` for older targets. Follow that pattern when adding P/Invoke declarations.
- The library declares `IsAotCompatible` for modern target frameworks. Preserve AOT compatibility when adding interop, marshalling, callbacks, reflection, or dynamically accessed code.
- Keep unsafe code limited to target frameworks where it is enabled and necessary.

## C# and Interop Conventions

- Follow `.editorconfig`: four-space indentation, CRLF line endings, block-scoped namespaces, explicit types instead of `var`, and `using` directives outside namespaces.
- Include the repository copyright and MIT license header in new C# files.
- Nullable reference types and warnings-as-errors are enabled. Resolve warnings rather than suppressing them unless a suppression is justified and narrowly scoped.
- Add XML documentation for public types and members; the package generates an XML documentation file.
- Keep each native DISM operation in the existing `DismApi.<Operation>.cs` partial-class pattern. Place the managed wrapper and its `NativeMethods` declaration in the same operation file rather than centralizing all P/Invoke declarations.
- Reuse shared interop infrastructure from `DismApi.NativeMethods.cs`, including the DISM library name, character set, and string marshalling constants. Operation-specific declarations belong in their operation files; shared constants and plumbing do not.
- Derive new or corrected native declarations from the repository-root `dismapi.h`. Treat it as the primary reference for signature details; use external documentation for behavioral context, not as a substitute for checking the bundled header.
- Match native integer widths, character encoding, calling behavior, and structure layout precisely. Prefer existing interop types and constants over introducing equivalents.
- Check native HRESULT values through the existing `DismUtilities.ThrowIfFail` path.
- Release native memory and handles in `finally` blocks or established disposable wrappers. Use the repository's `Delete` and session-lifetime patterns rather than inventing new ownership rules.
- Preserve source and binary compatibility unless the requested change explicitly allows a breaking change.

## Public API Changes

`Microsoft.CodeAnalysis.PublicApiAnalyzers` validates the package API.

- Update `src/Microsoft.Dism/PublicAPI.Unshipped.txt` when adding or intentionally changing public API.
- Do not edit `PublicAPI.Shipped.txt` to hide an accidental breaking change.
- Prefer additive API changes. Call out any compatibility impact before making a breaking change.

## Dependencies

Central package management is enabled. Add or update package versions only in `Directory.Packages.props`; do not put versions in project-level `PackageReference` items. Keep package entries alphabetically ordered within their item group.

Avoid adding dependencies when the .NET base class library or an existing repository dependency is sufficient.

## Tests

- Use xUnit v3 and Shouldly, matching the existing tests.
- Add tests in `src/Microsoft.Dism.Tests/` for new behavior and regressions.
- Prefer the existing DISM fixtures, WIM templates, session helpers, and validation helpers over duplicating setup.
- Ensure DISM initialization, sessions, mounted images, temporary files, and native resources are cleaned up even when a test fails.
- Integration tests can modify or inspect the running Windows installation, mount images, and create temporary files. Do not run independent DISM test processes concurrently.
- If a test is interrupted, remove a mount or temporary directory only when its exact path was captured from that run or positively identified through mounted-image metadata such as both its mount path and image file path. Do not infer ownership from a GUID-shaped directory name. Do not run global cleanup operations such as `DismApi.CleanupMountpoints` or `dism /Cleanup-Wim` without explicit user approval; report suspected leftovers instead when ownership is uncertain.
- Run the narrowest relevant test first, then the affected target frameworks when practical.

## Change Guidelines

- Make focused changes and avoid unrelated modernization of this mature interop layer.
- Consult the corresponding Microsoft DISM API documentation when behavior, flags, ownership, or marshalling is unclear.
- Keep managed behavior close to the native contract while still providing clear .NET exceptions and disposal semantics.
- Update README or XML documentation when user-visible behavior or usage changes.
- Before finishing, inspect the diff for accidental public API changes, generated files, build artifacts, or secrets.
