using System.Threading;

namespace Ubiquitous.Metrics.InMemory {
    class InMemoryCount : ICountMetric {
        long _count;

        public void Inc(string[]? labels = null, int count = 1) => Inc(count);
        
        void Inc(int count) => Interlocked.Add(ref _count, count);

        public long Count => _count;
    }

    class InMemoryCount<T> : InMemoryCount, ICountMetric<T> {
        GetLabels<T> ICountMetric<T>.GetLabels { get; }
    }
}