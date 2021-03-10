using System;
using Microsoft.Extensions.Logging;

namespace Ubiquitous.Metrics.MicrosoftLog {
    class LogCount : LoggingMetric, ICountMetric {
        internal LogCount(MetricDefinition metricDefinition, ILogger log) : base(metricDefinition, log) { }

        public void Inc()
        {
            Inc(1, Array.Empty<string>());
        }

        public void Inc(string label)
        {
            Inc(1, new[] {label});
        }

        public void Inc(int count, string label)
        {
            Inc(count,  new[] {label});
        }
        
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