namespace Bull;

/// <summary>
/// 
/// </summary>
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
    /// <param name="item"></param>
    /// <param name="identifier"></param>
    /// <returns></returns>
    private static string GroupByIdentifier(ContentItem item, string identifier)
    {
        if (identifier.Contains("|"))
        {
            var key = identifier.Split("|")[0];
            var modifier = String.Join("|", identifier.Split("|")[1..]);

            // Date
            if (key == "date")
            {
                var dateTime = DateTime.Parse(item.Meta["date"]);

                return dateTime.ToString(modifier);
            }
        }

        return item.Meta[identifier];
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
            var groupedContent = content.GroupBy(i => GroupByIdentifier(i, request.GroupBy), (key, items) => new ContentGroup
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