using System.Threading;

namespace Ubiquitous.Metrics.InMemory {
    public class InMemoryCount : InMemoryMetric, ICountMetric {
        long _count;

        protected internal InMemoryCount(MetricDefinition definition) : base(definition) { }

        public void Inc(string[]? labels = null, int count = 1) => Inc(count);
        
        void Inc(int count) => Interlocked.Add(ref _count, count);

        public long Count => _count;
    }
}