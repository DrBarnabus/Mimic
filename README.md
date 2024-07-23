# Mimic

#### Fast, friendly and familiar mocking library for modern .NET

[![GitHub Release][gh-release-badge]][gh-release]
[![NuGet Downloads][nuget-downloads-badge]][nuget-downloads]
[![Build Status][gh-actions-badge]][gh-actions]
[![Codecov][codecov-badge]][codecov]

#### [What is Mimic](#what-is-mimic) | [Features](#features) | [Roadmap](#roadmap) | [Changelog][changelog]

---

**Mimic** is still very early in it's development and the functionality and/or interfaces that it provides are subject
to change without warning between versions until the v1 release.

## What is Mimic

**Mimic** is a friendly and familiar mocking library built for modern .NET built on top of the [Castle Project][castle]'s
dynamic proxy generator. It's simple, intuitive and type-safe API for configuring mimic's of interfaces/classes allows
for both; Setup of return values for methods/properties and verifying if method calls have been received after the fact.

```csharp
var mimic = new Mimic<ITypeToMimic>();

// Easily setup methods
mimic.Setup(m => m.IsMimicEasyToUse(Arg.Any<string>()))
    .Returns(true);

// Access the `Object` property to generate an implementation of `ITypeToMimic` and call our setup method
ITypeToMimic mimickedObject = mimic.Object;
bool whatDoYouThink = mimickedObject.IsMimicEasyToUse("it's so intuitive");

// Verify that the specified method has been called at least once on the `Object`
mimic.VerifyReceived(m => m.IsMimicEasyToUse("it's so intuitive"), CallCount.AtLeastOnce);
```

## Features

- A friendly interface designed to ease adoption by users of other popular .NET mocking libraries
- Support for generating mock objects of interfaces and overridable members in classes
- Intuitive and type-safe expression based API for setups and verification of methods
- Mimic is **strict by default**, meaning it throws for methods without a corresponding setup, but it's possible to
  disable the default behaviour by setting `Strict = false` on construction
- Quick and easy stubbing of properties to store and retrieve values
- Implicit mocking of interfaces returned by mimicked methods allowing for easy setup of nested calls
- Comprehensive set of behaviours for method setups such as; `Returns`, `Throws`, `Callback`, `When`, `Limit`,
  `Expected`, `AsSequence` and `Proceed`
- Verification of expected, setup and received calls including asserting no additional calls

## Roadmap

```
Considering = ❓ | Planned = 📅 | In-Progress = 🚧
```

- [📅] Delay behaviour (or Extension to `Returns`/`Throws`) for setups that allows for specific or random delays in
  execution time
- [❓] Setup and Verification of Event's
- [❓] Configurable default return values instead of just `null` for reference and `default` for value types when
  `Strict = false`

<!-- Badges -->
[gh-release-badge]: https://img.shields.io/github/v/release/DrBarnabus/Mimic?color=g&style=for-the-badge
[gh-release]: https://github.com/DrBarnabus/Mimic/releases/latest
[nuget-downloads-badge]: https://img.shields.io/nuget/dt/Mimic?color=g&logo=nuget&style=for-the-badge
[nuget-downloads]: https://www.nuget.org/packages/Mimic
[gh-actions-badge]: https://img.shields.io/github/actions/workflow/status/DrBarnabus/Mimic/ci.yml?logo=github&branch=main&style=for-the-badge
[gh-actions]: https://github.com/DrBarnabus/Mimic/actions/workflows/ci.yml
[codecov-badge]: https://img.shields.io/codecov/c/github/DrBarnabus/Mimic?token=znImUftZNI&style=for-the-badge&logo=codecov&logoColor=white
[codecov]: https://codecov.io/gh/DrBarnabus/Mimic

<!-- Links -->
[changelog]: https://github.com/DrBarnabus/Mimic/blob/main/CHANGELOG.md
[castle]: https://www.castleproject.org/projects/dynamicproxy
