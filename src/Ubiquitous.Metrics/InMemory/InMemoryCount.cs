namespace Ubiquitous.Metrics.InMemory;

public class InMemoryCount : ICountMetric {
    long _count;

    protected internal InMemoryCount() { }

    public void Inc(string[]? labels = null, int count = 1) => Inc(count);
        
    void Inc(int count) => Interlocked.Add(ref _count, count);

    public long Count => _count;
}