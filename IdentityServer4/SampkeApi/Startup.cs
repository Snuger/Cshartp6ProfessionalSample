using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using IdentityModel.Client;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SampkeApi
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
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddMvc();        
            services.AddCors(options =>
            {
                 options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });        
            services.AddControllers();
            services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "http://localhost:5000/identity";
                options.RequireHttpsMetadata = false;                        
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
                options.Events = new JwtBearerEvents()
                {
                    OnChallenge = context =>
                    {
                        Console.WriteLine(context.AuthenticateFailure.Message);
                        Console.WriteLine("11111");
                        Console.WriteLine(context.ErrorDescription);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated=context=>{
                        Console.WriteLine("23232323232");
                        return Task.CompletedTask;
                    },
                    OnMessageReceived=context => {
                        Console.WriteLine("5555555");
                        context.Token = context.Request.Query["access_token"];                          
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed=context => {
                        Console.WriteLine("4444444");
                        Console.WriteLine(context.Exception);
                        return Task.CompletedTask;
                    }
                };              
            });

            	
            services.AddMvcCore(opt=>{
                opt.EnableEndpointRouting=false;
            }).AddAuthorization();   
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {       
             if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }   
           app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseMvc();          
        }
    }
}
