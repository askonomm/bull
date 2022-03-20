using HandlebarsDotNet;

namespace Bull;

public class Helpers
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dir"></param>
    public static void RegisterContentGenerationHelper(string dir)
    {
        Handlebars.RegisterHelper("content", (output, options, context, arguments) =>
        {
            if (arguments["from"] == null)
            {
                options.Inverse();
            }

            options.Template(output, new
            {
                items = ContentGenerator.Generate(new ContentGenerationRequest
                {
                    From = Path.Combine(new[] { dir, (string)arguments["from"] }),
                    Order = (string)arguments["order"],
                    GroupBy = (string)arguments["group-by"] ?? null, 
                })
            });
        });
    }
}