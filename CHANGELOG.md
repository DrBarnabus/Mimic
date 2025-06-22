# Changelog

All notable changes to this project will be automatically documented in this file.


## [0.6.0](https://github.com/DrBarnabus/Mimic/compare/v0.5.0...v0.6.0) (2025-06-22)


### Features

* Add `WithDelay` method to allow setting up delays for function invocations ([#8](https://github.com/DrBarnabus/Mimic/issues/8)) ([55cad2b](https://github.com/DrBarnabus/Mimic/commit/55cad2b68aee829312af828d9354516c5e26f5f0))
* Rename of `Verifiable` to `Expected` and `Verify*` to `Verify*Received` ([b5c73de](https://github.com/DrBarnabus/Mimic/commit/b5c73de90fff706d45aad32a1b9df3724e46f93a))
* Support for `Proceed()`'ing to base implementation ([#5](https://github.com/DrBarnabus/Mimic/issues/5)) ([8d3cc63](https://github.com/DrBarnabus/Mimic/commit/8d3cc63f657d77d6cb010b94b67a022a94cd2c7e))
* Support for mimicking classes ([#4](https://github.com/DrBarnabus/Mimic/issues/4)) ([26d73e9](https://github.com/DrBarnabus/Mimic/commit/26d73e96bc650fbb46e500d10609e791e0dc05a5))
* Support for setup of nested method calls ([#6](https://github.com/DrBarnabus/Mimic/issues/6)) ([4d26ef5](https://github.com/DrBarnabus/Mimic/commit/4d26ef584c1a5809a38392726f14fd2dab37a95c))
* Unify exceptions under `MimicException` type ([913103e](https://github.com/DrBarnabus/Mimic/commit/913103ea3860c4e78b501abd1ea365639a33735b))


### Bug Fixes

* Correct inheritance of `Strict` mode for nested `IMimic<T>` instances ([48470dd](https://github.com/DrBarnabus/Mimic/commit/48470dd9bd4a169951a340418db64930d6971688))

## [0.5.0](https://github.com/DrBarnabus/Mimic/compare/v0.4.0...v0.5.0) (2024-02-07)


### Features

* Add `Verify` methods for verifying non-setup invocations ([2fb2566](https://github.com/DrBarnabus/Mimic/commit/2fb2566c0cb7ff12636ba09b7fba5cc2d40574a1))


### Bug Fixes

* Correctly override `ToString`, `Equals` and `GetHashCode` on mimicked objects ([1c2432e](https://github.com/DrBarnabus/Mimic/commit/1c2432e6b980be734727d5440079a85af7369bb9))
* Enable use of `IGenericMatcher` types when using Arg matchers ([f20d9f5](https://github.com/DrBarnabus/Mimic/commit/f20d9f5036382310b80bb9d8b547fbbac8463575))

## [0.4.0](https://github.com/DrBarnabus/Mimic/compare/v0.3.2...v0.4.0) (2023-12-14)


### Features

* Add `AsSequence()` method to allow for setting up a sequence of returns/throws for repeated calls ([#3](https://github.com/DrBarnabus/Mimic/issues/3)) ([3d5c8ab](https://github.com/DrBarnabus/Mimic/commit/3d5c8abaf78dd68757004bc4ab6e4ba971d0ee36))
* Add Extension methods for `.Returns` of `Task<T>` and `ValueTask<T>` ([#2](https://github.com/DrBarnabus/Mimic/issues/2)) ([343ad00](https://github.com/DrBarnabus/Mimic/commit/343ad001ba506853edd03def9359ad47c61166a2))
* Add new argument matchers `Arg.In<TValue>` and `Arg.NotIn<TValue>` ([e250c5d](https://github.com/DrBarnabus/Mimic/commit/e250c5d19290aa12b399137aedeca4203dc09260))
* Add option to `.Limit(...)` number of allowed executions ([a031467](https://github.com/DrBarnabus/Mimic/commit/a0314678bcf88d4b2cdc2223437ed373de24e396))
* Add support for `Generic` type matchers in setups ([87e56de](https://github.com/DrBarnabus/Mimic/commit/87e56def947175fe967fb0fb4e63c92c465a5949))
* Support for setting up methods with `ref`/`in`/`out` parameters ([32b955b](https://github.com/DrBarnabus/Mimic/commit/32b955be94b0438eb22000e5b8fe0187abd20d0a))

### [0.3.2](https://github.com/DrBarnabus/Mimic/compare/v0.3.1...v0.3.2) (2023-11-18)

_Pushed again to fix the Icon of the Package_

### [0.3.1](https://github.com/DrBarnabus/Mimic/compare/v0.3.0...v0.3.1) (2023-11-17)


### Bug Fixes

* Change icon_128.jpeg to PNG ([4c2456e](https://github.com/DrBarnabus/Mimic/commit/4c2456eb930192747e8007fe94af1bd11aaa443a))

## [0.3.0](https://github.com/DrBarnabus/Mimic/compare/v0.2.0...v0.3.0) (2023-11-17)


### Features

* Add `Verifiable` flag to setups and `Verify` method on `Mimic<T>` ([6cbac49](https://github.com/DrBarnabus/Mimic/commit/6cbac49753a43bb6934eebbc0971d3e78e9e7561))
* Implement `Strict` mode (default: `true`) to require an invocation to have a matching setup or else throw an exception ([5c54b69](https://github.com/DrBarnabus/Mimic/commit/5c54b69b4a156684e44ed5a641b8b1fc76b3c3e5))
* Implement conditional setups with `Mimic<T>.When(Func<bool> condition)` ([97ac200](https://github.com/DrBarnabus/Mimic/commit/97ac20014b7851ff397b009f885b0a944ad70b82))


### Bug Fixes

* Change ExpressionSplitter to maintain mocked type for method call expressions ([d8fd281](https://github.com/DrBarnabus/Mimic/commit/d8fd28142aeee1e913bec0446987090903d35655))
* Properly implement `ToString` on `SetupBase` ([4669d18](https://github.com/DrBarnabus/Mimic/commit/4669d188638ae6a7eae6490176efa03a375c180a))

## [0.2.0](https://github.com/DrBarnabus/Mimic/compare/v0.1.0...v0.2.0) (2023-10-27)


### Features

* add `SetupAllProperties()` method for property stubbing to `Mimic<T>` ([74faafb](https://github.com/DrBarnabus/Mimic/commit/74faafb49c5039a83b9b5524ee4b2a892a3e16eb))
* add `SetupGet<TProperty>(...)` method to `Mimic<T>` ([8290e67](https://github.com/DrBarnabus/Mimic/commit/8290e67998ca795f125655f93029b064a0f3aebc))
* add `SetupProperty<TProperty>(...)` method for property stubbing to `Mimic<T>` ([a857e48](https://github.com/DrBarnabus/Mimic/commit/a857e48c748c7033a1cafe034ce832ab432e708d))
* add `SetupSet(...)` and `SetupSet<TProperty>(...)` methods to `Mimic<T>` ([d5b78f8](https://github.com/DrBarnabus/Mimic/commit/d5b78f8aa7ab3749b547f2dfe3efd850b204b7aa))
