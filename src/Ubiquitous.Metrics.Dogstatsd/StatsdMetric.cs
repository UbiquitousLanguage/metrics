namespace Ubiquitous.Metrics.Dogstatsd;

abstract class StatsdMetric(IDogStatsd service, MetricDefinition metricDefinition) {
    readonly string[] _labelNames = metricDefinition.LabelNames;

    protected string     MetricName { get; } = metricDefinition.Name;
    protected IDogStatsd Service    { get; } = service;

    protected string[]? FormTags(string[]? labels) => StatsTags.FormTags(_labelNames, labels);
}

static class StatsTags {
    internal static string[]? FormTags(string[]? labelNames, string[]? labelValues) {
        return labelNames?.Select(FormTag).ToArray();

        string FormTag(string tag, int position) {
            var label = IsInvalidLabel(position) ? null : $":{labelValues![position]}";
            return $"{tag}{label}";
        }

        bool IsInvalidLabel(int position)
            => labelValues == null || labelValues.Length <= position ||
                string.IsNullOrEmpty(labelNames[position]);
    }
}
