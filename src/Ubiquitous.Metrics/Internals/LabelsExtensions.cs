using System.Linq;
using Ubiquitous.Metrics.Labels;
// ReSharper disable ParameterTypeCanBeEnumerable.Global
// ReSharper disable ReturnTypeCanBeEnumerable.Global

namespace Ubiquitous.Metrics.Internals {
    static class LabelsExtensions {
        public static string[]? GetStrings(this LabelName[]? labelNames) 
            => labelNames?.Select(x => x.Name).ToArray();
        
        public static string[]? GetLabelNames(this Label[]? labels) 
            => labels?.Select(x => x.Name.Name).ToArray();
        
        public static string[] GetLabels(this Label[] labels) 
            => labels.Select(x => x.Value).ToArray();
    }
}
