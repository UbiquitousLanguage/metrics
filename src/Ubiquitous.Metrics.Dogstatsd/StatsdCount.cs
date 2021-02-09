using System.Threading;
using StatsdClient;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Dogstatsd {
    class StatsdCount : StatsdMetric, ICountMetric {
        internal StatsdCount(MetricDefinition metricDefinition) : base(metricDefinition) { }

        public void Inc(int count = 1, params LabelValue[] labels) {
            DogStatsd.Increment(MetricName, count, tags: FormTags(labels));
            Interlocked.Add(ref _count, count);
        }

        public long Count => _count;

        long _count;
    }
}
