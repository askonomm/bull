using HandlebarsDotNet;

namespace Bull;

public class Helpers
{
    public static void RegisterContentGenerationHelper(string dir)
    {
        Handlebars.RegisterHelper("content", (output, options, context, arguments) =>
        {
            if (arguments["from"] == null)
            {
                options.Inverse();
            }

            var content = Content.GetFromRequest(dir, new ContentGenerationRequest
            {
                From = (string)arguments["from"]
            });
        });
    }
}