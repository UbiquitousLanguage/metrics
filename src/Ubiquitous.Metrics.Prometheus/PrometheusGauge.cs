using Prometheus;

namespace Ubiquitous.Metrics.Prometheus {
    class PrometheusGauge : IGaugeMetric {
        readonly Gauge _gauge;

        internal PrometheusGauge(MetricDefinition metricDefinition)
            => _gauge = global::Prometheus.Metrics.CreateGauge(
                metricDefinition.Name,
                metricDefinition.Description,
                new GaugeConfiguration {
                    LabelNames = metricDefinition.LabelNames
                }
            );

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
