using System;
using System.Linq;
using StatsdClient;
using Stopwatch = System.Diagnostics.Stopwatch;

namespace Ubiquitous.Metrics.Dogstatsd {
    class StatsdHistogram : StatsdMetric, IHistogramMetric {
        internal StatsdHistogram(IDogStatsd service, MetricDefinition metricDefinition) : base(service, metricDefinition) { }

        public void Observe(Stopwatch stopwatch, string[]? labels = null, int count = 1)
            => Observe(stopwatch.Elapsed.TotalSeconds, count, labels);

        public void Observe(DateTimeOffset when, string[]? labels = null, int count = 1) {
            var sec = (DateTimeOffset.UtcNow - when).TotalSeconds;
            Observe(sec, 1, labels);
        }

        public void Observe(TimeSpan duration, string[]? labels = null, int count = 1)
            => Observe(duration.TotalSeconds, count, labels);

        void Observe(double value, int count, string[]? labels) {
            if (count == 1) {
                Service.Histogram(MetricName, value, tags: FormTags(labels));
                return;
            }

            foreach (var _ in Enumerable.Range(0, count))
                Service.Histogram(MetricName, value, tags: FormTags(labels));
        }
    }
}