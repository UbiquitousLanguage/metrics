namespace Ubiquitous.Metrics.Combined;

public class CombinedGauge : IGaugeMetric {
    readonly IGaugeMetric[] _inner;

    internal CombinedGauge(IGaugeMetric[] inner) => _inner = inner;

    public void Set(double value, string? label = null) => _inner.ForEach(x => x.Set(value, label));

    public void Set(double value, string[]? labels = null) => _inner.ForEach(x => x.Set(value, labels));
}