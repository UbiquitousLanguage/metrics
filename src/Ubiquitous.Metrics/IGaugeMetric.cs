using Ubiquitous.Metrics.Internals;

namespace Ubiquitous.Metrics {
    /// <summary>
    /// Gauge metric, which can go up and down
    /// </summary>
    public interface IGaugeMetric {
        /// <summary>
        /// Set the gauge to a new value
        /// </summary>
        /// <param name="value">The new gauge value</param>
        /// <param name="label">Metric label, must have one label name configured for the metric</param>
        public void Set(double value, string? label = null)
            => Set(value, label.ArrayOrNull());
        
        /// <summary>
        /// Set the gauge to a new value
        /// </summary>
        /// <param name="value">The new gauge value</param>
        /// <param name="labels">Metric labels, must be matching the number of configured label names</param>
        void Set(double value, string[]? labels = null);
    }
}
