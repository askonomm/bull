namespace Bull;

public record ContentGenerationRequest()
{
    public string Dir { get; init; } = default!;
};