namespace Ubiquitous.Metrics.NoMetrics {
    /// <summary>
    /// No-op metrics provider, allows you to introduce metrics without a need
    /// to configure the real provider yet.
    /// </summary>
    public class NoMetricsProvider : IMetricsProvider {
        public ICountMetric CreateCount(MetricDefinition definition) => new NoCount();

        public ICountMetric<T> CreateCount<T>(MetricDefinition<T> definition) => new NoCount<T>();

        public IHistogramMetric CreateHistogram(MetricDefinition definition) => new NoHistogram();

        public IGaugeMetric CreateGauge(MetricDefinition definition) => new NoGauge();
    }
}