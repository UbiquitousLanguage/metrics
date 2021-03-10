using System;
using System.Diagnostics;
using System.Threading;
using static System.BitConverter;

namespace Ubiquitous.Metrics.InMemory {
    class InMemoryHistogram : InMemoryMetric, IHistogramMetric {
        protected internal InMemoryHistogram(MetricDefinition definition) : base(definition) { }

        public void Observe(Stopwatch stopwatch, string[]? labels = null, int count = 1)
            => Observe(stopwatch.ElapsedMilliseconds * 1000, count);

        public void Observe(DateTimeOffset when, string[]? labels = null, int count = 1)
            => Observe((DateTimeOffset.Now - when).Seconds, count);

        public void Observe(TimeSpan duration, string[]? labels = null, int count = 1)
            => Observe(duration.TotalSeconds, count);

        void Observe(double seconds, int count) {
            Add(seconds);
            Interlocked.Add(ref _count, count);
        }

        public double Sum => Int64BitsToDouble(Interlocked.Read(ref _sum));
        public long Count => Interlocked.Read(ref _count);

        long _sum;
        long _count;

        void Add(double increment) {
            long   comp;
            double num;

            do {
                comp = _sum;
                num = Int64BitsToDouble(comp) + increment;
            } while (comp != Interlocked.CompareExchange(ref _sum, DoubleToInt64Bits(num), comp));
        }
    }
}