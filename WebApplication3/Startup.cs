using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Context;
using Context.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Api
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //services.AddIdentityServer();

            services.AddControllersWithViews();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddControllers();
            //services.AddVersionedApiExplorer(opt => opt.GroupNameFormat = "'v'VVV");
            services.AddApiVersioning(o => { o.ReportApiVersions = true; })
                .AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddAutoMapper(typeof(Startup));
            services.AddHttpClient();
            services.AddTransient<IUserService, UserService>();
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<RestApiContext>(optionsBuilder =>
                    optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DatabaseConnection"),
                        contextOptionsBuilder => contextOptionsBuilder
                            .MigrationsAssembly("Migrations")));

            //services.AddSwaggerGen();

            services.AddAuthentication("Bearer")
           .AddJwtBearer("Bearer", options =>
           {
               Console.WriteLine(Configuration.GetSection("IdentityServer:Url").Value);
               options.Authority = Configuration.GetSection("IdentityServer:Url").Value;
               options.RequireHttpsMetadata = false;
               options.IncludeErrorDetails = true;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateAudience = false,
               };

           });          
           //.AddOpenIdConnect("oidc", options =>
           //{
           //    options.SignInScheme = "Cookies";
           //    options.Authority = "https://localhost:44336";
           //    options.RequireHttpsMetadata = true;

           //    options.ClientId = "client";
           //    options.ClientSecret = "secret";
           //    options.ResponseType = "code id_token";

           //    options.SaveTokens = true;
           //    options.GetClaimsFromUserInfoEndpoint = true;

           //    options.Scope.Add("user.api");
           //});

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v2", new OpenApiInfo()
                {
                    Title = "V2",
                    Version = "v2"
                });
                option.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "V1",
                    Version = "v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in apiDescriptionProvider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }

                    app.Map($"/swagger/versions_info", builder => builder.Run(async context =>
                        await context.Response.WriteAsync(
                            string.Join(Environment.NewLine, options.ConfigObject.Urls.Select(
                                descriptor => $"{descriptor.Name} {descriptor.Url}"))).ConfigureAwait(false)));
                    options.DocExpansion(DocExpansion.List);
                });
            }
            else
            {
                app.UseExceptionHandler("/home/error");
            }

            //The authentication middleware should be added before the MVC in the pipeline.
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            // Added after upgrading to .NET Core 3.1
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
