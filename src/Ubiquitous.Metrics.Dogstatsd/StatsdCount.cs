namespace Ubiquitous.Metrics.Dogstatsd;

class StatsdCount(IDogStatsd service, MetricDefinition metricDefinition)
    : StatsdMetric(service, metricDefinition), ICountMetric {
    public void Inc(string[]? labels = null, int count = 1)
        => Service.Increment(MetricName, count, tags: FormTags(labels));
}
