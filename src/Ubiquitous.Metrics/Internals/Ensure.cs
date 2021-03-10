using System;

namespace Ubiquitous.Metrics.Internals {
    static class Ensure {
        public static void NotEmpty(string value, string parameterName) {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(parameterName);
        }

        public static T NotDefault<T>(T? value, string errorMessage) where T : class {
            if (value == default) throw new InvalidOperationException(errorMessage);
            return value;
        }
    }
}
