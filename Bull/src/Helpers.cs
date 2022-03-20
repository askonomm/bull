using HandlebarsDotNet;

namespace Bull;

public class Helpers
{
    private static string? GetStringOrNull(Arguments arguments, string key)
    {
        try
        {
            return (string)arguments[key];
        } catch(Exception)
        {
            return null;
        }
    }

    private static int? GetIntOrNull(Arguments arguments, string key)
    {
        try
        {
            return (int)arguments[key];
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dir"></param>
    public static void RegisterContentGenerationHelper(string dir)
    {
        Handlebars.RegisterHelper("content", (output, options, context, arguments) =>
        {
            if (GetStringOrNull(arguments, "from") == null)
            {
                options.Inverse();
            }

            options.Template(output, new
            {
                items = ContentGenerator.Generate(new ContentGenerationRequest
                {
                    From = Path.Combine(new[] { dir, (string)arguments["from"] }),
                    Limit = GetIntOrNull(arguments, "limit"),
                    OrderBy = GetStringOrNull(arguments, "order-by"),
                    Order = GetStringOrNull(arguments, "order"),
                    GroupBy = GetStringOrNull(arguments, "group-by"),
                    GroupOrder = GetStringOrNull(arguments, "group-order"),
                })
            });
        });
    }

    public static void RegisterDateFormatHelper()
    {
        //Handlebars.RegisterHelper("date-format", ())
    }
}