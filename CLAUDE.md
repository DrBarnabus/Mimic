# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build Commands

### Primary Commands

- `dotnet run --project build/Build` - Main build command that restores, builds the solution
- `dotnet run --project build/Build -- --target=Test` - Run unit tests with coverage
- `dotnet run --project build/Build -- --target=Package` - Build and package for NuGet
- `dotnet run --project build/Publish -- --target=PublishNuGet` - Publish to NuGet (CI only)

### Testing Commands

- `dotnet test src/Mimic.UnitTests/Mimic.UnitTests.csproj` - Run tests directly
- `dotnet test src/Mimic.UnitTests/Mimic.UnitTests.csproj --framework net8.0` - Test specific framework
- `dotnet test src/Mimic.UnitTests/Mimic.UnitTests.csproj --framework net9.0` - Test on .NET 9

## Project Structure

### Core Architecture

Mimic is a .NET mocking library built on Castle DynamicProxy with a fluent API design:

- **Main Library** (`src/Mimic/`): The core mocking functionality
    - `Mimic<T>` - Main generic mimic class for creating mock objects
    - `Setup/` - Fluent API for configuring method/property behaviours
    - `Proxy/` - Castle DynamicProxy integration for runtime proxy generation
    - `Expressions/` - Expression tree handling for type-safe setup syntax

- **Test Project** (`src/Mimic.UnitTests/`): Comprehensive unit tests
    - Multi-target framework support (net8.0, net9.0)
    - Uses xUnit, Shouldly, and AutoFixture
    - Organised by feature area (Setup/, Expressions/, Core/, etc.)

### Key Components

- **Mimic<T>**: Generic wrapper providing fluent API for mock configuration
- **Setup System**: Expression-based method/property setup with behaviours
- **Proxy Generation**: Runtime proxy creation using Castle DynamicProxy
- **Argument Matching**: Type-safe argument matchers (`Arg.Any<T>()`, etc.)
- **Verification**: Post-execution verification of method calls

### Build System

Custom Cake-based build system in `build/` directory:

- **Build Project**: Main build tasks (Clean, Build, Test, Package)
- **Publish Project**: NuGet publishing tasks
- **Common**: Shared utilities and models
- Uses GitVersion for semantic versioning
- Coverlet for code coverage with Codecov integration

## Development Guidelines

### Testing Framework

- Primary: xUnit with Shouldly assertions
- Test data: AutoFixture for test data generation
- Coverage: Coverlet with 80%+ target coverage
- Multi-framework testing on net8.0 and net9.0

### Code Organisation

- Follow existing namespace patterns (Mimic.Core, Mimic.Setup, etc.)
- Use fluent interface patterns for public APIs
- Internal classes use Castle DynamicProxy conventions
- Expression trees for type-safe configuration

### Package Management

- Central Package Management via Directory.Packages.props
- Package lock files are enabled for reproducible builds
- Castle.Core dependency for proxy generation
- JetBrains.Annotations for code contracts
