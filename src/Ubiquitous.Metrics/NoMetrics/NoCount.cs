namespace Ubiquitous.Metrics.NoMetrics {
    class NoCount : ICountMetric {
        internal NoCount(MetricDefinition _) { }

        public void Inc(string? label = null, int count = 1) { }

        public void Inc(string[]? labels = null, int count = 1) { }
    }
}