# Mimic

#### Fast, friendly and familiar mocking library for modern .NET

[![GitHub Release][gh-release-badge]][gh-release]
[![NuGet Downloads][nuget-downloads-badge]][nuget-downloads]
[![Build Status][gh-actions-badge]][gh-actions]

#### [Planned Features](#planned-features) | [License](#license)

---

**Mimic** is still very early in it's development and the functionality and/or interfaces that it provides are subject to change without warning between versions until the v1 release.

## Planned Features

```
Considering = ❓
Planned     = 📅
In-Progress = 🚧
Completed   = ✔
```

-   Mocking of:
    -   [✔] Interfaces
    -   [❓] Classes (inc support for arguments and calling the base implementation of mocked methods)
-   [✔] Setup of Methods
-   [🚧] Setup of Getters & Setters
-   [📅] Setup of Property Stubbing
-   [📅] Support for `ref`/`out` arguments
-   [📅] Verifiable Setup's
-   [📅] "Strict" Setup mode (Calls throw if not setup)
-   [📅] Execution Limits (Calls throw after n expected calls)
-   [📅] Conditional Setup of Methods
-   [📅] Sequential Returns (Calls return next sequential result on each call `.Returns(value1, value2, value3)`)
-   [❓] Setup of Event Handlers

Mimic makes use of [Castle.Core](https://www.castleproject.org/projects/dynamicproxy)'s `DynamicProxy` internally for generating proxies of types to mock.

# License

Licensed under [MIT](./LICENSE), Copyright (c) 2023 Daniel Woodward

<!-- Links -->

[gh-release-badge]: https://img.shields.io/github/v/release/DrBarnabus/Mimic?color=g&style=for-the-badge
[gh-release]: https://github.com/DrBarnabus/Mimic/releases/latest
[nuget-downloads-badge]: https://img.shields.io/nuget/dt/Mimic?color=g&logo=nuget&style=for-the-badge
[nuget-downloads]: https://www.nuget.org/packages/Mimic
[gh-actions-badge]: https://img.shields.io/github/actions/workflow/status/DrBarnabus/Mimic/ci.yml?logo=github&branch=main&style=for-the-badge
[gh-actions]: https://github.com/DrBarnabus/Mimic/actions/workflows/ci.yml
