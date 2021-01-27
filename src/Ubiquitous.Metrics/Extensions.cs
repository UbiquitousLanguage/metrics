using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics {
    public static class Extensions {
        public static ICountMetric CreateCount(this IMetrics metrics, string name, string? description, params LabelName[] labelNames)
            => metrics.CreateCount(new MetricDefinition(name, description, labelNames));

        public static IHistogramMetric CreateHistogram(this IMetrics metrics, string name, string? description, params LabelName[] labelNames)
            => metrics.CreateHistogram(new MetricDefinition(name, description, labelNames));

        public static IGaugeMetric CreateGauge(this IMetrics metrics, string name, string? description, params LabelName[] labelNames)
            => metrics.CreateGauge(new MetricDefinition(name, description, labelNames));
    }
}
