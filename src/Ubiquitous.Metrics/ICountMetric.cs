namespace Ubiquitous.Metrics {
    /// <summary>
    /// Counter metric, which only can increase
    /// </summary>
    public interface ICountMetric {
        
        //Zero Alloc overloads
        void Inc();
        void Inc(string label);
        void Inc(int count, string label);
        
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
