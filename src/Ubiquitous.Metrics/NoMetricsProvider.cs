// ReSharper disable NotAccessedField.Local

using System;
using System.Diagnostics;
using System.Threading;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics {
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

            public void Inc(int count = 1, params LabelValue[]? labels) => Interlocked.Add(ref _count, count);
            public long Count => _count;
        }

        class NoHistogram : NoMetric, IHistogramMetric {
            protected internal NoHistogram(MetricDefinition definition) : base(definition) { }

            public double Sum { get; private set; }

            public long Count { get; private set; }

            public void Observe(Stopwatch stopwatch, LabelValue[]? labels = null, int count = 1)
                => Observe(stopwatch.ElapsedMilliseconds * 1000, count);

            public void Observe(DateTimeOffset when, params LabelValue[] labels)
                => Observe((DateTimeOffset.Now - when).Seconds, 1);

            public void Observe(TimeSpan duration, LabelValue[]? labels = null, int count = 1)
                => Observe(duration.TotalSeconds, count);

            void Observe(double seconds, long count) {
                Sum += seconds;
                Count += count;
            }
        }

        class NoGauge : NoMetric, IGaugeMetric {
            double _value;

            protected internal NoGauge(MetricDefinition definition) : base(definition) { }

            public void Set(double value, params LabelValue[] labels)
                => Interlocked.Exchange(ref _value, _value + value);

            public double Value => _value;
        }
    }
}