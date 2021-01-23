namespace Ubiquitous.Metrics.Labels {
    public record LabelName {
        internal string Name { get; }
        
        public LabelName(string name) => Name = name;

        public static implicit operator LabelName(string name) => new(name);

        public static implicit operator string(LabelName labelName) => labelName.Name;
    }
}
