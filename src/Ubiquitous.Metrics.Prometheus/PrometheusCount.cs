using Prometheus;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Prometheus {
    class PrometheusCount : PrometheusMetric, ICountMetric {
        readonly Counter _count;

        internal PrometheusCount(MetricDefinition metricDefinition, Label[]? defaultLabels = null) : base(defaultLabels)
            => _count = global::Prometheus.Metrics.CreateCounter(
                metricDefinition.Name,
                metricDefinition.Description,
                new CounterConfiguration {
                    LabelNames = metricDefinition.LabelNames
                }
            );

        public void Inc(int count = 1, params LabelValue[] labels) => CombineLabels(_count, labels).Inc(count);
    }
}
