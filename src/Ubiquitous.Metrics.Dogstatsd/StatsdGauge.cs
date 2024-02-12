namespace Ubiquitous.Metrics.Dogstatsd;

class StatsdGauge(IDogStatsd service, MetricDefinition metricDefinition)
    : StatsdMetric(service, metricDefinition), IGaugeMetric {
    public void Set(double value, string[]? labels)
        => Service.Gauge(MetricName, value, tags: FormTags(labels));
}
