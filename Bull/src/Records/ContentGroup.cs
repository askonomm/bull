namespace Bull;

public record ContentGroup
{
    public string Identifier { get; init; }
    public List<ContentItem> Items { get; init; }
}