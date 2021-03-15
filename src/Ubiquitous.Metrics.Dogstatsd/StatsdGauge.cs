using StatsdClient;

namespace Ubiquitous.Metrics.Dogstatsd {
    class StatsdGauge : StatsdMetric, IGaugeMetric {
        internal StatsdGauge(MetricDefinition definition) : base(definition) { }

        public void Set(double value, string[]? labels) => DogStatsd.Gauge(MetricName, value, tags: FormTags(labels));
    }
}
