using System;
using System.Diagnostics;
using Ubiquitous.Metrics.Internals;

namespace Ubiquitous.Metrics.Combined {
    public class CombinedHistogram : IHistogramMetric {
        readonly IHistogramMetric[] _inner;

        internal CombinedHistogram(IHistogramMetric[] inner) => _inner = inner;

        public void Observe(Stopwatch stopwatch, string? label = null, int count = 1)
            => _inner.ForEach(x => x.Observe(stopwatch, label, count));

        public void Observe(Stopwatch stopwatch, string[]? labels = null, int count = 1)
            => _inner.ForEach(x => x.Observe(stopwatch, labels, count));

        public void Observe(DateTimeOffset when, string? label = null, int count = 1)
            => _inner.ForEach(x => x.Observe(when, label));

        public void Observe(DateTimeOffset when, string[]? labels = null, int count = 1)
            => _inner.ForEach(x => x.Observe(when, labels));

        public void Observe(TimeSpan duration, string? label = null, int count = 1)
            => _inner.ForEach(x => x.Observe(duration, label, count));

        public void Observe(TimeSpan duration, string[]? labels = null, int count = 1)
            => _inner.ForEach(x => x.Observe(duration, labels, count));
    }
}
