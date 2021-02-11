using Prometheus;
using Ubiquitous.Metrics.Internals;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Prometheus {
    class PrometheusGauge : IGaugeMetric {
        readonly Gauge     _gauge;
        readonly BaseGauge _base;

        internal PrometheusGauge(MetricDefinition metricDefinition, Label[]? defaultLabels) {
            _gauge = global::Prometheus.Metrics.CreateGauge(
                metricDefinition.Name,
                metricDefinition.Description,
                new GaugeConfiguration {
                    StaticLabels = defaultLabels.ToDictionary(),
                    LabelNames = metricDefinition.LabelNames
                }
            );
            _base = new BaseGauge();
        }

        public void Set(double value, params LabelValue[]? labels) {
            if (labels != null)
                _gauge.WithLabels(labels.GetStrings()!).Set(value);
            else
                _gauge.Set(value);
            _base.Set(value);
        }

        public double Value => _base.Value;
    }
}
