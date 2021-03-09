using System.Linq;
using Ubiquitous.Metrics.Internals;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics {
    /// <summary>
    /// Basic definition of any metric
    /// </summary>
    public record MetricDefinition {
        public string   Name          { get; }
        public string   Description   { get; }
        public string[] LabelNames    { get; }

        /// <summary>
        /// Metric definition constructor
        /// </summary>
        /// <param name="name">Metric name. Ensure you use the name supported by the provider you'll use.</param>
        /// <param name="description">Metric description, which is not supported for all the providers.</param>
        /// <param name="labelNames">Label names. You'd need to supply the exact number of label values when observing the metric.</param>
        public MetricDefinition(string name, string? description, params LabelName[] labelNames) {
            Name = name;
            Ensure.NotEmpty(name, nameof(name));

            LabelNames    = labelNames.ValueOrEmpty().GetStrings()!.ToArray();
            Description   = description ?? "";
        }
    }
}
