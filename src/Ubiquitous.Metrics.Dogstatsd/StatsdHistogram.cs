using System;
using System.Linq;
using System.Threading;
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
            foreach (var _ in Enumerable.Range(0, count))
                DogStatsd.Histogram(MetricName, stopwatch.Elapsed.TotalSeconds, tags: FormTags(labels));
            _base.Observe(stopwatch.Elapsed.TotalSeconds);
        }

        public void Observe(DateTimeOffset when, params LabelValue[] labels) {
            var sec = (DateTimeOffset.UtcNow - when).TotalSeconds;
            DogStatsd.Histogram(MetricName, sec, tags: FormTags(labels));
            _base.Observe(sec);
        }
    }
}