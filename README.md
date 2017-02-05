# Notes on C# 7

**At the time of writing C# 7 is not yet being released.**

This repository contains a lot of C# 7 code to showcase the new features of the language.

Follow some notes in preparation for a technical presentation at the Cambridge .NET User Group in UK.

## Features: 

[Language Feature Status](https://github.com/dotnet/roslyn/blob/3e580ad4bb069c3aad6c0b6eba854e13fa5dbfcc/docs/Language%20Feature%20Status.md)

**Working with Data**:
 * Tuples & Decostruction
 * Pattern Matching
 * Expression Bodied Everything (constructors/deconstructors/get/set)
 * ValueTask (arbitrary async returns)

**Improved Performance**:
 * Local Functions (clojure implemented as additional arguments)
 * Ref Returns and ref locals
 * ValueTask?

**Code Simplification**:
 * Binary Literals and Digit Separators
 * Out var
 * Throw Expr

Features for future version of the language:
 * Record types
 * More advanced pattern matching
 * Nullable & not-nullable reference types
 * Private proteted
 * ref returns?
 * Slices

## Tools

 * [Visual Studio 2017](https://www.visualstudio.com/vs/visual-studio-2017-rc/)
 * [.NET 4.6.2 develoepr pack](https://www.microsoft.com/en-us/download/confirmation.aspx?id=53321)

## Questions

 * Can I target older version of .NET and use C# 7?

# Value Tuples

 * Quick start guide: https://github.com/dotnet/roslyn/blob/master/docs/features/tuples.md
 * Proposal: https://github.com/dotnet/roslyn/issues/347
 * You need to install System.ValueTuple NuGet package to use tuples.
 * The ability to give a name to each elements is very useful!
 * **Names are only C# compiler syntax sugar**. They aren't represented at runtime!
 * **DO NOT LET TUPLES CROSS ASSEMBLY BOUNDARIES!**
 * [ValueTuple](https://github.com/dotnet/corefx/blob/master/src/System.ValueTuple/src/System/ValueTuple/ValueTuple.cs) != Tuple. It is a **struct** with **mutable** fieds. 
 * Why a struct? It is quicker (assuming creations will be more frequent then allocations) and don't require heap allocations
 * Equality and GetHashCode are re-implemented for efficiency (instead of relying on the default implementation in struct)
 * Conversions: identity, implicit , implicit nullable
 * Deconstruction allows to splat a tuple
 * Deconstruction can be used with any type with a Deconstruct method (included the old Tuple class)
 * The old Tuple type has being extended with extension methods to allow deconstruction
 * The compiler is smart in decostructing ValueTuple. No Decostruct method is involved. The compiler automatically does the assignments.

 # Pattern Matching

 * Proposal https://github.com/dotnet/roslyn/blob/features/patterns/docs/features/patterns.md
 * Tuple in the case?
 * expression is Type Identifier
 * case Pattern when Expression (type identifier, var identifier, constant)

# Nested Local Functions
 * Proposal: https://github.com/dotnet/roslyn/issues/259
 * Your can't have two local functions with the same name even if arguments list if different

# Other:
 * Equals(object) using is statement to delegate to IEquatable => obj is Type t ? Equals(t) : false