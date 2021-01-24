using System;
using System.Linq;
using StatsdClient;
using Ubiquitous.Metrics.Labels;
using Stopwatch = System.Diagnostics.Stopwatch;

namespace Ubiquitous.Metrics.Dogstatsd {
    class StatsdHistogram : StatsdMetric, IHistogramMetric {
        internal StatsdHistogram(MetricDefinition metricDefinition) : base(metricDefinition) { }

        public void Observe(Stopwatch stopwatch, LabelValue[]? labels, int count = 1) {
            foreach (var _ in Enumerable.Range(0, count)) DogStatsd.Histogram(MetricName, stopwatch.Elapsed.TotalSeconds, tags: FormTags(labels));
        }

        public void Observe(DateTimeOffset when, params LabelValue[] labels)
            => DogStatsd.Histogram(MetricName, (DateTimeOffset.UtcNow - when).TotalSeconds, tags: FormTags(labels));
    }
}
