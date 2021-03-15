namespace Ubiquitous.Metrics.NoMetrics {
    class NoCount : ICountMetric {
        public void Inc(string? label = null, int count = 1) { }

        public void Inc(string[]? labels = null, int count = 1) { }
    }

    class NoCount<T> : NoCount, ICountMetric<T> {
        // ReSharper disable once UnassignedGetOnlyAutoProperty
        GetLabels<T> ICountMetric<T>.GetLabels { get; }

        public void Inc(T element, int count = 1) { }
    }
}