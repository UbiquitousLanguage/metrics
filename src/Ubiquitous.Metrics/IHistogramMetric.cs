using System;
using System.Diagnostics;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics {
    public interface IHistogramMetric {
        void Observe(Stopwatch stopwatch, LabelValue[]? labels = null, int count = 1);

        void Observe(DateTimeOffset when, params LabelValue[] labels);
    }
}
