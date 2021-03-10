using Ubiquitous.Metrics.Internals;

namespace Ubiquitous.Metrics.Combined {
    public class CombinedCount : ICountMetric {
        readonly ICountMetric[] _inner;

        internal CombinedCount(ICountMetric[] inner) => _inner = inner;

        public void Inc(string? label = null, int count = 1) => _inner.ForEach(x => x.Inc(label, count));

        public void Inc(string[]? labels, int count = 1) => _inner.ForEach(x => x.Inc(labels, count));
    }
}