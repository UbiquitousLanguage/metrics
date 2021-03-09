using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Ubiquitous.Metrics.MicrosoftLog {
    class LogHistogram : LoggingMetric, IHistogramMetric {
        internal LogHistogram(MetricDefinition metricDefinition, ILogger log) : base(metricDefinition, log) { }

        public double Sum { get; private set; }
        public long Count { get; private set; }

        public void Observe(Stopwatch stopwatch, string[]? labels = null, int count = 1)
            => Observe(stopwatch.Elapsed.TotalSeconds, count, labels);

        public void Observe(DateTimeOffset when, string[]? labels = null, int count = 1) {
            var sec = (DateTimeOffset.UtcNow - when).TotalSeconds;
            Observe(sec, 1, labels);
        }

        public void Observe(TimeSpan duration, string[]? labels = null, int count = 1)
            => Observe(duration.TotalSeconds, count, labels);

        void Observe(double sum, long count, string[]? labels) {
            Sum   += sum;
            Count += count;

            Log.LogInformation(
                "Histogram {Name}: sum {Sum}, count {Count}, labels {Labels}",
                Definition.Name,
                Sum,
                Count,
                LabelsString(labels)
            );
        }
    }
}