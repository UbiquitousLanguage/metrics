namespace Ubiquitous.Metrics {
    /// <summary>
    /// Counter metric, which only can increase
    /// </summary>
    public interface ICountMetric {
        /// <summary>
        /// Increase the counter by a given number
        /// </summary>
        /// <param name="count">Optional: increase count, one by default</param>
        /// <param name="labels">Optional: metric labels, must be matching the number of configured label names</param>
        void Inc(int count = 1, params string[] labels);
        
        /// <summary>
        /// Currently observed count
        /// </summary>
        long Count { get; }
    }
}
