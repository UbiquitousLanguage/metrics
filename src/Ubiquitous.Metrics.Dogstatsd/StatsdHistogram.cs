using System;
using System.Linq;
using StatsdClient;
using Stopwatch = System.Diagnostics.Stopwatch;

namespace Ubiquitous.Metrics.Dogstatsd {
    class StatsdHistogram : StatsdMetric, IHistogramMetric {
        readonly BaseHistogram _base;

        internal StatsdHistogram(MetricDefinition metricDefinition) : base(metricDefinition) => _base = new BaseHistogram();

        public double Sum => _base.Sum;
        public long Count => _base.Count;

        public void Observe(Stopwatch stopwatch, string[]? labels = null, int count = 1) {
            Observe(stopwatch.Elapsed.TotalSeconds, count, labels);
            _base.Observe(stopwatch.Elapsed.TotalSeconds, count);
        }

        public void Observe(DateTimeOffset when, string[]? labels = null, int count = 1) {
            var sec = (DateTimeOffset.UtcNow - when).TotalSeconds;
            Observe(sec, 1, labels);
            _base.Observe(sec);
        }

        public void Observe(TimeSpan duration, string[]? labels = null, int count = 1) {
            Observe(duration.TotalSeconds, count, labels);
            _base.Observe(duration.TotalSeconds, count);
        }

        void Observe(double value, int count, string[]? labels) {
            if (count == 1) {
                DogStatsd.Histogram(MetricName, value, tags: FormTags(labels));
                return;
            }

            foreach (var _ in Enumerable.Range(0, count))
                DogStatsd.Histogram(MetricName, value, tags: FormTags(labels));
        }
    }
}