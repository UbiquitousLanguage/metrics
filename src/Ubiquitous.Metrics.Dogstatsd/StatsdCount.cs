using StatsdClient;

namespace Ubiquitous.Metrics.Dogstatsd {
    class StatsdCount : StatsdMetric, ICountMetric {
        internal StatsdCount(MetricDefinition definition) : base(definition) { }

        public void Inc(string[]? labels = null, int count = 1)
            => DogStatsd.Increment(MetricName, count, tags: FormTags(labels));
    }

    class StatsdCount<T> : StatsdCount, ICountMetric<T> {
        internal StatsdCount(MetricDefinition<T> definition) : base(definition) => GetLabels = definition.GetLabels;
        
        public GetLabels<T> GetLabels { get; }
    }
}