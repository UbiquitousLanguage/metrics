using System.Linq;

namespace Ubiquitous.Metrics {
    public static class Tags {
        public static string[] FormTags(string[] labelNames, DefaultLabel[] defaultLabels, Label[] labels) {
            return labelNames.Select(FormTag).ToArray();

            string FormTag(string tag, int position) {
                var combinedLabels = defaultLabels.ValueOrEmpty().Union(labels.ValueOrEmpty()).ToArray();

                var label = combinedLabels.Length <= position || string.IsNullOrEmpty(combinedLabels[position])
                    ? null
                    : $":{combinedLabels[position]}";

                return $"{tag}{label}";
            }
        }
    }
}
