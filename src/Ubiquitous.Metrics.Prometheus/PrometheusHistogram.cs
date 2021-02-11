using System;
using System.Diagnostics;
using Prometheus;
using Ubiquitous.Metrics.Internals;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Prometheus {
    class PrometheusHistogram : IHistogramMetric {
        readonly Histogram     _histogram;
        readonly BaseHistogram _base;

        static readonly double[] DefaultBounds =
            {.002, .005, .01, .025, .05, .075, .1, .25, .5, .75, 1, 2.5, 5, 7.5, 10, 30, 60, 120, 180, 240, 300};

        public PrometheusHistogram(
            MetricDefinition metricDefinition, Label[]? defaultLabels, double[]? bounds = null
        ) {
            _histogram = global::Prometheus.Metrics.CreateHistogram(
                metricDefinition.Name,
                metricDefinition.Description,
                new HistogramConfiguration {
                    Buckets = bounds ?? DefaultBounds,
                    StaticLabels = defaultLabels.ToDictionary(),
                    LabelNames = metricDefinition.LabelNames
                }
            );
            _base = new BaseHistogram();
        }

        public double Sum => _base.Sum;

        public long Count => _base.Count;

        public void Observe(Stopwatch stopwatch, LabelValue[]? labels = null, int count = 1) {
            var sec = stopwatch.Elapsed.TotalSeconds;
            Observe(sec, count, labels);
            _base.Observe(sec, count);
        }

        public void Observe(DateTimeOffset when, params LabelValue[] labels) {
            var sec = (DateTimeOffset.UtcNow - when).TotalSeconds;
            Observe(sec, 1, labels);
            _base.Observe(sec);
        }

        public void Observe(TimeSpan duration, LabelValue[]? labels = null, int count = 1) {
            Observe(duration.TotalSeconds, count, labels);
            _base.Observe(duration.TotalSeconds, count);
        }

        void Observe(double value, int count, LabelValue[]? labels) {
            if (count == 0) return;
            
            if (labels == null)
                _histogram.Observe(value / count, count);
            else
                _histogram.WithLabels(labels.GetStrings()!).Observe(value / count, count);
        }
    }
}