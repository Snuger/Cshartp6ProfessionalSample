using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.Swagger.ExportExtension.SwaggerDocGen;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SwaggerApiDocumentBuilderExtensions
    {     
        /// <summary>
        /// 注册<see cref="ISwaggerDocGenerator"/>服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerApiDocumentExportSupport(this IServiceCollection services)
        {
            services.AddScoped<SwaggerGenerator>();
             services.AddScoped<ISwaggerDocGenerator, SwaggerMakeDownDocGenerator>();
            services.Configure<SwaggerUIOptions>(opt => {
                //css注入
                opt.InjectStylesheet("/doc/swagger_ui_extension/swagger-common.css");//自定义样式
                opt.InjectStylesheet("/doc/buzyload/app.min.css");//等待load遮罩层样式
                //js注入
                opt.InjectJavascript("/doc/jquery/jquery.js");//jquery 插件
                opt.InjectJavascript("/doc/buzyload/app.min.js");//loading 遮罩层js
                opt.InjectJavascript("/doc/swagger_ui_extension/swagger-lang.js");//我们自定义的js
            });
            return services;        
        }
    }
}
