using System.Collections.Generic;
using JetBrains.Annotations;

namespace Ubiquitous.Metrics.InMemory {
    /// <summary>
    /// -memory metrics provider, allows you to do measurements for diagnostic purposes.
    /// </summary>
    [PublicAPI]
    public class InMemoryMetricsProvider : IMetricsProvider {
        Dictionary<string, InMemoryCount>     _counts     = new();
        Dictionary<string, InMemoryGauge>     _gauges     = new();
        Dictionary<string, InMemoryHistogram> _histograms = new();

        public ICountMetric CreateCount(MetricDefinition definition) {
            var count = new InMemoryCount(definition);
            _counts.Add(definition.Name, count);
            return count;
        }

        public IHistogramMetric CreateHistogram(MetricDefinition definition) {
            var histogram = new InMemoryHistogram(definition);
            _histograms.Add(definition.Name, histogram);
            return histogram;
        }

        public IGaugeMetric CreateGauge(MetricDefinition definition) {
            var gauge = new InMemoryGauge(definition);
            _gauges.Add(definition.Name, gauge);
            return gauge;
        }

        /// <summary>
        /// Try getting an in-memory counter
        /// </summary>
        /// <param name="key">Metric name</param>
        /// <returns></returns>
        public InMemoryCount? GetCount(string key) => _counts.TryGetValue(key, out var count) ? count : null;

        /// <summary>
        /// Try getting an in-memory gauge
        /// </summary>
        /// <param name="key">Metric key</param>
        /// <returns></returns>
        public InMemoryGauge? GetGauge(string key) => _gauges.TryGetValue(key, out var gauge) ? gauge : null;

        /// <summary>
        /// Try getting an in-memory histogram
        /// </summary>
        /// <param name="key">Metric key</param>
        /// <returns></returns>
        public InMemoryHistogram? GetHistogram(string key)
            => _histograms.TryGetValue(key, out var histogram) ? histogram : null;
    }

    public abstract class InMemoryMetric {
        readonly MetricDefinition _definition;

        protected InMemoryMetric(MetricDefinition definition) => _definition = definition;
    }
}