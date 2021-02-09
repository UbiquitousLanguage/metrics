using System;
using System.Diagnostics;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics {
    public interface IHistogramMetric {
        double Sum { get; }
        
        long Count { get; }
        
        void Observe(Stopwatch stopwatch, LabelValue[]? labels = null, int count = 1);

        void Observe(DateTimeOffset when, params LabelValue[] labels);
    }
}
