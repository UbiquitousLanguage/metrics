namespace Ubiquitous.Metrics {
    public interface IMetricsProvider {
        ICountMetric CreateCount(MetricDefinition definition);

        IHistogramMetric CreateHistogram(MetricDefinition definition);

        IGaugeMetric CreateGauge(MetricDefinition definition);
    }
}
