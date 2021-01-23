using Prometheus;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Prometheus {
    class PrometheusGauge : PrometheusMetric, IGaugeMetric {
        readonly Gauge _gauge;

        internal PrometheusGauge(MetricDefinition metricDefinition, DefaultLabel[] defaultLabels) : base(defaultLabels)
            => _gauge = global::Prometheus.Metrics.CreateGauge(
                metricDefinition.Name,
                metricDefinition.Description,
                new GaugeConfiguration {LabelNames = metricDefinition.LabelNames}
            );

        public void Set(double value, params Label[]? labels) => CombineLabels(_gauge, labels).Set(value);
    }
}
