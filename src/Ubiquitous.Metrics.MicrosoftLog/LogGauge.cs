using Microsoft.Extensions.Logging;
using Ubiquitous.Metrics.InMemory;

namespace Ubiquitous.Metrics.MicrosoftLog {
    class LogGauge : LoggingMetric, IGaugeMetric {
        readonly InMemoryGauge _gauge;

        internal LogGauge(MetricDefinition metricDefinition, ILogger log) : base(metricDefinition, log)
            => _gauge = new InMemoryGauge(metricDefinition);

        public void Set(double value, string[]? labels) {
            _gauge.Set(value, labels);
            Log.LogInformation(
                "Gauge {Name}: value {Value}, labels {Labels}",
                Definition.Name,
                Value,
                LabelsString(labels)
            );
        }

        public double Value => _gauge.Value;
    }
}