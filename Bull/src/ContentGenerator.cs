namespace Bull;

public class ContentGenerator
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static object Generate(ContentGenerationRequest request)
    {
        if (request.GroupBy != null)
        {
            return GenerateGroupList(request);
        }

        return GenerateItemList(request);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    private static List<ContentItem> GenerateItemList(ContentGenerationRequest request)
    {
        var content = Content.Get(request.From);

        return content;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    private static List<ContentGroup> GenerateGroupList(ContentGenerationRequest request)
    {
        var content = Content.Get(request.From);

        return new List<ContentGroup>();
    }
}