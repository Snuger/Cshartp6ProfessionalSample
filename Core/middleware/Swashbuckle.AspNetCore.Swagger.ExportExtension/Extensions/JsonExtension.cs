using System.Text.Json;

namespace Swashbuckle.AspNetCore.Swagger.ExportExtension.Extensions
{
    public static class JsonExtension
    {
        public static string ToJson(this object obj)
        {
            return JsonSerializer.Serialize(obj);
        }
        //private static string ConvertJsonString(string str)
        //{
        //    JsonSerializer serializer = new JsonSerializer();
        //    TextReader tr = new StringReader(str);
        //    JsonTextReader jtr = new JsonTextReader(tr);
        //    object obj = serializer.Deserialize(jtr);
        //    if (obj != null)
        //    {
        //        StringWriter textWriter = new StringWriter();
        //        JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
        //        {
        //            Formatting = Formatting.Indented,
        //            Indentation = 4,
        //            IndentChar = ' '
        //        };
        //        serializer.Serialize(jsonWriter, obj);
        //        return textWriter.ToString();
        //    }
        //    else
        //    {
        //        return str;
        //    }
        //}
    }
}
