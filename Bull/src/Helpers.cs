using HandlebarsDotNet;

namespace Bull;

public class Helpers
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="arguments"></param>
    /// <param name="key"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="arguments"></param>
    /// <param name="key"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 
    /// </summary>
    public static void RegisterDateFormatHelper()
    {
        Handlebars.RegisterHelper("date-format", (writer, context, parameters) =>
        {
            var format = GetStringOrNull(parameters, "format");

            if (format == null)
            {
                writer.WriteSafeString("");
            }

            try
            {
                writer.WriteSafeString(DateTime.Parse((string)parameters[0]).ToString(format));
            } catch(Exception)
            {
                writer.WriteSafeString("");
            }
        });
    }
}