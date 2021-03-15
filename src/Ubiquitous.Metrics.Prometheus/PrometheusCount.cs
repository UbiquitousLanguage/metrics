using Prometheus;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Prometheus {
    class PrometheusCount : ICountMetric {
        readonly Counter _count;

        internal PrometheusCount(MetricDefinition definition, Label[]? defaultLabels) {
            _count = global::Prometheus.Metrics.CreateCounter(
                definition.Name,
                definition.Description,
                new CounterConfiguration
                {
                    StaticLabels = defaultLabels.ToDictionary(),
                    LabelNames   = definition.LabelNames
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

    class PrometheusCount<T> : PrometheusCount, ICountMetric<T> {
        internal PrometheusCount(MetricDefinition<T> definition, Label[]? defaultLabels)
            : base(definition, defaultLabels)
            => GetLabels = definition.GetLabels;

        public GetLabels<T> GetLabels { get; }
    }
}