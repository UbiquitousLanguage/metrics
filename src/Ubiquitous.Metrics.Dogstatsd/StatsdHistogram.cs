using System;
using System.Linq;
using System.Threading;
using StatsdClient;
using Ubiquitous.Metrics.Internals;
using Ubiquitous.Metrics.Labels;
using Stopwatch = System.Diagnostics.Stopwatch;

namespace Ubiquitous.Metrics.Dogstatsd {
    class StatsdHistogram : StatsdMetric, IHistogramMetric {
        internal StatsdHistogram(MetricDefinition metricDefinition) : base(metricDefinition) { }

        public double Sum => _sum;
        public long Count => _count;

        double _sum;
        long   _count;

        public void Observe(Stopwatch stopwatch, LabelValue[]? labels, int count = 1) {
            foreach (var _ in Enumerable.Range(0, count))
                DogStatsd.Histogram(MetricName, stopwatch.Elapsed.TotalSeconds, tags: FormTags(labels));
            Interlocked.Exchange(ref _sum, _sum + stopwatch.WatchSum());
            Interlocked.Add(ref _count, count);
        }

        public void Observe(DateTimeOffset when, params LabelValue[] labels) {
            DogStatsd.Histogram(MetricName, (DateTimeOffset.UtcNow - when).TotalSeconds, tags: FormTags(labels));
            Interlocked.Exchange(ref _sum, _sum + (DateTimeOffset.Now - when).TotalSeconds);
            Interlocked.Increment(ref _count);
        }
    }
}