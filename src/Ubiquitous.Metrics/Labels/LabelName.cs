using Ubiquitous.Metrics.Internals;

namespace Ubiquitous.Metrics.Labels {
    public record LabelName {
        public string Name { get; }
        
        public LabelName(string name) {
            Ensure.NotEmpty(name, nameof(name));
            
            Name = name;
        }

        public static implicit operator LabelName(string name) => new(name);

        public static implicit operator string(LabelName labelName) => labelName.Name;
    }
}
