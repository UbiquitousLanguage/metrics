using Microsoft.Extensions.Logging;

namespace Ubiquitous.Metrics.MicrosoftLog {
    class LogGauge : LoggingMetric, IGaugeMetric {
        internal LogGauge(MetricDefinition metricDefinition, ILogger log) : base(metricDefinition, log) { }

        public void Set(double value, params string[] labels) {
            Value = value;

            Log.LogInformation(
                "Gauge {Name}: value {Value}, labels {Labels}",
                Definition.Name,
                Value,
                LabelsString(labels)
            );
        }

        public double Value { get; private set; }
    }
}