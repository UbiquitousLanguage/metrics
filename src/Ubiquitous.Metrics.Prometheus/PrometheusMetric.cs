using System.Linq;
using Prometheus;
using Ubiquitous.Metrics.Internals;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Prometheus {
    abstract class PrometheusMetric {
        string[] DefaultLabelValues { get; }

        protected PrometheusMetric(Label[]? defaultLabels) {
            var labels = defaultLabels.ValueOrEmpty();
            DefaultLabelValues = labels.GetLabels();
        }

        protected TChild CombineLabels<TChild>(Collector<TChild> collector, LabelValue[]? labels)
            where TChild : ChildBase
            => collector.Labels(DefaultLabelValues.SafeUnion(labels.GetStrings()).ToArray());
    }
}
