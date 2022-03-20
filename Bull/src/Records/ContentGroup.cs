namespace Bull;

public record ContentGroup
{
    public string Identifier { get; init; } = default!;
    public List<ContentItem> Items { get; init; } = default!;
}