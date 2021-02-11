using Prometheus;
using Ubiquitous.Metrics.Internals;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Prometheus {
    class PrometheusCount : ICountMetric {
        readonly Counter   _count;
        readonly BaseCount _base;

        internal PrometheusCount(MetricDefinition metricDefinition, Label[]? defaultLabels) {
            _count = global::Prometheus.Metrics.CreateCounter(
                metricDefinition.Name,
                metricDefinition.Description,
                new CounterConfiguration {
                    StaticLabels = defaultLabels.ToDictionary(),
                    LabelNames = metricDefinition.LabelNames
                }
            );
            _base = new BaseCount();
        }

        public void Inc(int count = 1, params LabelValue[]? labels) {
            if (labels == null)
                _count.Inc(count);
            else
                _count.WithLabels(labels.GetStrings()!).Inc(count);
            _base.Inc(count);
        }

        public long Count => _base.Count;
    }
}
