using StatsdClient;

namespace Ubiquitous.Metrics.Dogstatsd {
    class StatsdGauge : StatsdMetric, IGaugeMetric {
        internal StatsdGauge(MetricDefinition metricDefinition) : base(metricDefinition) { }

        public void Set(double value, string[]? labels) => DogStatsd.Gauge(MetricName, value, tags: FormTags(labels));
    }
}
