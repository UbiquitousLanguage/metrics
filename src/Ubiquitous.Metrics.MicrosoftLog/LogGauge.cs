using Ubiquitous.Metrics.InMemory;

namespace Ubiquitous.Metrics.MicrosoftLog;

class LogGauge(MetricDefinition metricDefinition, ILogger log)
    : LoggingMetric(metricDefinition, log), IGaugeMetric {
    readonly InMemoryGauge _gauge = new();

    public void Set(double value, string[]? labels) {
        _gauge.Set(value, labels);

        Log.LogInformation(
            "Gauge {Name}: value {Value}, labels {Labels}",
            Definition.Name,
            Value,
            LabelsString(labels)
        );
    }

    double Value => _gauge.Value;
}
