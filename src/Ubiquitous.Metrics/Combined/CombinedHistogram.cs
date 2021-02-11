using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Ubiquitous.Metrics.Internals;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Combined {
    public class CombinedHistogram : IHistogramMetric {
        readonly ICollection<IHistogramMetric> _inner;

        internal CombinedHistogram(ICollection<IHistogramMetric> inner) => _inner = inner;

        public double Sum => _inner.First().Sum;
        public long Count => _inner.First().Count;


        public void Observe(Stopwatch stopwatch, LabelValue[]? labels = null, int count = 1)
            => _inner.ForEach(x => x.Observe(stopwatch, labels, count));

        public void Observe(DateTimeOffset when, LabelValue[]? labels = null)
            => _inner.ForEach(x => x.Observe(when, labels.ValueOrEmpty()));

        public void Observe(TimeSpan duration, LabelValue[]? labels = null, int count = 1)
            => _inner.ForEach(x => x.Observe(duration, labels, count));
    }
}
