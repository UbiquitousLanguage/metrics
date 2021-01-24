using JetBrains.Annotations;
using StatsdClient;
using Ubiquitous.Metrics.Internals;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Dogstatsd {
    [PublicAPI]
    public class StatsdConfigurator : IMetrics {
        public StatsdConfigurator(Label[] defaultLabels, string? prefix = null, string? server = null)
            => DogStatsd.Configure(
                new StatsdConfig {
                    ConstantTags     = StatsTags.FormTags(defaultLabels.GetLabelNames(), defaultLabels.GetLabels()),
                    StatsdServerName = server,
                    Prefix           = prefix
                }
            );

        public ICountMetric CreateCount(MetricDefinition metricDefinition) => new StatsdCount(metricDefinition);

        public IHistogramMetric CreateHistogram(MetricDefinition metricDefinition) => new StatsdHistogram(metricDefinition);

        public IGaugeMetric CreateGauge(MetricDefinition metricDefinition) => new StatsdGauge(metricDefinition);
    }
}
