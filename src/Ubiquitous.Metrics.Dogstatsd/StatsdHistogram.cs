using System;
using System.Linq;
using StatsdClient;
using Ubiquitous.Metrics.Labels;
using Stopwatch = System.Diagnostics.Stopwatch;

namespace Ubiquitous.Metrics.Dogstatsd {
    class StatsdHistogram : StatsdMetric, IHistogramMetric {
        readonly BaseHistogram _base;

        internal StatsdHistogram(MetricDefinition metricDefinition) : base(metricDefinition) {
            _base = new BaseHistogram();
        }

        public double Sum => _base.Sum;
        public long Count => _base.Count;

        public void Observe(Stopwatch stopwatch, LabelValue[]? labels, int count = 1) {
            Observe(stopwatch.Elapsed.TotalSeconds, count, labels);
            _base.Observe(stopwatch.Elapsed.TotalSeconds, count);
        }

        public void Observe(DateTimeOffset when, params LabelValue[] labels) {
            var sec = (DateTimeOffset.UtcNow - when).TotalSeconds;
            Observe(sec, 1, labels);
            _base.Observe(sec);
        }

        public void Observe(TimeSpan duration, LabelValue[]? labels = null, int count = 1) {
            Observe(duration.TotalSeconds, count, labels);
            _base.Observe(duration.TotalSeconds, count);
        }

        void Observe(double value, int count, LabelValue[]? labels) {
            if (count == 1) {
                DogStatsd.Histogram(MetricName, value, tags: FormTags(labels));
                return;
            }

            var singleValue = value / count;

            foreach (var _ in Enumerable.Range(0, count))
                DogStatsd.Histogram(MetricName, singleValue, tags: FormTags(labels));
        }
    }
}