// ReSharper disable NotAccessedField.Local
using System;
using System.Diagnostics;
using System.Threading;

namespace Ubiquitous.Metrics {
    /// <summary>
    /// No-op metrics provider, allows you to introduce metrics without a need
    /// to configure the real provider yet.
    /// </summary>
    public class NoMetricsProvider : IMetricsProvider {
        public ICountMetric CreateCount(MetricDefinition definition) => new NoCount(definition);

        public IHistogramMetric CreateHistogram(MetricDefinition definition) => new NoHistogram(definition);

        public IGaugeMetric CreateGauge(MetricDefinition definition) => new NoGauge(definition);

        class NoMetric {
            readonly MetricDefinition _definition;

            protected NoMetric(MetricDefinition definition) => _definition = definition;
        }

        class NoCount : NoMetric, ICountMetric {
            long _count;

            protected internal NoCount(MetricDefinition definition) : base(definition) { }

            public void Inc(int count = 1, params string[]? labels) => Interlocked.Add(ref _count, count);
            public long Count => _count;
        }

        class NoHistogram : NoMetric, IHistogramMetric {
            protected internal NoHistogram(MetricDefinition definition) : base(definition) { }

            public double Sum { get; private set; }

            public long Count { get; private set; }

            public void Observe(Stopwatch stopwatch, string[]? labels = null, int count = 1)
                => Observe(stopwatch.ElapsedMilliseconds * 1000, count);

            public void Observe(DateTimeOffset when, string[]? labels = null, int count = 1)
                => Observe((DateTimeOffset.Now - when).Seconds, count);

            public void Observe(TimeSpan duration, string[]? labels = null, int count = 1)
                => Observe(duration.TotalSeconds, count);

            void Observe(double seconds, long count) {
                Sum += seconds;
                Count += count;
            }
        }

        class NoGauge : NoMetric, IGaugeMetric {
            double _value;

            protected internal NoGauge(MetricDefinition definition) : base(definition) { }

            public void Set(double value, params string[] labels)
                => Interlocked.Exchange(ref _value, _value + value);

            public double Value => _value;
        }
    }
}