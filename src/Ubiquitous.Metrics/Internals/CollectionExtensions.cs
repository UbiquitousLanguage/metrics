using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Ubiquitous.Metrics.Internals {
    internal static class CollectionExtensions {
        [DebuggerStepThrough]
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> doIt) => Parallel.ForEach(collection, doIt);

        [DebuggerStepThrough]
        public static T[] ValueOrEmpty<T>(this T[]? collection) => collection ?? Array.Empty<T>();

        [DebuggerStepThrough]
        public static IEnumerable<T> ValueOrEmpty<T>(this IEnumerable<T>? collection) => collection ?? Enumerable.Empty<T>();

        [DebuggerStepThrough]
        public static IEnumerable<T> SafeUnion<T>(this IEnumerable<T>? first, IEnumerable<T>? second)
            => (first ?? new List<T>()).Union(second ?? new List<T>());
    }
}
