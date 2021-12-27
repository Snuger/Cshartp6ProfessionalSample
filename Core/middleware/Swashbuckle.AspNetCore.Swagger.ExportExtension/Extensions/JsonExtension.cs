using System.Text.Json;

namespace Swashbuckle.AspNetCore.Swagger.ExportExtension.Extensions
{
    public static class JsonExtension
    {
        public static string ToJson(this object obj)
        {
            return JsonSerializer.Serialize(obj, DefaultJsonSerializerOptions.options);
        }      
    }
}
