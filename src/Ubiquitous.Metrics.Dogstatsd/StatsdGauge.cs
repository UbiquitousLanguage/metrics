using System.Threading;
using StatsdClient;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Dogstatsd {
    class StatsdGauge : StatsdMetric, IGaugeMetric {
        internal StatsdGauge(MetricDefinition metricDefinition) : base(metricDefinition) { }

        public void Set(double value, params LabelValue[] labels) {
            DogStatsd.Gauge(MetricName, value, tags: FormTags(labels));
            Interlocked.Exchange(ref _value, _value + value);
        }

        public double Value => _value;

        double _value;
    }
}
