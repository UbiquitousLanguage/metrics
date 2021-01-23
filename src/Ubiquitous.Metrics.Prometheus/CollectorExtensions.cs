using System.Linq;
using Prometheus;
using Ubiquitous.Metrics.Internals;

namespace Ubiquitous.Metrics.Prometheus {
    static class CollectorExtensions {
        public static TChild WithCombinedLabels<TChild>(this Collector<TChild> collector, string[] defaultLabels, string[]? labels)
            where TChild : ChildBase
            => collector.Labels(defaultLabels.SafeUnion(labels).ToArray());
    }
}
