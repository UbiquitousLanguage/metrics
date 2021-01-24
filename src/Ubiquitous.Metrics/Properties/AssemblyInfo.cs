using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Ubiquitous.Metrics.Prometheus")]
[assembly: InternalsVisibleTo("Ubiquitous.Metrics.Dogstatsd")]

// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices
{
    public class IsExternalInit{}
}

