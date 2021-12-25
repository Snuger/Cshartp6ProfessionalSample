using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swashbuckle.AspNetCore.Swagger.ExportExtension.Extensions
{
    public static class SwaggerDocGeneratorExtension
    { 
        /// <summary>
        /// 判断是否为 Object 类型
        /// </summary>
        /// <param name="openApiSchema"></param>
        /// <returns></returns>
        public static bool IsObject(this OpenApiSchema openApiSchema, IDictionary<string, OpenApiSchema> schemas)
        {
            return openApiSchema.Type == null && openApiSchema.Reference != null && schemas.FirstOrDefault(x => x.Key == openApiSchema.Reference.Id).Value.Enum.Count == 0;
        }
        /// <summary>
        /// 判断是否为枚举类型
        /// </summary>
        /// <param name="openApiSchema"></param>
        /// <param name="schemas"></param>
        /// <returns></returns>
        public static bool IsEnum(this OpenApiSchema openApiSchema, IDictionary<string, OpenApiSchema> schemas)
        {
            return openApiSchema.Reference != null && schemas.FirstOrDefault(x => x.Key == openApiSchema.Reference.Id).Value.Enum.Count != 0;
        }
        /// <summary>
        /// 判断是否为数组类型
        /// </summary>
        /// <param name="openApiSchema"></param>
        /// <returns></returns>
        public static bool IsArray(this OpenApiSchema openApiSchema)
        {
            return openApiSchema.Type == "array" && openApiSchema.Items != null;
        }
        /// <summary>
        /// 判断是否为基础数组类型
        /// </summary>
        /// <param name="openApiSchema"></param>
        /// <returns></returns>
        public static bool IsBaseTypeArray(this OpenApiSchema openApiSchema)
        {
            return openApiSchema.Type == "array" && openApiSchema.Items != null && openApiSchema.Items.Type != null && openApiSchema.Items.Reference == null;
        }

        /// <summary>
        /// 判断是否为基本类型
        /// </summary>
        /// <param name="openApiSchema"></param>
        public static bool IsBaseType(this OpenApiSchema openApiSchema)
        {
            return openApiSchema.Type != null;
        }
    }
}
