# Changelog

All notable changes to this project will be automatically documented in this file.


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
