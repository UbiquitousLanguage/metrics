# Ubiquitous Metrics

Abstractions for metrics with implementation for APM providers.

## Why?

Metrics in OpenTelemetry for .NET is still out sight, and we have a need to expose metrics to different vendors.

In order to avoid rewriting the whole measurement when switching from one vendor to another, we decided to create a
small abstraction layer, which exposes similar metrics in a uniform fashion.

Then, we can configure a specific provider to expose the measurements properly. By default, measurements will still be
performed, even when no provider is configures, using the no-op provider. It works great for tests, where you won't need
to care about configuring, for example, Prometheus, to prevent the test from crashing.

It should be trivial also to create a provider for a benchmarking tool and collect performance evidence when running the
tests.

## Essentials

The library provides three metric types:

- Counter
- Gauge
- Histogram

Having histograms support essentially limits the number of providers we could support, but we accepted this trade-off
due to the high value of histogram metrics.

### Getting started

In your application, you start by creating an instance of the `Metrics` class with one or more configuration providers.
Configuration providers implement specifics for a given observability product (Prometheus, Datadog, etc).

You might want to isolate the initial but to a separate function, which you then call from your `Startup`:

```csharp
public static class Measurements {
    public static void ConfigureMetrics(string environment) {
        var metrics = Metrics.CreateUsing(
            new PrometheusConfigurator(
                new Label("app", "myAwesomeApp"),
                new Label("environment", environment)
            )
        );
        MyAwesomeAppMetrics.Configure(metrics);
    }
}
```

In the snipped above, we added the default labels `app` and `environment`, which will be added to all the measurements implicitly.

Then, in the `MyAwesomeAppMetrics.Configure` function, you need to configure all the metrics needed for your
application:

```csharp
public static class MyAwesomeMetrics {
    public static void Configure(Metrics metrics) {
        CurrentUsers    = metrics.CreateGauge("current_users", "The number of users on the site");
        HttpApiErrors   = metrics.CreateCount("api_errors", "Number of requests, which failed");
        HttpApiResponse = metrics.CreateHistogram("http_response_time", "HTTP request processing time");
    }

    public static IGaugeMetric CurrentUsers { get; private set; } = null!;

    public static IHistogramMetric HttpApiResponse { get; private set; } = null!;

    public static ICountMetric HttpApiErrors { get; private set; } = null!;
}
```

As you probably want to see measurements per resource, you can add labels, identifying the resource, which is being observed:

```csharp
HttpApiResponse = metrics.CreateHistogram(
    "http_response_time", 
    "HTTP request processing time",
    "http_resource",
    "http_method"
);
```

Then, when you observe the execution time, you need to supply the label value:

```csharp
app.Use((context, next) => Metrics.Measure(next, 
    MyAwesomeMetrics.HttpApiResponse, 
    MyAwesomeMetrics.HttpApiErrors, 
    new LabelValue[] {
        context.Request.Path.Value,
        context.Request.Method
    })
); 
```

In this simple HTTP middleware, we use the `Measure` function, which calls the specified action, wrapped in the time measurement.
The metric histogram will get the observation, where the time is measured in seconds.

### Benefits

In your application, you'd need to configure the metrics instance with a proper provider, so you get your application properly measured.

However, you might want to avoid this in your test project. There, you can use the `NoMetrics` configuration provider. When instantiating the `Metrics` instance without any provider supplied, the `NoMetrics` provider will be used.

The `Metrics.Instance` static member will implicitly create an instance of the `Metrics` class with `NoMetrics` provider.

```csharp
public class MyTestFixture {
    static MyTextFixture() => MyAwesomeAppMetrics.Configure(Metrics.Instance);
    
    // here are my other setups
}
```

Another scenario would be that your organisation is using Datadog APM to measure apps in production, but when running locally, you'd still like to measure and you don't have the Datadog agent running on your dev machine.

In this case, you can run Prometheus and Grafana locally in Docker and use different configuration providers, based on the environment name.

```csharp
public static class Measurements {
    public static void ConfigureMetrics(string environment) {
        IMetricsProvider configProvider = environment == "Development"
            ? new PrometheusConfigurator()
            : new StatsdConfigurator(new [] {
                  new Label("app", "myAwesomeApp"),
                  new Label("environment", environment)
              });
        );
        MyAwesomeAppMetrics.Configure(Metrics.CreateUsing(configProvider));
    }
}
```

## Vendors support

Currently, the library supports exposing metrics for:

- Prometheus (using [prometheus-net](https://github.com/prometheus-net/prometheus-net))
- Datadog Statsd (using [Dogstatsd](https://github.com/DataDog/dogstatsd-csharp-client))

## Licence

MIT