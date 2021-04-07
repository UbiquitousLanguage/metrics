using StatsdClient;

namespace Ubiquitous.Metrics.Dogstatsd {
    class StatsdGauge : StatsdMetric, IGaugeMetric {
        internal StatsdGauge(IDogStatsd service, MetricDefinition metricDefinition) : base(service, metricDefinition) { }

        public void Set(double value, string[]? labels) => Service.Gauge(MetricName, value, tags: FormTags(labels));
    }
}
