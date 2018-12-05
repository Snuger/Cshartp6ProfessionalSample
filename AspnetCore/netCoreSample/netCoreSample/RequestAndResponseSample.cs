﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace netCoreSample
{
    public class RequestAndResponseSample
    {
        private static string GetDiv(string key, string value) => $"<div><span>{key}:</span> <span>{value}</span></div>";


        public static string GetRequestInformation(HttpRequest request)
        {
            var sb = new StringBuilder();
            sb.Append(GetDiv("scheme", request.Scheme
            ));

            sb.Append(GetDiv("host", request.Host.HasValue ? request.Host.Value :"no host"));
            sb.Append(GetDiv("path", request.Path));            sb.Append(GetDiv("query string", request.QueryString.HasValue ?request.QueryString.Value : "no query string"));
            sb.Append(GetDiv("method", request.Method));            sb.Append(GetDiv("protocol", request.Protocol));
            return sb.ToString();

        }

        public static string GetHeaderInfomation(HttpRequest request) {
            var sb = new StringBuilder();
            IHeaderDictionary headers = request.Headers;
            foreach (var header in request.Headers) {
                sb.Append(GetDiv(header.Key,string.Join(";",header.Value)));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string Content(HttpRequest request)=>request.Query["data"];


        public static string ContentEncoded(HttpRequest request) => HtmlEncoder.Default.Encode(request.Query["data"]);


        public static string GetForm(HttpRequest request) {
            string result = string.Empty;
            switch (request.Method) {
                case "GET":
                    result = GetForm();
                    break;
                case "POST":
                    result = ShowForm(request);
                    break;
            }

            return result;
        }

          private static string GetForm() =>"<form method=\"post\" action=\"form\">" +"<input type=\"text\" name=\"text1\" />" +"<input type=\"submit\" value=\"Submit\" />" +"</form>";


        private static string ShowForm(HttpRequest request)
        {
            var sb = new StringBuilder();
            if (request.HasFormContentType)
            {
                IFormCollection coll = request.Form;
                foreach (var key in coll.Keys)
                {
                    sb.Append(GetDiv(key, HtmlEncoder.Default.Encode(coll[key])));
                }
                return sb.ToString();
            }
            else return "no form".Div();
        }        public static string WriteCookies(HttpResponse response) {
            response.Cookies.Append("color", "red",
                new CookieOptions { Path = "/",
                    Expires = DateTime.Now.AddDays(1)
            });
            return "cookie written".Div();
        }        public static string ReadCookies(HttpRequest request) {
            StringBuilder builder = new StringBuilder();
            IRequestCookieCollection cookies= request.Cookies;
            foreach (var key in cookies.Keys) {
                builder.Append(GetDiv(key, cookies[key]));
            }
            return builder.ToString();
        }        public static string GetJson(HttpResponse response) {
            var b = new
            {
                Title = "Professional C# 6",
                Publisher = "Wrox Press",
                Author = "Christian Nagel"
            };

            string json= JsonConvert.SerializeObject(b);            
            response.ContentType = "application/json";
            return json;
        }        
    }
}
