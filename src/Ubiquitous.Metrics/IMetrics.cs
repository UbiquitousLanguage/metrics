namespace Ubiquitous.Metrics {
    public interface IMetrics {
        ICountMetric CreateCount(MetricDefinition definition);

        IHistogramMetric CreateHistogram(MetricDefinition definition);

        IGaugeMetric CreateGauge(MetricDefinition definition);
    }
}
