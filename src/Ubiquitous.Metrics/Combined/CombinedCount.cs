using System.Collections.Generic;
using System.Linq;
using Ubiquitous.Metrics.Internals;

namespace Ubiquitous.Metrics.Combined {
    public class CombinedCount : ICountMetric {
        readonly ICollection<ICountMetric> _inner;

        internal CombinedCount(ICollection<ICountMetric> inner) => _inner = inner;

        public void Inc(int count = 1, params string[] labels) {
            _inner.ForEach(x => x.Inc(count, labels));
        }

        public long Count => _inner.First().Count;
    }
}
