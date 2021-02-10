using StatsdClient;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Dogstatsd {
    class StatsdCount : StatsdMetric, ICountMetric {
        internal StatsdCount(MetricDefinition metricDefinition) : base(metricDefinition) {
            _base = new BaseCount();
        }

        public void Inc(int count = 1, params LabelValue[] labels) {
            DogStatsd.Increment(MetricName, count, tags: FormTags(labels));
            _base.Inc(count);
        }

        public long Count => _base.Count;

        readonly BaseCount _base;
    }
}
