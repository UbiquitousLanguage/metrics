using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics {
    public interface IGaugeMetric {
        void Set(double value, params Label[] labels);
    }
}
