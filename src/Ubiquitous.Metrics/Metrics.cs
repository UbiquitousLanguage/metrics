using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Ubiquitous.Metrics.Combined;
using Ubiquitous.Metrics.Internals;
using Ubiquitous.Metrics.NoMetrics;

namespace Ubiquitous.Metrics {
    /// <summary>
    /// The entry point for all the metrics
    /// </summary>
    [PublicAPI]
    public class Metrics {
        Func<MetricDefinition, ICountMetric>?     _createCount;
        Func<MetricDefinition, IGaugeMetric>?     _createGauge;
        Func<MetricDefinition, IHistogramMetric>? _createHistogram;

        static Metrics() => Instance = new Metrics();

        /// <summary>
        /// Get the Metrics instance. Normally, you'd need only one Metrics instance per application.
        /// </summary>
        public static Metrics Instance { get; }

        /// <summary>
        /// Configure the Metrics instance using one or more providers
        /// </summary>
        /// <param name="configurators">Zero or more metric providers. If you don't supply any provider,
        /// the NoOp provider will be used.</param>
        /// <returns></returns>
        public static Metrics CreateUsing(params IMetricsProvider[] configurators) {
            var cfg = configurators.Length > 0 ? configurators : new[] {new NoMetricsProvider()};

            var combine = cfg.Length > 1;

            Instance._createCount = combine
                ? def => new CombinedCount(cfg.Select(x => x.CreateCount(def)).ToArray())
                : def => cfg[0].CreateCount(def);

            Instance._createGauge = combine
                ? def => new CombinedGauge(cfg.Select(x => x.CreateGauge(def)).ToArray())
                : def => cfg[0].CreateGauge(def);

            Instance._createHistogram = combine
                ? def => new CombinedHistogram(cfg.Select(x => x.CreateHistogram(def)).ToArray())
                : def => cfg[0].CreateHistogram(def);

            return Instance;
        }

        /// <summary>
        /// Create a counter metric
        /// </summary>
        /// <param name="definition">Metric definition (name, description and labels)</param>
        /// <returns></returns>
        public ICountMetric CreateCount(MetricDefinition definition)
            => Ensure.NotDefault(_createCount, "Metrics provider hasn't been configured")(definition);

        /// <summary>
        /// Create a histogram metric
        /// </summary>
        /// <param name="definition">Metric definition (name, description and labels)</param>
        /// <returns></returns>
        public IHistogramMetric CreateHistogram(MetricDefinition definition)
            => Ensure.NotDefault(_createHistogram, "Metrics provider hasn't been configured")(definition);

        /// <summary>
        /// Create a gauge metric
        /// </summary>
        /// <param name="definition">Metric definition (name, description and labels)</param>
        /// <returns></returns>
        public IGaugeMetric CreateGauge(MetricDefinition definition)
            => Ensure.NotDefault(_createGauge, "Metrics provider hasn't been configured")(definition);

        [Obsolete("Use MeasureTask")]
        public static Task Measure(
            Func<Task>       action,
            IHistogramMetric metric,
            ICountMetric?    errorCount = null,
            string[]?        labels     = null,
            int              count      = 1
        ) => MeasureTask(action, metric, errorCount, labels, count);

        [Obsolete("Use MeasureTask")]
        public static Task<T> Measure<T>(
            Func<Task<T>>    action,
            IHistogramMetric metric,
            ICountMetric?    errorCount = null,
            string[]?        labels     = null,
            int              count      = 1
        ) => MeasureTask(action, metric, errorCount, labels, count);

        [Obsolete("Use MeasureTask")]
        public static Task<T?> Measure<T>(
            Func<Task<T>>    action,
            IHistogramMetric metric,
            Func<T, int>     getCount,
            ICountMetric?    errorCount = null,
            string[]?        labels     = null
        ) where T : class => MeasureTask(action, metric, getCount, errorCount, labels);

        /// <summary>
        /// Helpful function to measure the execution time of a given asynchronous function
        /// </summary>
        /// <param name="action">Actual operation to measure</param>
        /// <param name="metric">Histogram metrics, which will observe the measurement</param>
        /// <param name="errorCount">Optional: counter metric to count errors</param>
        /// <param name="labels">Labels for the histogram and errors counter metrics</param>
        /// <param name="count">Optional: count, one by default</param>
        /// <returns></returns>
        public static async Task MeasureTask(
            Func<Task>       action,
            IHistogramMetric metric,
            ICountMetric?    errorCount = null,
            string[]?        labels     = null,
            int              count      = 1
        ) {
            var stopwatch = Stopwatch.StartNew();

            try {
                await action().ConfigureAwait(false);
            }
            catch (Exception) {
                errorCount?.Inc(labels);

                throw;
            }
            finally {
                stopwatch.Stop();
                metric.Observe(stopwatch, labels, count);
            }
        }

