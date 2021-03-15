namespace Ubiquitous.Metrics.NoMetrics {
    class NoGauge : IGaugeMetric {
        public void Set(double value, string? label = null) { }

        public void Set(double value, string[]? labels = null) { }
    }
}