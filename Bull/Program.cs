using HandlebarsDotNet;

namespace Bull;

class Program
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    private static void CreateParentDirs(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="contents"></param>
    private static async void Write(string path, string contents)
    {
        await File.WriteAllTextAsync(path, contents);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dir"></param>
    private static void BuildContentItems(string dir)
    {
        var template = new Template(dir);

        // Build individual content items
        foreach (var contentItem in Content.Get(dir))
        {
            Console.WriteLine("Writing {0}", contentItem.Slug);

            var hasLayout = contentItem.Meta.TryGetValue("layout", out var layoutValue);
            var layoutName = hasLayout && layoutValue != null ? layoutValue : "default";
            var writePath = Path.Combine(new[] { dir, "public", contentItem.Slug, "index.html" });

            CreateParentDirs(Path.Combine(new[] { dir, "public", contentItem.Slug }));
            Write(writePath, template.Get(layoutName)(new
            {
                x = contentItem
            }));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dir"></param>
    private static void BuildPages(string dir)
    {
        foreach(var page in Pages.Get(dir))
        {
            Console.WriteLine("Writing {0}", page.Slug);

            var writePath = Path.Combine(new[] { dir, "public", page.Slug });

            CreateParentDirs(Path.Combine(new[] { dir, "public", page.Dir }));
            Write(writePath, Template.From(page.Path)(new
            {
                x = false,
            }));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dir"></param>
    private static void Build(string dir)
    {
        // Build content items.
        BuildContentItems(dir);

        // Build pages
        BuildPages(dir);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_"></param>
    private static void Main(string[] _)
    {
        Build("/Users/asko/projects/bull-test");
        // Potentially watch for changes
    }
}