using JetBrains.Annotations;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Prometheus {
    /// <summary>
    /// Prometheus metrics configurator
    /// </summary>
    [PublicAPI]
    public class PrometheusConfigurator : IMetricsProvider {
        readonly Label[] _defaultLabels;

        /// <summary>
        /// Instantiate the Prometheus metrics configurator
        /// </summary>
        /// <param name="defaultLabels">Optional: default labels, which will be added for all the configured metrics.
        /// Usually, things like the app name and the environment are used as default labels.</param>
        public PrometheusConfigurator(params Label[] defaultLabels) => _defaultLabels = defaultLabels;

        public ICountMetric CreateCount(MetricDefinition definition)
            => new PrometheusCount(definition, _defaultLabels);

        public ICountMetric<T> CreateCount<T>(MetricDefinition<T> definition)
            => new PrometheusCount<T>(definition, _defaultLabels);

        public IHistogramMetric CreateHistogram(MetricDefinition definition) 
            => new PrometheusHistogram(definition, _defaultLabels);

        public IGaugeMetric CreateGauge(MetricDefinition definition)
            => new PrometheusGauge(definition, _defaultLabels);
    }
}
