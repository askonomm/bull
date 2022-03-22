using HandlebarsDotNet;

namespace Bull;

/// <summary>
/// 
/// </summary>
public class Template
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="templateName"></param>
    /// <returns></returns>
    private static string GetContents(string rootDir, string templateName)
    {
        string[] pathParts = {rootDir, "layouts", templateName + ".hbs"};
        var path = Path.Combine(pathParts);

        return !File.Exists(path) ? "" : File.ReadAllText(path);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="templateName"></param>
    /// <returns></returns>
    public static HandlebarsTemplate<object, object> Get(string rootDir, string templateName)
    {
        var templateContents = GetContents(rootDir, templateName);
        var template = Handlebars.Compile(Partials.Parse(rootDir, templateContents));

        return template;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static HandlebarsTemplate<object, object> From(string rootDir, string path)
    {
        var contents = !File.Exists(path) ? "" : File.ReadAllText(path);

        return Handlebars.Compile(Partials.Parse(rootDir, contents));
    }
}