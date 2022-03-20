using System.Linq;

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

        // Sort by
        if (request.OrderBy != null)
        {
            content = content.OrderBy(i => i.Meta[request.OrderBy]).ToList();
        }

        // Order
        if (request.Order == "desc")
        {
            content.Reverse();
        }

        // Limit
        if (request.Limit != null)
        {
            content = content.Take((int)request.Limit).ToList();
        }

        return content;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    private static List<ContentGroup> GenerateGroupList(ContentGenerationRequest request)
    {
        var content = GenerateItemList(request);

        if (request.GroupBy == null)
        {
            return new List<ContentGroup>();
        }

        try
        {
            // Group by
            var groupedContent = content.GroupBy(i => i.Meta[request.GroupBy], (key, items) => new ContentGroup
            {
                Identifier = key,
                Items = items.ToList(),
            }).ToList();

            // Group order
            if (request.GroupOrder == "desc")
            {
                groupedContent.Reverse();
            }

            return groupedContent;
        } catch (Exception)
        {
            return new List<ContentGroup>();
        }
    }
}