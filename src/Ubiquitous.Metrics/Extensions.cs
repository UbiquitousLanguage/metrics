using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics;

/// <summary>
/// Extensions to simplify creation of metrics.
/// </summary>
[PublicAPI]
public static class Extensions {
    /// <summary>
    /// Creates a counter metric. Constructs the <seealso cref="MetricDefinition"/> from the provided arguments.
    /// </summary>
    /// <param name="metricsProvider">Metrics provider instance</param>
    /// <param name="name">Metric name. Ensure you use the name supported by the provider you'll use.</param>
    /// <param name="description">Metric description, which is not supported for all the providers.</param>
    /// <param name="labelNames">Label names. You'd need to supply the exact number of label values when observing the metric.</param>
    /// <returns></returns>
    public static ICountMetric CreateCount(this Metrics metricsProvider, string name, string? description, params LabelName[] labelNames)
        => metricsProvider.CreateCount(new MetricDefinition(name, description, labelNames));

    /// <summary>
    /// Creates a gauge metric. Constructs the <seealso cref="MetricDefinition"/> from the provided arguments.
    /// </summary>
    /// <param name="metricsProvider">Metrics provider instance</param>
    /// <param name="name">Metric name. Ensure you use the name supported by the provider you'll use.</param>
    /// <param name="description">Metric description, which is not supported for all the providers.</param>
    /// <param name="labelNames">Label names. You'd need to supply the exact number of label values when observing the metric.</param>
    /// <returns></returns>
    public static IHistogramMetric CreateHistogram(this Metrics metricsProvider, string name, string? description, params LabelName[] labelNames)
        => metricsProvider.CreateHistogram(new MetricDefinition(name, description, labelNames));

    /// <summary>
    /// Creates a histogram metric. Constructs the <seealso cref="MetricDefinition"/> from the provided arguments.
    /// </summary>
    /// <param name="metricsProvider">Metrics provider instance</param>
    /// <param name="name">Metric name. Ensure you use the name supported by the provider you'll use.</param>
    /// <param name="description">Metric description, which is not supported for all the providers.</param>
    /// <param name="labelNames">Label names. You'd need to supply the exact number of label values when observing the metric.</param>
    /// <returns></returns>
    public static IGaugeMetric CreateGauge(this Metrics metricsProvider, string name, string? description, params LabelName[] labelNames)
        => metricsProvider.CreateGauge(new MetricDefinition(name, description, labelNames));
}