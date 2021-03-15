using System;
using System.Diagnostics;
using Prometheus;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Prometheus {
    class PrometheusHistogram : IHistogramMetric {
        readonly Histogram _histogram;

        static readonly double[] DefaultBounds =
            {.002, .005, .01, .025, .05, .075, .1, .25, .5, .75, 1, 2.5, 5, 7.5, 10, 30, 60, 120, 180, 240, 300};

        public PrometheusHistogram(
            MetricDefinition definition, Label[]? defaultLabels, double[]? bounds = null
        ) {
            _histogram = global::Prometheus.Metrics.CreateHistogram(
                definition.Name,
                definition.Description,
                new HistogramConfiguration
                {
                    Buckets      = bounds ?? DefaultBounds,
                    StaticLabels = defaultLabels.ToDictionary(),
                    LabelNames   = definition.LabelNames
                }
            );
        }

        public void Observe(Stopwatch stopwatch, string[]? labels = null, int count = 1) {
            var sec = stopwatch.Elapsed.TotalSeconds;
            Observe(sec, count, labels);
        }

        public void Observe(DateTimeOffset when, string[]? labels = null, int count = 1) {
            var sec = (DateTimeOffset.UtcNow - when).TotalSeconds;
            Observe(sec, count, labels);
        }

        public void Observe(TimeSpan duration, string[]? labels = null, int count = 1)
            => Observe(duration.TotalSeconds, count, labels);

        void Observe(double value, int count, string[]? labels) {
            if (count == 0) return;

            if (labels == null)
                _histogram.Observe(value, count);
            else
                _histogram.WithLabels(labels).Observe(value, count);
        }
    }
}