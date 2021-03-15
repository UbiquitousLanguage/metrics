using JetBrains.Annotations;

namespace Ubiquitous.Metrics.InMemory {
    /// <summary>
    /// -memory metrics provider, allows you to do measurements for diagnostic purposes.
    /// </summary>
    [PublicAPI]
    public class InMemoryMetricsProvider : IMetricsProvider {
        public ICountMetric CreateCount(MetricDefinition definition) => new InMemoryCount();

        public ICountMetric<T> CreateCount<T>(MetricDefinition<T> definition) => new InMemoryCount<T>();

        public IHistogramMetric CreateHistogram(MetricDefinition definition) => new InMemoryHistogram();

        public IGaugeMetric CreateGauge(MetricDefinition definition) => new InMemoryGauge();
    }
}