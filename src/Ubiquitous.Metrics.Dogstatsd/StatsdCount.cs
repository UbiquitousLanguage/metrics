using System;
using StatsdClient;

namespace Ubiquitous.Metrics.Dogstatsd {
    class StatsdCount : StatsdMetric, ICountMetric {
        internal StatsdCount(MetricDefinition metricDefinition) : base(metricDefinition) => _base = new BaseCount();

        public void Inc()
        {
            Inc(1, Array.Empty<string>());
        }

        public void Inc(string label)
        {
            Inc(1, new[] {label});
        }

        public void Inc(int count, string label)
        {
            Inc(count,  new[] {label});
        }

        public void Inc(int count = 1, params string[] labels) {
            DogStatsd.Increment(MetricName, count, tags: FormTags(labels));
            _base.Inc(count);
        }

        public long Count => _base.Count;

        readonly BaseCount _base;
    }
}
