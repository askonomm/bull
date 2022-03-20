namespace Bull;

public record ContentGenerationRequest()
{
    public string From { get; init; } = default!;
    public string? GroupBy { get; init; }
    public string? GroupOrder { get; init; }
    public string? OrderBy { get; init; }
    public string? Order { get; init; }
    public int? Limit { get; init; }
};