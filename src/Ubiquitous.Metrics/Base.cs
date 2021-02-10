using System.Threading;
using static System.BitConverter;

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
            Add(seconds * count);
            Interlocked.Add(ref _count, count);
        }

        public double Sum => Int64BitsToDouble(Interlocked.Read(ref _sum));
        public long Count => Interlocked.Read(ref _count);

        long _sum;
        long _count;

        void Add(double increment) {
            long   comp;
            double num;

            do {
                comp = _sum;
                num = Int64BitsToDouble(comp) + increment;
            } while (comp != Interlocked.CompareExchange(ref _sum, DoubleToInt64Bits(num), comp));
        }
    }
}