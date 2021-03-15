using System.Threading;

namespace Ubiquitous.Metrics.InMemory {
    class InMemoryGauge : IGaugeMetric {
        double _value;

        public void Set(double value, string[]? labels = null) => Interlocked.Exchange(ref _value, _value + value);

        public double Value => _value;
    }
}