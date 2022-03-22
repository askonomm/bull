using System.Text.RegularExpressions;

namespace Bull;

/// <summary>
/// 
/// </summary>
public class Partials
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="partialsDir"></param>
    /// <param name="contents"></param>
    /// <returns></returns>
    public static string Parse(string partialsDir, string contents)
    {
        var rx = new Regex(@"{{>(.*?)}}");

        if (!rx.IsMatch(contents)) return contents;

        var matches = rx.Matches(contents);

        foreach (Match match in matches)
        {
            var partialName = match.Value.Replace("{{>", "").Replace("}}", "").Trim();

            contents = contents.Replace(match.Value, Get(partialsDir, partialName));
        }

        return contents;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="partialsDir"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    private static string Get(string partialsDir, string name)
    {
        var partialPath = Path.Combine(new[] { partialsDir, name + ".hbs" });

        if (!File.Exists(partialPath))
        {
            return "";
        }

        return File.ReadAllText(partialPath);
    }
}

