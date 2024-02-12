using System.Diagnostics.CodeAnalysis;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Prometheus;

static class LabelsExtensions {
    [return: NotNullIfNotNull(nameof(labels))]
    public static Dictionary<string, string>? ToDictionary(this Label[]? labels)
        => labels?.ToDictionary(x => x.Name.Name, x => x.Value);
}