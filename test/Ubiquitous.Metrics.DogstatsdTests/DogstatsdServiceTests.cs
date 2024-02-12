using System.Diagnostics.CodeAnalysis;
using Ubiquitous.Metrics.Dogstatsd;

namespace Ubiquitous.Metrics.DogstatsdTests;

public class DogstatsdServiceTests {
    [Fact]
    [SuppressMessage("Performance", "CA1806:Do not ignore method results")]
    public void PreventUsingNotConfiguredDefaultInstance() {
        Action init = () => new StatsdConfigurator();
        init.Should().Throw<InvalidOperationException>();
    }
}