using System;

namespace Ubiquitous.Metrics.Internals {
    public static class Ensure {
        public static void NotEmpty(string value, string parameterName) {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(parameterName);
        }
    }
}
