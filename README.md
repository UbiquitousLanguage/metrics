# Ubiquitous Metrics

Abstractions for metrics with implementation for APM providers.

## Why?

Metrics in OpenTelemetry for .NET is still out sight, and we have a need to expose metrics to different vendors.

In order to avoid rewriting the whole measurement when switching from one vendor to another, we decided to create a small abstraction layer, which exposes similar metrics in a uniform fashion.

Then, we can configure a specific provider to expose the measurements properly. By default, measurements will still be performed, even when no provider is configures, using the no-op provider. It works great for tests, where you won't need to care about configuring, for example, Prometheus, to prevent the test from crashing.

It should be trivial also to create a provider for a benchmarking tool and collect performance evidence when running the tests.

## Vendors support

Currently, the library supports exposing metrics for:
- Prometheus
- Datadog Statsd

## Licence

MIT