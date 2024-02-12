using System.Reflection;
using Ubiquitous.Metrics.Internals;
using Ubiquitous.Metrics.Labels;

namespace Ubiquitous.Metrics.Dogstatsd;

/// <summary>
/// Datadog StatsD metrics configurator
/// </summary>
[PublicAPI]
public class StatsdConfigurator : IMetricsProvider {
    IDogStatsd _service;
        
    /// <summary>
    /// Instantiate the Datadog StatsD metrics configurator
    /// </summary>
    /// <param name="defaultLabels">Optional: default labels, which will be added for all the configured metrics.
    /// Usually, things like the app name and the environment are used as default labels.</param>
    /// <param name="prefix">Prefix, which will be added for all the metrics</param>
    /// <param name="server">StatsD server address</param>
    public StatsdConfigurator(Label[] defaultLabels, string? prefix = null, string? server = null) {
        _service = GetService();
        _service.Configure(
            new StatsdConfig {
                ConstantTags     = StatsTags.FormTags(defaultLabels.GetLabelNames(), defaultLabels.GetLabels()),
                StatsdServerName = server,
                Prefix           = prefix
            }
        );
    }

    /// <summary>
    /// Instantiate the Datadog StatsD metrics configurator with a given instance of <see cref="IDogStatsd"/>.
    /// </summary>
    /// <param name="dogStatsd">Pre-configured Dogstatsd service instance.</param>
    public StatsdConfigurator(IDogStatsd dogStatsd) => _service = dogStatsd;

    /// <summary>
    /// Instantiate the Datadog StatsD metrics configurator.
    /// Should only be used if DogStatsd is already configured.
    /// </summary>
    public StatsdConfigurator() {
        _service = GetService();
        var field  = typeof(DogStatsdService).GetField("_config", BindingFlags.Instance | BindingFlags.NonPublic);
        var config = (StatsdConfig?) field!.GetValue(_service);

        if (config == null)
            throw new InvalidOperationException("Dogstatsd hasn't been configured");
    }

    static DogStatsdService GetService() {
        var field = typeof(DogStatsd).GetField("_dogStatsdService", BindingFlags.NonPublic | BindingFlags.Static);
        var service = (DogStatsdService?) field!.GetValue(null);

        return service ?? throw new InvalidOperationException("Dogstatsd service is not available");
    }

    public ICountMetric CreateCount(MetricDefinition metricDefinition) => new StatsdCount(_service, metricDefinition);

    public IHistogramMetric CreateHistogram(MetricDefinition metricDefinition)
        => new StatsdHistogram(_service, metricDefinition);

    public IGaugeMetric CreateGauge(MetricDefinition metricDefinition) => new StatsdGauge(_service, metricDefinition);
}