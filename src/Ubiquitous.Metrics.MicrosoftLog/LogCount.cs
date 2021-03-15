using Microsoft.Extensions.Logging;
using Ubiquitous.Metrics.InMemory;

namespace Ubiquitous.Metrics.MicrosoftLog {
    class LogCount : LoggingMetric, ICountMetric {
        readonly InMemoryCount _counter;

        internal LogCount(MetricDefinition definition, ILogger log) : base(definition, log)
            => _counter = new InMemoryCount();

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

    class LogCount<T> : LogCount, ICountMetric<T> {
        internal LogCount(MetricDefinition<T> definition, ILogger log) : base(definition, log)
            => GetLabels = definition.GetLabels;
        
        public GetLabels<T> GetLabels { get; }
    }
}