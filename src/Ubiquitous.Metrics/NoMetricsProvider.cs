// ReSharper disable NotAccessedField.Local

using System;
using System.Diagnostics;
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
            int _count;

            protected internal NoCount(MetricDefinition definition) : base(definition) { }

            public void Inc(int count = 1, params LabelValue[]? labels) => _count += count;
        }

        class NoHistogram : NoMetric, IHistogramMetric {
            protected internal NoHistogram(MetricDefinition definition) : base(definition) { }

            public void Observe(Stopwatch stopwatch, LabelValue[]? labels = null, int count = 1) => Observe(stopwatch.ElapsedMilliseconds * 1000, labels);

            public void Observe(DateTimeOffset when, params LabelValue[] labels) => Observe((DateTimeOffset.Now - when).Seconds, labels);

            void Observe(double seconds, LabelValue[]? labels) { }
        }

        class NoGauge : NoMetric, IGaugeMetric {
            protected internal NoGauge(MetricDefinition definition) : base(definition) { }

            public void Set(double value, params LabelValue[] labels) { }
        }
    }
}
