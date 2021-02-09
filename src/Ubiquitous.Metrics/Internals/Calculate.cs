using System.Diagnostics;

namespace Ubiquitous.Metrics.Internals {
    public static class Calculate {
        public static double WatchSum(this Stopwatch stopwatch)
            => stopwatch.ElapsedTicks / (double) Stopwatch.Frequency;
    }
}