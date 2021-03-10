using Microsoft.Extensions.Logging;
using Ubiquitous.Metrics.InMemory;

namespace Ubiquitous.Metrics.MicrosoftLog {
    class LogCount : LoggingMetric, ICountMetric {
        readonly InMemoryCount _counter;

        internal LogCount(MetricDefinition metricDefinition, ILogger log) : base(metricDefinition, log)
            => _counter = new InMemoryCount(metricDefinition);

        public void Inc(string[]? labels, int count = 1) {
            _counter.Inc(labels, count);
            Log.LogInformation(
                "Counter {Name}: value {Value}, labels {Labels}",
                Definition.Name,
                Count,
                LabelsString(labels)
            );
        }

        public long Count => _counter.Count;
    }
}