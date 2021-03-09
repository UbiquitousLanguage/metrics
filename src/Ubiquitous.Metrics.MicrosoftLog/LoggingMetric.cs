using System.Linq;
using Microsoft.Extensions.Logging;

namespace Ubiquitous.Metrics.MicrosoftLog {
    public class LoggingMetric {
        protected MetricDefinition Definition { get; }
        protected ILogger Log { get; }

        protected LoggingMetric(MetricDefinition metricDefinition, ILogger log) {
            Definition = metricDefinition;
            Log        = log;
        }

        protected string LabelsString(string[]? labels)
            => labels == null ? "" : string.Join(",", Definition.LabelNames.Select((s, i) => $"{s}:{labels[i]}"));
    }
}