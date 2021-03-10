using System;
using Prometheus;
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
        
        public void Inc()
        {
            Inc(1, Array.Empty<string>());
        }

        public void Inc(string label)
        {
            Inc(1, new[] {label});
        }

        public void Inc(int count, string label)
        {
            Inc(count,  new[] {label});
        }

        public void Inc(int count = 1, params string[]? labels) {
            if (labels == null)
                _count.Inc(count);
            else
                _count.WithLabels(labels).Inc(count);
            _base.Inc(count);
        }

        public long Count => _base.Count;
    }
}
