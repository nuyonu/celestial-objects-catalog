using System.Reflection;
using Newtonsoft.Json;

namespace Nasa.API.Common;

public class JsonDeserializeWrapper<TModel>
{
    public JsonDeserializeWrapper(TModel? value)
    {
        Value = value;
    }

    public TModel? Value { get; }

    public static async ValueTask<JsonDeserializeWrapper<TModel>?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        if (!context.Request.HasJsonContentType())
        {
            throw new BadHttpRequestException(
                "Request content type was not a recognized JSON content type.",
                StatusCodes.Status415UnsupportedMediaType);
        }

        using var sr = new StreamReader(context.Request.Body);
        var str = await sr.ReadToEndAsync();

        return new JsonDeserializeWrapper<TModel>(JsonConvert.DeserializeObject<TModel>(str));
    }
}