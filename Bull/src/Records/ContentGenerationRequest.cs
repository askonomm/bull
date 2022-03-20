namespace Bull;

public record ContentGenerationRequest()
{
    public string From { get; init; }
    public string? GroupBy { get; init; }
};