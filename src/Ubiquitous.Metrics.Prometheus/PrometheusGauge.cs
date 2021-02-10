using System.Linq;
using Prometheus;
using Ubiquitous.Metrics.Internals;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Prometheus {
    class PrometheusGauge : PrometheusMetric, IGaugeMetric {
        readonly Gauge     _gauge;
        readonly BaseGauge _base;

        internal PrometheusGauge(MetricDefinition metricDefinition, Label[]? defaultLabels) : base(defaultLabels) {
            _gauge = global::Prometheus.Metrics.CreateGauge(
                metricDefinition.Name,
                metricDefinition.Description,
                new GaugeConfiguration {
                    LabelNames = metricDefinition.LabelNames.SafeUnion(defaultLabels.GetLabelNames()).ToArray()
                }
            );
            _base = new BaseGauge();
        }

        public void Set(double value, params LabelValue[]? labels) {
            CombineLabels(_gauge, labels).Set(value);
            _base.Set(value);
        }

        public double Value => _base.Value;
    }
}
