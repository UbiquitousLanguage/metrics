using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Ubiquitous.Metrics.Internals {
    static class CollectionExtensions {
        [DebuggerStepThrough]
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> doIt) => Parallel.ForEach(collection, doIt);

        [DebuggerStepThrough]
        public static T[] ValueOrEmpty<T>(this T[]? collection) => collection ?? Array.Empty<T>();

        [DebuggerStepThrough]
        public static string[]? ArrayOrNull(this string? value) => value == null ? null : new[] {value};
    }
}
