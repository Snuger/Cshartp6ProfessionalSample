using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Swashbuckle.AspNetCore.Swagger.ExportExtension.Extensions
{
    public static class DefaultJsonSerializerOptions
    {
        public static JsonSerializerOptions options => new JsonSerializerOptions { Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) };
    }
}
