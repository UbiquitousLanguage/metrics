using System;
using FluentAssertions;
using Xunit;

namespace Ubiquitous.Metrics.Tests {
    public class MetricsInstanceTests {
        [Fact]
        public void ShouldNotThrowIfMetricsNotConfigured() {
            Action captureSomeMetrics = () => TestApplicationMetrics.CountUploads(Guid.NewGuid());

            captureSomeMetrics.Should().NotThrow<Exception>();

            TestApplicationMetrics.UploadsCounter.Should().Be(1);
        }
    }

    static class TestApplicationMetrics {
        static ICountMetric UploadsCount { get; } = Metrics.Instance.CreateCount("app_uploads", "Number of uploads", "request_id");

        public static ulong UploadsCounter { get; private set; }

        public static void CountUploads(Guid requestId) {
            UploadsCount.Inc(new[] { requestId.ToString() });
            UploadsCounter++;
        }
    }
}