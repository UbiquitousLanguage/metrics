using JetBrains.Annotations;
using StatsdClient;
using Ubiquitous.Metrics.Internals;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Dogstatsd {
    /// <summary>
    /// Datadog StatsD metrics configurator
    /// </summary>
    [PublicAPI]
    public class StatsdConfigurator : IMetricsProvider {
        /// <summary>
        /// Instantiate the Datadog StatsD metrics configurator
        /// </summary>
        /// <param name="defaultLabels">Optional: default labels, which will be added for all the configured metrics.
        /// Usually, things like the app name and the environment are used as default labels.</param>
        /// <param name="prefix">Prefix, which will be added for all the metrics</param>
        /// <param name="server">StatsD server address</param>
        public StatsdConfigurator(Label[] defaultLabels, string? prefix = null, string? server = null)
            => DogStatsd.Configure(
                new StatsdConfig {
                    ConstantTags     = StatsTags.FormTags(defaultLabels.GetLabelNames(), defaultLabels.GetLabels()),
                    StatsdServerName = server,
                    Prefix           = prefix
                }
            );

        /// <summary>
        /// Should be used if DogStatsd is already configured.
        /// </summary>
        public StatsdConfigurator() { }

        public ICountMetric CreateCount(MetricDefinition metricDefinition) => new StatsdCount(metricDefinition);

        public IHistogramMetric CreateHistogram(MetricDefinition metricDefinition) => new StatsdHistogram(metricDefinition);

        public IGaugeMetric CreateGauge(MetricDefinition metricDefinition) => new StatsdGauge(metricDefinition);
    }
}
