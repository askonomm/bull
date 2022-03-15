using System.Text.RegularExpressions;
using Markdig;

namespace Bull;

public class Content
{
    /// <summary>
    /// 
    /// </summary>
    private readonly string _dir;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dir"></param>
    public Content(string dir) => _dir = dir;
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public List<ContentItem> GetAll()
    {
        var contentItems = new List<ContentItem>();

        if (!Directory.Exists(_dir))
        {
            Console.WriteLine("Directory does not exist.");
            return new List<ContentItem>();
        }

        foreach (var contentItemPath in GetPaths(_dir))
        {
            contentItems.Add(GetItem(contentItemPath));
        }

        return contentItems;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private static IEnumerable<string> GetPaths(string path)
    {
        var filePaths = Directory.GetFiles(path).Where(item => item.EndsWith(".md")).ToArray();
        var directoryPaths = Directory.GetDirectories(path);

        foreach (var directoryPath in directoryPaths)
        {
            filePaths = filePaths.Concat(GetPaths(directoryPath)).ToArray();
        }

        return filePaths;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private string ComposeSlugFromPath(string path)
    {
        return path
            .Replace(_dir, "")
            .Replace(".md", "")[1..];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="contents"></param>
    /// <returns></returns>
    private static IDictionary<string, string> ComposeMetaFromContents(string contents)
    {
        var rx = new Regex(@"^---(.*?)---*", RegexOptions.Singleline);
        var meta = new Dictionary<string, string>();

        if (!rx.IsMatch(contents)) return meta;
        
        var metaLines = rx.Match(contents).Value
            .Split("\n")
            .Where(item => item != "---")
            .ToArray();

        foreach (var metaLine in metaLines)
        {
            var key = metaLine.Split(":")[0].Trim();
            var val = string.Join(":", metaLine.Split(":")[1..]).Trim();
                
            meta.Add(key, val);
        }

        return meta;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="contents"></param>
    /// <returns></returns>
    private static string ComposeEntryFromContents(string contents)
    {
        var rx = new Regex(@"^---(.*?)---*", RegexOptions.Singleline);
        
        return Markdown.ToHtml(rx.Replace(contents, "").Trim());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private ContentItem GetItem(string path)
    {
        var contents = File.ReadAllText(path);

        return new ContentItem
        {
            Slug = ComposeSlugFromPath(path),
            Path = path,
            Meta = ComposeMetaFromContents(contents),
            Entry = ComposeEntryFromContents(contents)
        };
    }
}