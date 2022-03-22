namespace Bull;

public class Pages
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public static List<Page> Get(string dir)
    {
        var pages = new List<Page>();

        if (!Directory.Exists(dir))
        {
            Console.WriteLine("Directory does not exist.");
            return new List<Page>();
        }

        Parallel.ForEach(GetPaths(dir), pagePath =>
        {
            pages.Add(GetItem(dir, pagePath));
        });

        return pages;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rootDir"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    private static string ComposeSlugFromPath(string rootDir, string path)
    {
        return path
            .Replace(rootDir, "")
            .Replace(".hbs", "")[1..];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rootDir"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    private static string ComposeDirFromPath(string rootDir, string path)
    {
        return String.Join("/", path
            .Replace(rootDir, "")
            .Split("/")
            .SkipLast(1));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private static IEnumerable<string> GetPaths(string path)
    {
        var filePaths = Directory.GetFiles(path).Where(item => item.EndsWith(".html.hbs")).ToArray();
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
    /// <param name="rootDir"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    private static Page GetItem(string rootDir, string path)
    {
        return new Page
        {
            Slug = ComposeSlugFromPath(rootDir, path),
            Dir = ComposeDirFromPath(rootDir, path),
            Path = path,
            Contents = File.ReadAllText(path),
        };
    }
}