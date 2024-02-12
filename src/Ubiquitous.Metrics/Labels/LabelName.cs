namespace Ubiquitous.Metrics.Labels;

public record LabelName {
    public string Name { get; }
        
    [PublicAPI]
    public LabelName(string name) => Name = Ensure.NotEmpty(name, nameof(name));

    public static implicit operator LabelName(string name) => new(name);

    public static implicit operator string(LabelName labelName) => labelName.Name;
}