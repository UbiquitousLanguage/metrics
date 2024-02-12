using Ubiquitous.Metrics.InMemory;

namespace Ubiquitous.Metrics.MicrosoftLog;

class LogCount(MetricDefinition metricDefinition, ILogger log)
    : LoggingMetric(metricDefinition, log), ICountMetric {
    readonly InMemoryCount _counter = new();

    public void Inc(string[]? labels, int count = 1) {
        _counter.Inc(labels, count);

        Log.LogInformation(
            "Counter {Name}: value {Value}, labels {Labels}",
            Definition.Name,
            Count,
            LabelsString(labels)
        );
    }

    long Count => _counter.Count;
}
