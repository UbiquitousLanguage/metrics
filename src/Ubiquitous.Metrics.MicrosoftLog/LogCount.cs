using Microsoft.Extensions.Logging;

namespace Ubiquitous.Metrics.MicrosoftLog {
    class LogCount : LoggingMetric, ICountMetric {
        internal LogCount(MetricDefinition metricDefinition, ILogger log) : base(metricDefinition, log) { }

        public void Inc(int count = 1, params string[] labels) {
            Count += count;

            Log.LogInformation(
                "Counter {Name}: value {Value}, labels {Labels}",
                Definition.Name,
                Count,
                LabelsString(labels)
            );
        }

        public long Count { get; private set; }
    }
}