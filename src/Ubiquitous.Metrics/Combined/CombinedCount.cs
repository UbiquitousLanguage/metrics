using System.Collections.Generic;
using Ubiquitous.Metrics.Internals;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Combined {
    public class CombinedCount : ICountMetric {
        readonly ICollection<ICountMetric> _inner;

        internal CombinedCount(ICollection<ICountMetric> inner) => _inner = inner;

        public void Inc(int count = 1, params Label[]? labels) => _inner.ForEach(x => x.Inc(count, labels));
    }
}
