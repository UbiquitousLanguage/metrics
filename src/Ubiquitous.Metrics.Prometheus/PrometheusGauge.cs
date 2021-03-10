using Prometheus;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Prometheus {
    class PrometheusGauge : IGaugeMetric {
        readonly Gauge _gauge;

        internal PrometheusGauge(MetricDefinition metricDefinition, Label[]? defaultLabels) {
            _gauge = global::Prometheus.Metrics.CreateGauge(
                metricDefinition.Name,
                metricDefinition.Description,
                new GaugeConfiguration
                {
                    StaticLabels = defaultLabels.ToDictionary(),
                    LabelNames   = metricDefinition.LabelNames
                }
            );
        }

        public void Set(double value, string? label = null) {
            if (label != null)
                _gauge.WithLabels(label).Set(value);
            else
                _gauge.Set(value);
        }

        public void Set(double value, params string[]? labels) {
            if (labels != null)
                _gauge.WithLabels(labels).Set(value);
            else
                _gauge.Set(value);
        }
    }
}