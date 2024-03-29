﻿using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger.ExportExtension.Extensions;
using Swashbuckle.AspNetCore.Swagger.ExportExtension.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swashbuckle.AspNetCore.Swagger.ExportExtension.SwaggerDocGen
{
    public class SwaggerMakeDownDocGenerator : ISwaggerDocGenerator
    {
        /// <summary>
        /// SwaggerGenerator
        /// </summary>
        private readonly SwaggerGenerator _generator;
        
        /// <summary>
        /// Schemas
        /// </summary>
        private IDictionary<string, OpenApiSchema> Schemas;

        /// <summary>
        /// contentType
        /// </summary>
        const string contentType = "application/json";


        /// <summary>
        /// SwaggerDocGenerator
        /// </summary>
        /// <param name="swagger"></param>
        public SwaggerMakeDownDocGenerator(SwaggerGenerator swagger)
        {
            _generator = swagger;
        }

        /// <summary>
        /// 生成MarkDown
        /// </summary>
        /// <returns></returns>
        public string GetSwaggerDoc(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("name is null !");
            var document = _generator.GetSwagger(name);
            if (document == null)
                throw new Exception("document is null !");
            Schemas = document.Components.Schemas;
            var markDown = new StringBuilder();
            markDown.AppendLine(document?.Info?.Title.H());//文档标题
            markDown.AppendLine(document?.Info?.Description.Ref());//文档描述
            foreach (var path in document.Paths)
            {
                foreach (var operationItem in path.Value.Operations)
                {
                    var operation = operationItem.Value;
                    var method = operationItem.Key.ToString();
                    var row = new StringBuilder();
                    var url = path.Key;
                    var title = operation.Summary ?? url;
                    var query = GetParameters(operation.Parameters);
                    var (requestExapmle, requestSchema) = GetRequestBody(operation.RequestBody);
                    var (responseExapmle, responseSchema) = GetResponses(operation.Responses);                
                    row.AppendLine(title.H(2));//接口名称
                    row.AppendLine();
                    row.AppendLine("基本信息".H(3).NewLine());//基本信息
                    row.AppendLine();
                    row.AppendLine($"{"接口地址：".B()}{url}".Li().NewLine());
                    row.AppendLine($"{"请求方式：".B()}{method}".Li().NewLine());
                    if (method == "Post" || method == "Put")
                    {
                        row.AppendLine($"{"请求类型：".B()}{contentType}".Li().NewLine());
                    }
                    if (string.IsNullOrWhiteSpace(query) == false)//Query
                    {
                        row.AppendLine("Parameters".H(3));
                        row.AppendLine(query);
                    }
                    if (string.IsNullOrWhiteSpace(requestSchema) == false)//RequestSchema
                    {
                        row.AppendLine("Request Schema".H(3));
                        row.AppendLine(requestSchema.Code());
                    }
                    if (string.IsNullOrWhiteSpace(requestExapmle) == false)//RequestBody
                    {
                        row.AppendLine("RequestBody Example".H(3));
                        row.AppendLine(requestExapmle.Code());
                    }
                    if (string.IsNullOrWhiteSpace(responseSchema) == false)//ResponseSchema
                    {
                        row.AppendLine("Response Schema".H(3));
                        row.AppendLine(responseSchema.Code());
                    }
                    if (string.IsNullOrWhiteSpace(responseExapmle) == false)//ResponseBody
                    {
                        row.AppendLine("ResponseBody Example".H(3));
                        row.AppendLine(responseExapmle.Code());
                    }
                    if (string.IsNullOrWhiteSpace(row.ToString()) == false) {
                        markDown.AppendLine(row.ToString().Br());
                        markDown.AppendLine();
                    }
                        
                }
            }
            return markDown.ToString();
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="apiParameters"></param>
        /// <returns></returns>
        private string GetParameters(IList<OpenApiParameter> apiParameters)
        {
            string str = null;
            var isFirst = true;
            foreach (var parameter in apiParameters)
            {
                var queryTitle = "|参数名称|参数类型|参数位置|描述|".NewLine();
                queryTitle += "|:----:|:----:|:----:|:----:|".NewLine();
                var queryStr = $"|{parameter.Name}|{parameter.Schema.Type ?? parameter.Schema.Reference.Id}|{parameter.In}|{parameter.Description}|".NewLine();
                str += isFirst ? $"{queryTitle}{queryStr}" : queryStr;
                isFirst = false;
            }
            return str;
        }
        /// <summary>
        /// 获取 RequestBody 参数说明、JSON 示例
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        private (string exampleJson, string schemaJson) GetRequestBody(OpenApiRequestBody body)
        {
            if (body == null || body.Content.ContainsKey(contentType) == false) return (null, null);
            string exampleJson = null, schemaJson = null;
            var schema = body.Content[contentType].Schema;
            exampleJson += GetExapmple(schema).ToJson();
            schemaJson += GetModelInfo(schema, (id) => GetModelInfo(id)).ToJson();
            return (exampleJson, schemaJson);
        }
        /// <summary>
        /// 获取 GetResponses 参数说明、JSON 示例
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        private (string exampleJson, string schemaJson) GetResponses(OpenApiResponses body)
        {
            if (body == null || body["200"].Content.ContainsKey(contentType) == false) return (null, null);
            string exampleJson = null, schemaJson = null;
            var schema = body["200"].Content[contentType].Schema;
            exampleJson += GetExapmple(schema).ToJson();
            schemaJson += GetModelInfo(schema, (id) => GetModelInfo(id, false)).ToJson();
            return (exampleJson, schemaJson);
        }
        /// <summary>
        /// 获取 Body 示例
        /// </summary>
        /// <param name="apiSchema"></param>
        /// <returns></returns>
        private object GetExapmple(OpenApiSchema apiSchema)
        {
            object exapmle = null;
            if (apiSchema.IsObject(Schemas))
            {
                var key = apiSchema.Reference.Id;
                exapmle = GetExapmple(key);
            }
            else if (apiSchema.IsArray())
            {
                if (apiSchema.IsBaseTypeArray())
                    exapmle = new[] { GetDefaultValue(apiSchema.Items.Type) };
                else
                    exapmle = new[] { GetExapmple(apiSchema.Items.Reference.Id) };
            }
            else if (apiSchema.IsEnum(Schemas))
            {
                var key = apiSchema.Reference.Id;
                exapmle = GetEnum(key).Select(x => x.Value).Min();
            }
            else
            {
                exapmle = GetDefaultValue(apiSchema.Type);
            }
            return exapmle;
        }

        /// <summary>
        /// 获取枚举的值
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        private int[] GetEnumValues(string enumType) => GetEnum(enumType).Select(x => x.Value).ToArray();
        /// <summary>
        /// 获取枚举
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        private IEnumerable<OpenApiInteger> GetEnum(string enumType) => GetEnumSchema(enumType).Enum.Select(x => ((OpenApiInteger)x));
        /// <summary>
        /// 获取枚举Schema
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        private OpenApiSchema GetEnumSchema(string enumType) => Schemas.SingleOrDefault(x => x.Key == enumType).Value;

        /// <summary>
        /// 递归获取 Body 示例
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private object GetExapmple(string key)
        {
            if (key == null || Schemas.ContainsKey(key) == false) return null;
            var schema = Schemas.SingleOrDefault(x => x.Key == key).Value;
            if (schema.Properties.Any() == false) return null;
            var exapmle = new ModelExample();
            foreach (var item in schema.Properties)
            {
                if (item.Value.IsObject(Schemas))
                {
                    var objKey = item.Value.Reference.Id;
                    if (objKey == key)
                        exapmle.Add(item.Key, null);
                    else
                        exapmle.Add(item.Key, GetExapmple(objKey));
                }
                else if (item.Value.IsArray())
                {
                    if (item.Value.IsBaseTypeArray())
                        exapmle.Add(item.Key, new[] { GetExapmple(item.Value.Items.Type) });
                    else
                        exapmle.Add(item.Key, new[] { GetExapmple(item.Value.Items.Reference.Id) });
                }
                else
                {
                    if (item.Value.IsEnum(Schemas))
                        exapmle.Add(item.Key, GetEnumValues(item.Value.Reference.Id).Min());
                    else
                        exapmle.Add(item.Key, GetDefaultValue(item.Value.Format ?? item.Value.Type));
                }
            }
            return exapmle;
        }


        /// <summary>
        /// 获取 Body 参数说明
        /// </summary>
        /// <param name="apiSchema"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private object GetModelInfo(OpenApiSchema apiSchema, Func<string, object> func)
        {
            object info = null;
            var key = "";
            if (apiSchema.IsObject(Schemas) || apiSchema.IsEnum(Schemas))
                key = apiSchema.Reference.Id;
            else if (apiSchema.IsArray())
                key = apiSchema.Items.Type ?? apiSchema.Items.Reference.Id;
            else if (apiSchema.IsBaseType())
                key = apiSchema.Type;
            if (key != null)
                info = func(key);
            return info;
        }
        /// <summary>
        /// 递归获取 Body 参数说明
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isShowRequired"></param>
        /// <returns></returns>
        private object GetModelInfo(string key, bool isShowRequired = true)
        {
            if (key == null) return null;
            if (key != null && Schemas.ContainsKey(key) == false) return key;
            var schema = Schemas.SingleOrDefault(x => x.Key == key).Value;
            if (schema.Properties.Any() == false)
                return new EnumInfo()
                {
                    枚举范围 = GetEnumValues(key),
                    枚举描述 = schema.Description,
                    枚举类型 = schema.Format,
                    枚举名称 = key
                };
            var properties = new Dictionary<string, object>();
            foreach (var item in schema.Properties)
            {
                object obj = "object";
                if (item.Value.IsObject(Schemas))
                {
                    var objKey = item.Value.Reference.Id;
                    if (objKey == key)
                        obj = objKey;
                    else
                        obj = GetModelInfo(objKey, isShowRequired);
                }
                else if (item.Value.IsArray())
                {
                    var arrayKey = "";
                    if (item.Value.IsBaseTypeArray())
                        arrayKey = item.Value.Items.Type;
                    else
                        arrayKey = item.Value.Items.Reference.Id;
                    obj = new[] { GetModelInfo(arrayKey, isShowRequired) };
                }
                else if (item.Value.IsEnum(Schemas))
                {
                    var enumKey = item.Value.Reference.Id;
                    var enumObj = GetEnumSchema(enumKey);
                    obj = new EnumInfo()
                    {
                        枚举范围 = GetEnumValues(enumKey),
                        枚举类型 = enumObj.Format,
                        枚举名称 = enumKey,
                        枚举描述 = enumObj.Description
                    };
                }
                else
                {
                    obj = item.Value.Format ?? item.Value.Type;
                }

                if (isShowRequired)
                {
                    var requestModelInfo = new RequestModelInfo
                    {
                        参数类型 = obj,
                        描述 = item.Value.Description,
                        是否必传 = schema.Required.Any(x => x == item.Key),
                        可空类型 = item.Value.Nullable
                    };
                    properties.Add(item.Key, requestModelInfo);
                }
                else
                {
                    var responseModelInfo = new ResponseModelInfo
                    {
                        参数类型 = obj,
                        描述 = item.Value.Description,
                        可空类型 = item.Value.Nullable
                    };
                    properties.Add(item.Key, responseModelInfo);
                }
            }
            return properties;
        }
        /// <summary>
        /// 获取类型默认值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private object GetDefaultValue(string type)
        {
            var number = new string[] { "byte", "decimal", "double", "enum", "float", "int32", "int64", "sbyte", "short", "uint", "ulong", "ushort" };
            if (number.Any(x => type == x)) return 0;
            if (type == "string") return "string";
            if (type == "bool" || type == "boolean") return false;
            if (type == "date-time") return DateTime.Now;
            return null;
        }
        /// <summary>
        /// 获取 MarkDown 文件流
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<MemoryStream> GetSwaggerDocStreamAsync(string name)
        {
            using var stream = new MemoryStream();
            using var sw = new StreamWriter(stream);
            var content = GetSwaggerDoc(name);
            await sw.WriteLineAsync(content);
            return stream;
        }
    }
}

