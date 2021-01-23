using System.Linq;
using JetBrains.Annotations;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Prometheus {
    [PublicAPI]
    public class PrometheusConfigurator : IMetrics {
        readonly DefaultLabel[] _defaultLabels;

        public PrometheusConfigurator(params DefaultLabel[] defaultLabels) => _defaultLabels = defaultLabels;

        public ICountMetric CreateCount(MetricDefinition metricDefinition)
            => new PrometheusCount(metricDefinition, _defaultLabels);

        public IHistogramMetric CreateHistogram(MetricDefinition metricDefinition) 
            => new PrometheusHistogram(metricDefinition, _defaultLabels);

        public IGaugeMetric CreateGauge(MetricDefinition metricDefinition)
            => new PrometheusGauge(metricDefinition, _defaultLabels);
    }
}
