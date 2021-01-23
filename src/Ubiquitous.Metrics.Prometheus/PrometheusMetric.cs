using System.Linq;
using Prometheus;
using Ubiquitous.Metrics.Internals;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Prometheus {
    abstract class PrometheusMetric {
        protected string[] DefaultLabels { get; }

        protected PrometheusMetric(DefaultLabel[] defaultLabels) => DefaultLabels = defaultLabels.GetLabels();

        public TChild CombineLabels<TChild>(Collector<TChild> collector, Label[]? labels)
            where TChild : ChildBase {
            return collector.Labels(DefaultLabels.SafeUnion(labels).ToArray());
        }
    }
}
