using HandlebarsDotNet;

namespace Bull;

public class Template
{
    /// <summary>
    /// 
    /// </summary>
    private readonly string _dir;

    /// <summary>
    /// 
    /// </summary>
    private readonly IDictionary<string, HandlebarsTemplate<object, object>> templates;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dir"></param>
    public Template(string dir)
    {
        _dir = dir;
        templates = new Dictionary<string, HandlebarsTemplate<object, object>>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="templateName"></param>
    /// <returns></returns>
    private string GetContents(string templateName)
    {
        string[] pathParts = {_dir, "_layouts", templateName, ".hbs"};
        var path = Path.Combine(pathParts);

        return !File.Exists(path) ? "" : File.ReadAllText(path);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="templateName"></param>
    /// <returns></returns>
    public HandlebarsTemplate<object, object> Get(string templateName)
    {
        if (templates.ContainsKey(templateName))
        {
            return templates[templateName];
        }

        var templateContents = GetContents(templateName);
        var template = Handlebars.Compile(Partials.Parse(_dir, templateContents));

        templates.Add(templateName, template);

        return template;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public HandlebarsTemplate<object, object> From(string path)
    {
        var contents = !File.Exists(path) ? "" : File.ReadAllText(path);

        return Handlebars.Compile(Partials.Parse(_dir, contents));
    }
}