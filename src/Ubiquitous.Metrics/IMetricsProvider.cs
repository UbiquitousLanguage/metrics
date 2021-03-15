namespace Ubiquitous.Metrics {
    /// <summary>
    /// Metrics provider interface
    /// </summary>
    public interface IMetricsProvider {
        /// <summary>
        /// Create a counter metric
        /// </summary>
        /// <param name="definition">Metric definition (name, description and labels)</param>
        /// <returns></returns>
        ICountMetric CreateCount(MetricDefinition definition);

        ICountMetric<T> CreateCount<T>(MetricDefinition<T> definition);

        /// <summary>
        /// Create a histogram metric
        /// </summary>
        /// <param name="definition">Metric definition (name, description and labels)</param>
        /// <returns></returns>
        IHistogramMetric CreateHistogram(MetricDefinition definition);

        /// <summary>
        /// Create a gauge metric
        /// </summary>
        /// <param name="definition">Metric definition (name, description and labels)</param>
        /// <returns></returns>
        IGaugeMetric CreateGauge(MetricDefinition definition);
    }
}
