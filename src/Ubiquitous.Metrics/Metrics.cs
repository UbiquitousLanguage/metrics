using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Ubiquitous.Metrics.Combined;
using Ubiquitous.Metrics.Internals;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics {
    [PublicAPI]
    public class Metrics {
        Func<MetricDefinition, ICountMetric> _createCount = null!;

        Func<MetricDefinition, IGaugeMetric> _createGauge = null!;

        Func<MetricDefinition, IHistogramMetric> _createHistogram = null!;

        static Metrics() => Instance = new Metrics();

        /// <summary>
        /// Get the Metrics instance. Normally, you'd need only one Metrics instance per application.
        /// </summary>
        public static Metrics Instance { get; }

        public static Metrics CreateUsing(params IMetricsProvider[] configurators) {
            var cfg = configurators.Length > 0 ? configurators : new[] {new NoMetricsProvider()};

            var combine = cfg.Length > 1;

            Instance._createCount = combine
                ? def => new CombinedCount(cfg.Select(x => x.CreateCount(def)).ToList())
                : def => cfg[0].CreateCount(def);

            Instance._createGauge = combine
                ? def => new CombinedGauge(cfg.Select(x => x.CreateGauge(def)).ToList())
                : def => cfg[0].CreateGauge(def);

            Instance._createHistogram = combine
                ? def => new CombinedHistogram(cfg.Select(x => x.CreateHistogram(def)).ToList())
                : def => cfg[0].CreateHistogram(def);

            return Instance;
        }

        public ICountMetric CreateCount(MetricDefinition definition) => _createCount(definition);

        public IHistogramMetric CreateHistogram(MetricDefinition definition) => _createHistogram(definition);

        public IGaugeMetric CreateGauge(MetricDefinition definition) => _createGauge(definition);

        public static async Task Measure(
            Func<Task>       action,
            IHistogramMetric metric,
            ICountMetric?    errorCount = null,
            LabelValue[]?    labels     = null,
            int              count      = 1
        ) {
            var stopwatch = Stopwatch.StartNew();

            try {
                await action();
            }
            catch (Exception) {
                errorCount?.Inc(labels: labels.ValueOrEmpty());

                throw;
            }
            finally {
                stopwatch.Stop();
                metric.Observe(stopwatch, labels, count);
            }
        }

        public static void Measure(
            Action           action,
            IHistogramMetric metric,
            ICountMetric?    errorCount = null,
            LabelValue[]?    labels     = null,
            int              count      = 1
        ) {
            var stopwatch = Stopwatch.StartNew();

            try {
                action();
            }
            catch (Exception) {
                errorCount?.Inc(labels: labels.ValueOrEmpty());

                throw;
            }
            finally {
                stopwatch.Stop();
                metric.Observe(stopwatch, labels, count);
            }
        }

        public static async Task<T> Measure<T>(
            Func<Task<T>>    action,
            IHistogramMetric metric,
            ICountMetric?    errorCount = null,
            LabelValue[]?    labels     = null,
            int              count      = 1
        ) {
            var stopwatch = Stopwatch.StartNew();

            T result;

            try {
                var task = action();
                result = task.IsCompleted ? task.Result : await action();
            }
            catch (Exception) {
                errorCount?.Inc(labels: labels.ValueOrEmpty());

                throw;
            }
            finally {
                stopwatch.Stop();
                metric.Observe(stopwatch, labels, count);
            }

            return result;
        }

        public static async Task<T> Measure<T>(
            Func<ValueTask<T>> action,
            IHistogramMetric   metric,
            ICountMetric?      errorCount = null,
            LabelValue[]?      labels     = null,
            int                count      = 1
        ) {
            var stopwatch = Stopwatch.StartNew();

            T result;

            try {
                var task = action();
                result = task.IsCompleted ? task.Result : await action();
            }
            catch (Exception) {
                errorCount?.Inc(labels: labels.ValueOrEmpty());

                throw;
            }
            finally {
                stopwatch.Stop();
                metric.Observe(stopwatch, labels, count);
            }

            return result;
        }

        public static async Task<T?> Measure<T>(
            Func<Task<T>>    action,
            IHistogramMetric metric,
            Func<T, int>     getCount,
            ICountMetric?    errorCount = null,
            LabelValue[]?    labels     = null
        ) where T : class {
            var stopwatch = Stopwatch.StartNew();

            T? result = null;

            try {
                var task = action();
                result = task.IsCompleted ? task.Result : await action();
            }
            catch (Exception) {
                errorCount?.Inc(labels: labels.ValueOrEmpty());

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