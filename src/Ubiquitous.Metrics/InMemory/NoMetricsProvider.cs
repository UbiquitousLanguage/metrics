using Ubiquitous.Metrics.NoMetrics;

namespace Ubiquitous.Metrics.InMemory {
    /// <summary>
    /// -memory metrics provider, allows you to do measurements for diagnostic purposes.
    /// </summary>
    public class InMemoryMetricsProvider : IMetricsProvider {
        public ICountMetric CreateCount(MetricDefinition definition) => new InMemoryCount(definition);

        public IHistogramMetric CreateHistogram(MetricDefinition definition) => new InMemoryHistogram(definition);

        public IGaugeMetric CreateGauge(MetricDefinition definition) => new InMemoryGauge(definition);
    }

    class InMemoryMetric {
        readonly MetricDefinition _definition;

        protected InMemoryMetric(MetricDefinition definition) => _definition = definition;
    }
}