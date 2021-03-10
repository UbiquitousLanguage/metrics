using Prometheus;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Prometheus {
    class PrometheusCount : ICountMetric {
        readonly Counter _count;

        internal PrometheusCount(MetricDefinition metricDefinition, Label[]? defaultLabels) {
            _count = global::Prometheus.Metrics.CreateCounter(
                metricDefinition.Name,
                metricDefinition.Description,
                new CounterConfiguration
                {
                    StaticLabels = defaultLabels.ToDictionary(),
                    LabelNames   = metricDefinition.LabelNames
                }
            );
        }

        public void Inc(string? label = null, int count = 1) {
            if (label == null)
                _count.Inc(count);
            else
                _count.WithLabels(label).Inc(count);
        }

        public void Inc(string[]? labels, int count = 1) {
            if (labels == null)
                _count.Inc(count);
            else
                _count.WithLabels(labels).Inc(count);
        }
    }
}