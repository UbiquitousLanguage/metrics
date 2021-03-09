using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Ubiquitous.Metrics.MicrosoftLog {
    [PublicAPI]
    public class LoggingConfigurator : IMetricsProvider {
        readonly ILogger _log;

        public LoggingConfigurator(ILoggerFactory loggerFactory) =>
            _log = loggerFactory.CreateLogger("Metrics");

        public ICountMetric CreateCount(MetricDefinition definition) => new LogCount(definition, _log);

        public IHistogramMetric CreateHistogram(MetricDefinition definition) => new LogHistogram(definition, _log);

        public IGaugeMetric CreateGauge(MetricDefinition definition) => new LogGauge(definition, _log);
    }
}