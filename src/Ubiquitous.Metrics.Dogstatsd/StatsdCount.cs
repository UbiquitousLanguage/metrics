using StatsdClient;

namespace Ubiquitous.Metrics.Dogstatsd {
    class StatsdCount : StatsdMetric, ICountMetric {
        internal StatsdCount(MetricDefinition metricDefinition) : base(metricDefinition) { }

        public void Inc(string[]? labels = null, int count = 1)
            => DogStatsd.Increment(MetricName, count, tags: FormTags(labels));
    }
}