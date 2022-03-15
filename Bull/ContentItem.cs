namespace Bull;

public record ContentItem
{ 
    public string? Slug { get; init; }
    public string? Path { get; init; }
    public IDictionary<string, string>? Meta { get; init; }
    public string? Entry { get; init; }
};