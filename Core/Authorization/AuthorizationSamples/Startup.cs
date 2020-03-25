using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using static IdentityModel.OidcConstants;
using IdentityModel.Client;
using System.Net.Http;

namespace AuthorizationSamples
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddControllersWithViews();

            services.AddHttpClient();
 

            services.AddControllersWithViews();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

           JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                // 使用cookie来本地登录用户（通过DefaultScheme = "Cookies"）
                options.DefaultScheme = "Cookies";
                // 设置 DefaultChallengeScheme = "oidc" 时，表示我们使用 OIDC 协议
                options.DefaultChallengeScheme = "oidc";
            })
            // 我们使用添加可处理cookie的处理程序
            .AddCookie("Cookies")
            // 配置执行OpenID Connect协议的处理程序
            .AddOpenIdConnect("oidc", options =>
            {
                // 
                options.SignInScheme = "Cookies";
                // 表明我们信任IdentityServer客户端
                options.Authority = "http://172.19.30.60:31001/hrp-gongyinlian-identity";
                // 表示我们不需要 Https
                options.RequireHttpsMetadata = false;
                // 用于在cookie中保留来自IdentityServer的 token，因为以后可能会用
                options.SaveTokens = true;

                options.ClientId = "authorization_code_test";
                options.ClientSecret = "AVdp9r2QJZF"; 
                options.ResponseType = "code"; // Authorization Code

                options.Scope.Clear();
                options.Scope.Add("api1");
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("address");
                options.Scope.Add("phone");
                options.Scope.Add("email");
                // Scope中添加了OfflineAccess后，就可以在 Action 中获得 refreshToken
                options.Scope.Add(StandardScopes.OfflineAccess);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

             app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute()
                    .RequireAuthorization();
            });
        }
    }
}
