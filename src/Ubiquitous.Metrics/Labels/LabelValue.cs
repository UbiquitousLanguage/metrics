namespace Ubiquitous.Metrics.Labels {
    public record LabelValue {
        public string Value { get; }
        
        public LabelValue(string value) => Value = value;

        public static implicit operator LabelValue(string value) => new(value);

        public static implicit operator string(LabelValue value) => value.Value;
    }
}
