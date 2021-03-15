using System;
using System.Diagnostics;

namespace Ubiquitous.Metrics.NoMetrics {
    class NoHistogram : IHistogramMetric {
        public void Observe(Stopwatch stopwatch, string[]? labels = null, int count = 1) { }
        
        public void Observe(Stopwatch stopwatch, string? label = null, int count = 1) { }

        public void Observe(DateTimeOffset when, string[]? labels = null, int count = 1) { }
        
        public void Observe(DateTimeOffset when, string? label = null, int count = 1) { }

        public void Observe(TimeSpan duration, string[]? labels = null, int count = 1) { }
        
        public void Observe(TimeSpan duration, string? label = null, int count = 1) { }
    }
}