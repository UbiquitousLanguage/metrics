using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics {
    public interface ICountMetric {
        void Inc(int count = 1, params LabelValue[] labels);
        
        long Count { get; }
    }
}
