using System;
using FluentAssertions;
using Ubiquitous.Metrics.Dogstatsd;
using Xunit;

namespace Ubiquitous.Metrics.DogstatsdTests {
    public class DogstatsdServiceTests {
        [Fact]
        public void PreventUsingNotConfiguredDefaultInstance() {
            Action init = () => new StatsdConfigurator();
            init.Should().Throw<InvalidOperationException>();
        }
    }
}