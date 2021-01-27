using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics {
    public static class Extensions {
        public static ICountMetric CreateCount(this Metrics metricsProvider, string name, string? description, params LabelName[] labelNames)
            => metricsProvider.CreateCount(new MetricDefinition(name, description, labelNames));

        public static IHistogramMetric CreateHistogram(this Metrics metricsProvider, string name, string? description, params LabelName[] labelNames)
            => metricsProvider.CreateHistogram(new MetricDefinition(name, description, labelNames));

        public static IGaugeMetric CreateGauge(this Metrics metricsProvider, string name, string? description, params LabelName[] labelNames)
            => metricsProvider.CreateGauge(new MetricDefinition(name, description, labelNames));
    }
}
