namespace Bull;

public record ContentItem
{
    public string Slug { get; init; } = default!;
    public string Path { get; init; } = default!;
    public IDictionary<string, string> Meta { get; init; } = default!;
    public string Entry { get; init; } = default!;
};