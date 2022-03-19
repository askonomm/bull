namespace Bull;

public record Page
{
    public string Slug { get; init; } = default!;
    public string Dir { get; init; } = default!;
    public string Path { get; init; } = default!;
    public string Contents { get; init; } = default!;
}