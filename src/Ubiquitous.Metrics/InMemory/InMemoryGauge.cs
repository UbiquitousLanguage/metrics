using System.Threading;

namespace Ubiquitous.Metrics.InMemory {
    class InMemoryGauge : InMemoryMetric, IGaugeMetric {
        double _value;

        protected internal InMemoryGauge(MetricDefinition definition) : base(definition) { }

        public void Set(double value, string[]? labels = null)
            => Interlocked.Exchange(ref _value, _value + value);

        public double Value => _value;
    }
}