using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics {
    /// <summary>
    /// Gauge metric, which can go up and down
    /// </summary>
    public interface IGaugeMetric {
        /// <summary>
        /// Set the gauge to a new value
        /// </summary>
        /// <param name="value">The new gauge value</param>
        /// <param name="labels">Optional: metric labels, must be matching the number of configured label names</param>
        void Set(double value, params LabelValue[] labels);
        
        /// <summary>
        /// The current gauge value
        /// </summary>
        double Value { get; }
    }
}
