namespace Ubiquitous.Metrics.Internals;

static class Ensure {
    public static string NotEmpty(string value, string parameterName)
        => string.IsNullOrWhiteSpace(value) ? throw new ArgumentNullException(parameterName) : value;

    public static T NotDefault<T>(T? value, string errorMessage) where T : class {
        if (value == default) throw new InvalidOperationException(errorMessage);

        return value;
    }
}