using System;
using System.Diagnostics;

namespace Ubiquitous.Metrics {
    /// <summary>
    /// Histogram metric, which allows you to measure duration of an operation
    /// </summary>
    public interface IHistogramMetric {
        /// <summary>
        /// Returns the current measured sum (usually the time)
        /// </summary>
        double Sum { get; }
        
        /// <summary>
        /// Returns the current measured count (usually the number of operations)
        /// </summary>
        long Count { get; }
        
        /// <summary>
        /// Observe an operation using the finished stopwatch
        /// </summary>
        /// <param name="stopwatch">Stopwatch, which was started before the operation begun</param>
        /// <param name="labels">Optional: metric labels, must be matching the number of configured label names</param>
        /// <param name="count">Optional: custom count, one by default</param>
        void Observe(Stopwatch stopwatch, string[]? labels = null, int count = 1);

        /// <summary>
        /// Observe an operation using the operation start timestamp. Current time will be used as the operation finish time.
        /// </summary>
        /// <param name="when">The timestamp when the operation started</param>
        /// <param name="labels">Optional: metric labels, must be matching the number of configured label names</param>
        /// <param name="count">Optional: custom count, one by default</param>
        void Observe(DateTimeOffset when, string[]? labels = null, int count = 1);
        
        /// <summary>
        /// Observe an operation using the time already measured
        /// </summary>
        /// <param name="duration">A known duration of the operation</param>
        /// <param name="labels">Optional: metric labels, must be matching the number of configured label names</param>
        /// <param name="count">Optional: custom count, one by default</param>
        void Observe(TimeSpan duration, string[]? labels = null, int count = 1);
    }
}
