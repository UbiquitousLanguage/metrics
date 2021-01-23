// ReSharper disable ParameterTypeCanBeEnumerable.Global

using System.Linq;
using Ubiquitous.Metrics.Internals;

namespace Ubiquitous.Metrics.Labels {
    public record DefaultLabel {
        public DefaultLabel(string name, string value) {
            Ensure.NotEmpty(name, nameof(name));
            Ensure.NotEmpty(value, nameof(value));
            
            Name  = name;
            Value = value;
        }

        public string Name  { get; }
        public string Value { get; }
    }
}
