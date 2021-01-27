using System.Linq;
using Ubiquitous.Metrics.Internals;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics {
    public record MetricDefinition {
        public string   Name          { get; }
        public string   Description   { get; }
        public string[] LabelNames    { get; }

        public MetricDefinition(string name, string? description, params LabelName[] labelNames) {
            Name = name;
            Ensure.NotEmpty(name, nameof(name));

            LabelNames    = labelNames.ValueOrEmpty().GetStrings()!.ToArray();
            Description   = description ?? "";
        }
    }
}
