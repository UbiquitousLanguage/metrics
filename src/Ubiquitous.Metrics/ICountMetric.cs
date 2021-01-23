using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics {
    public interface ICountMetric {
        void Inc(int count = 1, params Label[] labels);
    }
}
