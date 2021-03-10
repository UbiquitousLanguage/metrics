using JetBrains.Annotations;
using Ubiquitous.Metrics.Internals;

namespace Ubiquitous.Metrics {
    /// <summary>
    /// Counter metric, which only can increase
    /// </summary>
    [PublicAPI]
    public interface ICountMetric {
        /// <summary>
        /// Increase the counter by one with a single label
        /// </summary>
        /// <param name="count">Custom count, if you want to increase for more than one</param>
        /// <param name="label">Metric label</param>
        public void Inc(string? label = null, int count = 1)
            => Inc(label.ArrayOrNull(), count);

        /// <summary>
        /// Increase the counter by a given number
        /// </summary>
        /// <param name="count">Increase count, one by default</param>
        /// <param name="labels">Metric labels, must be matching the number of configured label names</param>
        void Inc(string[]? labels = null, int count = 1);
    }
}