using System;

namespace Ubiquitous.Metrics.Internals {
    static class Ensure {
        public static void NotEmpty(string value, string parameterName) {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(parameterName);
        }
    }
}
