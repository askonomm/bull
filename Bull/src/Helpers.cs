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
                items = Content.GetFromRequest(dir, new ContentGenerationRequest
                {
                    From = (string)arguments["from"],
                })
            });
        });
    }
}