        /// <summary>
        /// Helpful function to measure the execution time of a given synchronous function
        /// </summary>
        /// <param name="action">Actual operation to measure</param>
        /// <param name="metric">Histogram metrics, which will observe the measurement</param>
        /// <param name="errorCount">Optional: counter metric to count errors</param>
        /// <param name="labels">Labels for the histogram and errors counter metrics</param>
        /// <param name="count">Optional: count, one by default</param>
        /// <returns></returns>
        public static void Measure(
            Action           action,
            IHistogramMetric metric,
            ICountMetric?    errorCount = null,
            string[]?        labels     = null,
            int              count      = 1
        ) {
            var stopwatch = Stopwatch.StartNew();

            try {
                action();
            }
            catch (Exception) {
                errorCount?.Inc(labels);

                throw;
            }
            finally {
                stopwatch.Stop();
                metric.Observe(stopwatch, labels, count);
            }
        }

        /// <summary>
        /// Helpful function to measure the execution time of a given asynchronous function,
        /// and returns the observed function result.
        /// </summary>
        /// <param name="action">Actual operation to measure</param>
        /// <param name="metric">Histogram metrics, which will observe the measurement</param>
        /// <param name="errorCount">Optional: counter metric to count errors</param>
        /// <param name="labels">Labels for the histogram and errors counter metrics</param>
        /// <param name="count">Optional: count, one by default</param>
        /// <returns></returns>
        public static async Task<T> MeasureTask<T>(
            Func<Task<T>>    action,
            IHistogramMetric metric,
            ICountMetric?    errorCount = null,
            string[]?        labels     = null,
            int              count      = 1
        ) {
            var stopwatch = Stopwatch.StartNew();

            T result;

            try {
                var task = action();
                result = task.IsCompleted ? task.Result : await task.ConfigureAwait(false);
            }
            catch (Exception) {
                errorCount?.Inc(labels);

                throw;
            }
            finally {
                stopwatch.Stop();
                metric.Observe(stopwatch, labels, count);
            }

            return result;
        }

        /// <summary>
        /// Helpful function to measure the execution time of a given asynchronous function,
        /// and returns the observed function result.
        /// </summary>
        /// <param name="action">Actual operation to measure</param>
        /// <param name="metric">Histogram metrics, which will observe the measurement</param>
        /// <param name="errorCount">Optional: counter metric to count errors</param>
        /// <param name="labels">Labels for the histogram and errors counter metrics</param>
        /// <param name="count">Optional: count, one by default</param>
        /// <returns></returns>
        public static async Task<T> MeasureValueTask<T>(
            Func<ValueTask<T>> action,
            IHistogramMetric   metric,
            ICountMetric?      errorCount = null,
            string[]?          labels     = null,
            int                count      = 1
        ) {
            var stopwatch = Stopwatch.StartNew();

            T result;

            try {
                var task = action();
                result = task.IsCompleted ? task.Result : await task.ConfigureAwait(false);
            }
            catch (Exception) {
                errorCount?.Inc(labels);

                throw;
            }
            finally {
                stopwatch.Stop();
                metric.Observe(stopwatch, labels, count);
            }

            return result;
        }

        /// <summary>
        /// Helpful function to measure the execution time of a given asynchronous function,
        /// and returns the observed function result.
        /// </summary>
        /// <param name="action">Actual operation to measure</param>
        /// <param name="metric">Histogram metrics, which will observe the measurement</param>
        /// <param name="getCount">A function, which is able to get the custom count from the function return</param>
        /// <param name="errorCount">Optional: counter metric to count errors</param>
        /// <param name="labels">Labels for the histogram and errors counter metrics</param>
        /// <typeparam name="T">The observed function result</typeparam>
        /// <returns></returns>
        public static async Task<T?> MeasureTask<T>(
            Func<Task<T>>    action,
            IHistogramMetric metric,
            Func<T, int>     getCount,
            ICountMetric?    errorCount = null,
            string[]?        labels     = null
        ) where T : class {
            var stopwatch = Stopwatch.StartNew();

            T? result = null;

            try {
                var task = action();
                result = task.IsCompleted ? task.Result : await task.ConfigureAwait(false);
            }
            catch (Exception) {
                errorCount?.Inc(labels);

                throw;
            }
            finally {
                stopwatch.Stop();
                metric.Observe(stopwatch, labels, result != null ? getCount(result) : 0);
            }

            return result;
        }
    }
}