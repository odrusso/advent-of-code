# Advent of Code Solutions

This repository contains solutions to various Advent of Code challenges.

## Structure

### C# Solutions (`/src`)

The .NET projects follow a standard structure:

- **`AdventOfCode.sln`** - Solution file at the root
- **`src/AdventUtils/`** - Shared utility library with base classes for Advent of Code challenges
- **`src/AdventOfCode2023/`** - C# solutions for Advent of Code 2023
- **`src/AdventOfCode2024/`** - C# solutions for Advent of Code 2024

Each Day solution inherits from `AbstractDay` and implements:
- `ProcessPartOne(string[] input)` - Solution for part 1
- `ProcessPartTwo(string[] input)` - Solution for part 2

Input files are automatically located using the `GetLines()` method which looks for `input.txt` in the same directory as the Day class.

### Python Solutions (`/python`)

Earlier solutions written in Python:

- **`python/2020/`** - Python solutions for Advent of Code 2020
- **`python/2021/`** - Python solutions for Advent of Code 2021

## Building

To build all C# projects:

```bash
dotnet build
```

To run a specific year:

```bash
dotnet run --project src/AdventOfCode2024
```

## Development

The solution uses:
- .NET 10.0
- BenchmarkDotNet for performance testing
- MSTest for testing (AdventOfCode2024)
