using System.Collections.Generic;
using System.Linq;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Prometheus {
    static class LabelsExtensions {
        public static Dictionary<string, string>? ToDictionary(this Label[]? labels)
            => labels?.ToDictionary(x => x.Name.Name, x => x.Value);
    }
}
