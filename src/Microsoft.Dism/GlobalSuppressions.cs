// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "This library only works on Windows", Scope = "member", Target = "~P:Microsoft.Dism.DismUtilities.WAIKDISMAPIPath")]
[assembly: SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "This library only works on Windows", Scope = "member", Target = "~P:Microsoft.Dism.DismUtilities.WADK10DismApiPath")]
[assembly: SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "This library only works on Windows", Scope = "member", Target = "~P:Microsoft.Dism.DismUtilities.WADK80DISMAPIPath")]
[assembly: SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "This library only works on Windows", Scope = "member", Target = "~P:Microsoft.Dism.DismUtilities.WADK81DISMAPIPath")]
[assembly: SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "This library only works on Windows", Scope = "member", Target = "~M:Microsoft.Dism.DismUtilities.GetKitsRoot(System.String)")]
