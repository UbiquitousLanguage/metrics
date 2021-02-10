using System.Diagnostics;
using System.Threading;

namespace Ubiquitous.Metrics {
    public class BaseCount {
        public void Inc(int count = 1) => Interlocked.Add(ref _count, count);

        public long Count => _count;

        long _count;
    }

    public class BaseGauge {
        public void Set(double value) => Interlocked.Exchange(ref _value, value);

        public double Value => _value;

        double _value;
    }

    public class BaseHistogram {
        public void Observe(double seconds, int count = 1) {
            var newSum = _sum + seconds;
            Interlocked.Exchange(ref _sum, newSum);
            Interlocked.Add(ref _count, count);
        }
        
        public double Sum => _sum;
        public long Count => _count;

        double _sum;
        long   _count;
    }
}