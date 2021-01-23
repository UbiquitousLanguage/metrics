// ReSharper disable NotAccessedField.Local

using System;
using System.Diagnostics;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics {
    public class NoMetrics : IMetrics {
        public ICountMetric CreateCount(MetricDefinition definition) => new NoCount(definition);

        public IHistogramMetric CreateHistogram(MetricDefinition definition) => new NoHistogram(definition);

        public IGaugeMetric CreateGauge(MetricDefinition definition) => new NoGauge(definition);

        class NoMetric {
            readonly MetricDefinition _definition;

            protected NoMetric(MetricDefinition definition) => _definition = definition;
        }

        class NoCount : NoMetric, ICountMetric {
            int _count;

            protected internal NoCount(MetricDefinition definition) : base(definition) { }

            public void Inc(int count = 1, params Label[]? labels) => _count += count;
        }

        class NoHistogram : NoMetric, IHistogramMetric {
            protected internal NoHistogram(MetricDefinition definition) : base(definition) { }

            public void Observe(Stopwatch stopwatch, Label[]? labels = null, int count = 1) => Observe(stopwatch.ElapsedMilliseconds * 1000, labels);

            public void Observe(DateTimeOffset when, params Label[] labels) => Observe((DateTimeOffset.Now - when).Seconds, labels);

            void Observe(double seconds, Label[]? labels) { }
        }

        class NoGauge : NoMetric, IGaugeMetric {
            protected internal NoGauge(MetricDefinition definition) : base(definition) { }

            public void Set(double value, params Label[] labels) { }
        }
    }
}
