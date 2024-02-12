namespace Ubiquitous.Metrics.InMemory;

public class InMemoryGauge : IGaugeMetric {
    double _value;

    protected internal InMemoryGauge() { }

    public void Set(double value, string[]? labels = null) => Interlocked.Exchange(ref _value, value);

    public double Value => _value;
}