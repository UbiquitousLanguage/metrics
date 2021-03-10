namespace Ubiquitous.Metrics.NoMetrics {
    class NoGauge : IGaugeMetric {
        internal NoGauge(MetricDefinition _) { }

        public void Set(double value, string? label = null) { }

        public void Set(double value, string[]? labels = null) { }
    }
}