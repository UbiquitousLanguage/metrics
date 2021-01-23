using System;
using System.Diagnostics;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics {
    public interface IHistogramMetric {
        void Observe(Stopwatch stopwatch, Label[]? labels = null, int count = 1);

        void Observe(DateTimeOffset when, params Label[] labels);
    }
}
