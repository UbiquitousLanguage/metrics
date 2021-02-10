using System.Linq;
using Prometheus;
using Ubiquitous.Metrics.Internals;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Prometheus {
    class PrometheusCount : PrometheusMetric, ICountMetric {
        readonly Counter   _count;
        readonly BaseCount _base;

        internal PrometheusCount(MetricDefinition metricDefinition, Label[]? defaultLabels) : base(defaultLabels) {
            _count = global::Prometheus.Metrics.CreateCounter(
                metricDefinition.Name,
                metricDefinition.Description,
                new CounterConfiguration {
                    LabelNames = metricDefinition.LabelNames.SafeUnion(defaultLabels.GetLabelNames()).ToArray()
                }
            );
            _base = new BaseCount();
        }

        public void Inc(int count = 1, params LabelValue[] labels) {
            CombineLabels(_count, labels).Inc(count);
            _base.Inc(count);
        }

        public long Count => _base.Count;
    }
}
