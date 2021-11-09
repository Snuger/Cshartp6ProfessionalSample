using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using RazorEngine;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Threading.Tasks;
using RazorEngine.Templating;

namespace Swashbuckle.AspNetCore.Swagger.ExportExtension.SwaggerDocGen
{
    public class SwaggerHtmlDocGenProvider
    {   

        private const string Html_Doc_Template_path = "Swashbuckle.AspNetCore.Swagger.ExportExtension.node_modules.swagger_ui_extension.SwaggerDoc.cshtml";

        public SwaggerHtmlDocGenProvider()
        {
        }

        public async Task<string> SwaggerToHtml(HttpContext context, string version) {

            string html = string.Empty;
           var generator=  context.RequestServices.GetService(typeof(SwaggerGenerator)) as SwaggerGenerator;
            var model= generator.GetSwagger(version);

            //foreach (var path in model.Paths) {

            //    foreach (var opera in  path.Value.Operations) {

            //        foreach (var response in opera.Value.Responses) {

            //            foreach (var content in response.Value.Content) { 
                        
            //                string key=content.Key;
            //                foreach (var example in content.Value.Examples) { 
            //                    string _example =example.Key;   
                                
            //                }
            //            }
            //        }               
            //    }
            //}

            foreach (var example in model.Components.Examples) { 
                  var key = example.Key;
                   var val = example.Value;
                
            }


            var assemble = typeof(SwaggerHtmlDocGenProvider).Assembly;         
            using (var stream = assemble.GetManifestResourceStream(Html_Doc_Template_path)) {
                StreamReader reader = new StreamReader(stream);
                var templateStr = reader.ReadToEnd();
                try
                {
                    html = Engine.Razor.RunCompile(templateStr, "ApiDoc", typeof(OpenApiDocument), model, null);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                //html = Engine.Razor.RunCompile(templateStr, "ApiDoc", typeof(OpenApiDocument), model,null);            
                reader.Close();
            }
            return html;              
        }       

    }
}
