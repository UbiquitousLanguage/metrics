using System.Collections.Generic;
using Ubiquitous.Metrics.Internals;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Combined {
    public class CombinedGauge : IGaugeMetric {
        readonly ICollection<IGaugeMetric> _inner;

        internal CombinedGauge(ICollection<IGaugeMetric> inner) => _inner = inner;

        public void Set(double value, params Label[]? labels) => _inner.ForEach(x => x.Set(value, labels));
    }
}
