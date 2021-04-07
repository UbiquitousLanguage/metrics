using StatsdClient;

namespace Ubiquitous.Metrics.Dogstatsd {
    class StatsdCount : StatsdMetric, ICountMetric {
        internal StatsdCount(IDogStatsd service, MetricDefinition metricDefinition) : base(service, metricDefinition) { }

        public void Inc(string[]? labels = null, int count = 1)
            => Service.Increment(MetricName, count, tags: FormTags(labels));
    }
}