using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Prometheus;

/// <summary>
/// Prometheus metrics configurator
/// </summary>
[PublicAPI]
public class PrometheusConfigurator : IMetricsProvider {
    /// <summary>
    /// Instantiate the Prometheus metrics configurator
    /// </summary>
    /// <param name="defaultLabels">Optional: default labels, which will be added for all the configured metrics.
    /// Usually, things like the app name and the environment are used as default labels.</param>
    public PrometheusConfigurator(params Label[] defaultLabels)
        => global::Prometheus.Metrics.DefaultRegistry.SetStaticLabels(defaultLabels.ToDictionary());

    public ICountMetric CreateCount(MetricDefinition metricDefinition)
        => new PrometheusCount(metricDefinition);

    public IHistogramMetric CreateHistogram(MetricDefinition metricDefinition)
        => new PrometheusHistogram(metricDefinition);

    public IGaugeMetric CreateGauge(MetricDefinition metricDefinition)
        => new PrometheusGauge(metricDefinition);
}