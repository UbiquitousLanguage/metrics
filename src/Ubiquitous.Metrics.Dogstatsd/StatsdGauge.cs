using StatsdClient;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Dogstatsd {
    class StatsdGauge : StatsdMetric, IGaugeMetric {
        readonly BaseGauge _base;

        internal StatsdGauge(MetricDefinition metricDefinition) : base(metricDefinition) {
            _base = new BaseGauge();
        }

        public void Set(double value, params LabelValue[] labels) {
            DogStatsd.Gauge(MetricName, value, tags: FormTags(labels));
            _base.Set(value);
        }

        public double Value => _base.Value;
    }
}
