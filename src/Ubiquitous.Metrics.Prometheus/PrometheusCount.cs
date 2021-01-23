using Prometheus;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Prometheus {
    class PrometheusCount : PrometheusMetric, ICountMetric {
        readonly Counter _count;

        internal PrometheusCount(MetricDefinition metricDefinition, DefaultLabel[] defaultLabels) : base(defaultLabels)
            => _count = global::Prometheus.Metrics.CreateCounter(
                metricDefinition.Name,
                metricDefinition.Description,
                new CounterConfiguration {
                    LabelNames = metricDefinition.LabelNames
                }
            );

        public void Inc(int count = 1, params Label[] labels) => CombineLabels(_count, labels).Inc(count);
    }
}
